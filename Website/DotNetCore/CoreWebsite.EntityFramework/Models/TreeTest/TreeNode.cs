using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreWebsite.EntityFramework.Models.TreeTest
{
    public class TreeNode
    {
        [Key]
        public long Id { get; set; }
        public string NodeName { get; set; }
        public long? ParentId { get; set; }
        public virtual TreeNode Parent { get; set; }
        public virtual ICollection<TreeNode> Children { get; set; }

    }
}
