using CodeLibraryForDotNetCore.QueryTree.Db;
using CodeLibraryForDotNetCore.QueryTree.Dtos;
using CodeLibraryForDotNetCore.QueryTree.Enums;
using CodeLibraryForDotNetCore.QueryTree.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibraryForDotNetCore.QueryTree
{
    public class QueryTreeDemo
    {
        private static string connectionString = @"Data Source=D:\MicroDesktop\NotePractice\Console\CodeLibraryForDotNetCore\QueryTree\Db\regionTreeDb.db";
        private RegionTreeDbContext db;
        public QueryTreeDemo()
        {
            db = new RegionTreeDbContext(connectionString);
        }
        public async Task SqliteQuery()
        {
            //ef core sqlite 使用
            using (var db2 = new RegionTreeDbContext(connectionString))
            {
                var region = db2.Regions.FirstOrDefault();
            }
        }

        public async Task TreeQuery()
        {
            //根据几个节点id，查询对应部分树
            var queryRegionIds = new List<string> { "130923", "210803", "130972", "1", "210802", "2109" };

            var resultTree = new List<RegionTreeNode>();
            var list = db.Regions.Where(x => queryRegionIds.Contains(x.Code)).ToList();

            //向上查询
            foreach (var d in list)
            {
                if (d.AdministrativeLevel != AdministrativeLevelEnum.Province)
                {
                    var tree = await GetTree(d);
                    var isExistTree = resultTree.Find(x => x.Id == tree.Id);
                    //目前不存在此树（此省级的数据）
                    if (isExistTree == null)
                    {
                        resultTree.Add(tree);
                    }
                    //存在此树，需要将当前数据合并到现有树的数据中
                    else
                    {
                        //查询两棵树的交汇点
                        var intersectionId = GetIntersection(tree, isExistTree);
                        var resultTreeIntersection = GetIntersectionChild(isExistTree, intersectionId);
                        var treeIntersection = GetIntersectionChild(tree, intersectionId);
                        //将新树的数据从交汇点开始，合并到现有树
                        foreach (var child in treeIntersection.Children)
                        {
                            resultTreeIntersection.Children.Add(child);
                        }
                    }
                }
                else
                {
                    var node = ConvertToTreeNode(d);
                    resultTree.Add(node);
                }
            }

            //打断点查看结果:resultTree
        }

        private async Task<RegionTreeNode> GetTree(RegionTree d)
        {
                RegionTreeNode tree = new RegionTreeNode();
                List<RegionTree> list = new List<RegionTree>();
                //向上查询树
                var parent = await db.Regions.FirstOrDefaultAsync(x => x.Code == d.RegionParentId);
                list.Add(d);
                if (parent != null)
                {
                    list.Add(parent);
                }
                int count = 0; //防止死循环
                while (parent != null && count < 5)
                {
                    parent = await db.Regions.FirstOrDefaultAsync(x => x.Code == parent.RegionParentId);
                    if (parent != null)
                    {
                        list.Add(parent);
                    }
                    count++;
                }
                //处理成树结构
                tree = ConvertToTreeNode(list[list.Count - 1]);
                tree.Children = new List<RegionTreeNode>();
                var node = tree.Children;
                for (int i = list.Count - 2; i >= 0; i--)
                {
                    var t = ConvertToTreeNode(list[i]);
                    node.Add(t);
                    node[0].Children = new List<RegionTreeNode>();
                    node = node[0].Children;
                }
                return tree;
        }

        private RegionTreeNode ConvertToTreeNode(RegionTree regionTree)
        {
            //这里可以写成automap，懒得写了~
            return new RegionTreeNode()
            {
                Id = regionTree.Id,
                Code = regionTree.Code,
                RegionId = regionTree.RegionId,
                RegionParentId = regionTree.RegionParentId,
                AdministrativeLevel = regionTree.AdministrativeLevel,
                Name = regionTree.Name,
                Children=new List<RegionTreeNode>()
            };
        }

        /// <summary>
        /// 查询两棵树的交汇点(最低的子节点)
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="isExistTree"></param>
        /// <returns></returns>
        private long GetIntersection(RegionTreeNode tree, RegionTreeNode isExistTree)
        {
            var treeCodeList = new List<RegionTreeNode>();
            treeCodeList.Add(new RegionTreeNode() { Id = tree.Id, Code = tree.Code });
            GetCodeList(tree, treeCodeList);
            var isExistTreeCodeList = new List<RegionTreeNode>();
            isExistTreeCodeList.Add(new RegionTreeNode() { Id = isExistTree.Id, Code = isExistTree.Code });
            GetCodeList(isExistTree, isExistTreeCodeList);

            //查询重复的，最大的code
            var intersectCodeList = (from t in treeCodeList
                                     from i in isExistTreeCodeList
                                     where t.Id == i.Id
                                     select t).ToList();
            var target = new RegionTreeNode() { Code = "" };
            foreach (var code in intersectCodeList)
            {
                if (target.Code.Length < code.Code.Length)
                {
                    target = code;
                }
            }
            return target.Id;
        }

        private void GetCodeList(RegionTreeNode tree, List<RegionTreeNode> codeList)
        {
            foreach (var node in tree.Children)
            {
                codeList.Add(new RegionTreeNode() { Id = node.Id, Code = node.Code });
                GetCodeList(node, codeList);
            }
        }

        /// <summary>
        /// 查询交汇点节点
        /// </summary>
        /// <param name="intersection"></param>
        /// <returns></returns>
        private RegionTreeNode GetIntersectionChild(RegionTreeNode tree, long intersection)
        {
            if (tree.Id == intersection)
            {
                return tree;
            }
            //保证节点遍历完
            foreach (var node in tree.Children)
            {
                if (node.Id == intersection)
                {
                    return node;
                }
            }
            foreach (var node in tree.Children)
            {
                return GetIntersectionChild(node, intersection);
            }
            return null;
        }

    }
}
