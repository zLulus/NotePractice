using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.QueryTree.Models
{
    public class RegionTree
    {
        /// <summary>
        /// 自增长id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用于业务查询的id
        /// </summary>
        public string RegionId { get; set; }
        /// <summary>
        /// 父级id(记录的是父级记录的RegionId)
        /// </summary>
        public string RegionParentId { get; set; }
        /// <summary>
        /// 自身的编码，不同层级依次添加编码
        /// eg.湖北省为42,武汉市为4201，汉阳区为420105
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 行政区名称
        /// </summary>
        public string Name {get; set; }
    }
}
