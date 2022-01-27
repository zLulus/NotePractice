using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Dtos
{
    public class PaginationDto<TEntity> where TEntity:class
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// 总条数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 每页分页条数
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count,PageSize));

        public List<TEntity> Data { get; set; }
    }
}
