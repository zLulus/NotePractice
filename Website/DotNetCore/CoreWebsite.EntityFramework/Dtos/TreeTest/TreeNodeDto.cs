using CoreWebsite.EntityFramework.Models.TreeTest;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.EntityFramework.Dtos.TreeTest
{
    public class TreeNodeDto
    {
        public long Id { get; set; }
        public string NodeName { get; set; }
        public long? ParentId { get; set; }
        public virtual ICollection<TreeNode> Children { get; set; }
    }
}
