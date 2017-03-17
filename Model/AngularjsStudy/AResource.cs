using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AngularjsStudy
{
    public class AResource
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 执行标准
        /// </summary>
        public string standard { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? uploadTime { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        public string resourceCode { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string drawingno { get; set; }
        /// <summary>
        /// 资源位置
        /// </summary>
        public string resourceLocation { get; set; }
        /// <summary>
        /// 技术参数
        /// </summary>
        public string technicalParameters { get; set; }
        /// <summary>
        /// 材质
        /// </summary>
        public string material { get; set; }
        /// <summary>
        /// 单重
        /// </summary>
        public decimal singleWeight { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string descriptionRemark { get; set; }
    }
}
