using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.FTP.Dtos
{
    public class RemoteEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 相对路径 不包含 host
        /// </summary>
        public string RelativePath { get; set; }
        /// <summary>
        /// 文件远程路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 0文件 1文件夹
        /// </summary>
        public int RType { get; set; }
    }
}
