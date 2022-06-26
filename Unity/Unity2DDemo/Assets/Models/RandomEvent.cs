using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Assets.Models
{
    [Table(name: nameof(RandomEvent))]
    public class RandomEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Question { get; set; }
        public int Order { get; set; }
    }
}
