using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.ReadAndWriteXml.Dtos
{
    public class ColumnDto
    {
        public string Name { get; set; }
        public string AliasName { get; set; }
        /// <summary>
        /// 修改数据时，是否可以被修改
        /// </summary>
        public bool IsCanNotModify { get; set; }
    }
}
