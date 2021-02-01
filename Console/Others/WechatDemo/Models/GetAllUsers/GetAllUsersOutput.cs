using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WechatDemo.Models.GetAllUsers
{
    public class GetAllUsersOutput
    {
        public int total { get; set; }
        public int count { get; set; }
        public GetAllUsersData data { get; set; }
        public string next_openid { get; set; }
    }
}
