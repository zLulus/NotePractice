using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.DeleteFileInUse
{
    public class DeleteFileInUseDemo
    {
        public static void Run()
        {
            string filePath = @"";
            var processes= FileUtil.WhoIsLocking(filePath);
            foreach(var process in processes)
                process.Kill();

            File.Delete(filePath);
        }
    }
}
