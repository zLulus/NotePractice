using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebsite.Api.Services
{
    public class TxtLogService: ILogService
    {
        private readonly IHostingEnvironment _appEnvironment;

        public TxtLogService(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public void Log(string content)
        {
            var path = $"{_appEnvironment.WebRootPath}\\Log.txt";
            using(FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                //获得字节数组
                byte[] data = System.Text.Encoding.Default.GetBytes($"记录日志：{content}\n");
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
        }
    }
}
