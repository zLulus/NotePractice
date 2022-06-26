using Assets.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Assets.Models
{
    [Table(name: nameof(UserInfo))]
    public class UserInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public SexEnum Sex { get; set; }
        public DateTime Birthday { get; set; }
    }
}
