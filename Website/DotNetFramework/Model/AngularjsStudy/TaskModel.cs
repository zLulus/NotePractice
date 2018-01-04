using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AngularjsStudy
{
    public enum Status
    {
        todo=0,
        doing=1,
        done=2
    }

    public class TaskModel
    {
        public int Id { get; set; }

        public Status OwnStatus { get; set; }

        public string TaskName { get; set; }
    }
}
