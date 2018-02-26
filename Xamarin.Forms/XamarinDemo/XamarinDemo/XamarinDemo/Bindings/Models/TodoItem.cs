using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace XamarinDemo.Bindings.Models
{
    public class TodoItem 
    {
        private string name;
        public string Name
        {
            get
            {
                return $"buy {Count} {Item}";
            }
        }

        public int Count { get; set; }
        public string Item { get; set; }
        public bool Done { get; set; }
    }
}
