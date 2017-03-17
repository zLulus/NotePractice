using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace ContosoUniversity.DAL
{
    //The Entity Framework automatically runs the code it finds in a class that derives from DbConfiguration. You can use the DbConfiguration class to do configuration tasks in code that you would otherwise do in the Web.config file.
    //EF自动跑继承了DbConfiguration的类的代码，在此可以执行想要放在 Web.config 里面的配置工作
    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            //log
            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());
        }
    }
}