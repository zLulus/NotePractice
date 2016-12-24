using BLL.Models.AboutDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AboutDBManager
    {
        public List<BitModel> GetBitModel()
        {
            return DAL.DbManager<BitModel>.Instance.QueryBySQL("select PKID,IsDelete from BitTable").ToList();
        }
    }
}
