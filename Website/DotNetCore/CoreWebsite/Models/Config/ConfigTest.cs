using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace CoreWebsite.Models.Config
{
    public class ConfigTest:IOptions<ConfigTest>
    {
        public int TotalCount { get; set; }
        public List<Student> Students { get; set; }
        public ConfigTest Value => this;
    }
}
