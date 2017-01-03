using BLL.Models;
using BLL.Models.AboutDB;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public void InsertBitModelByTraditionalWay()
        {
            TraditionalSQLServerDBManager manager = new TraditionalSQLServerDBManager();
            manager.ExcuteSQL(@"INSERT INTO [test2].[dbo].[BitTable] ([IsDelete]) VALUES(1)");
        }

        public List<Area> GetArea(int ParentID)
        {
            string sql =string.Format("select PKID,ParentID,Name from area where ParentID={0}",ParentID);
            return DAL.DbManager<Area>.Instance.QueryBySQL(sql).ToList();
        }

    }
}
