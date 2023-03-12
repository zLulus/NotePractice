using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodeLibrary.SqlCommandTest
{
    public class AddParameterArrayToSqlCommandDemo
    {
        public static void Run()
        {
            var ageList = new List<int> { 1, 3, 5, 7, 9, 11 };
            var cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM MyTable WHERE Age IN (@Age)";
            cmd.AddArrayParameters("Age", ageList);

            foreach (SqlParameter parameter in cmd.Parameters)
            {
                Console.WriteLine($"{parameter.ParameterName},{parameter.Value}");
            }
            Console.WriteLine(cmd.CommandText);
        }
    }


}
