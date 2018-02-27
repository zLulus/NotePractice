using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;

namespace CoreWebsite.Controllers
{
    public class QiNiuFileUploadController : Controller
    {
        //引用：
        //Install-Package Newtonsoft.Json
        //Install-Package Qiniu.Shared
        //参考资料：https://developer.qiniu.com/kodo/sdk/4056/c-sdk-v7-2-15#5

        public IActionResult UploadByFile()
        {
            // 生成(上传)凭证时需要使用此Mac
            // 这个示例单独使用了一个Settings类，其中包含AccessKey和SecretKey
            // 实际应用中，请自行设置您的AccessKey和SecretKey
            Mac mac = new Mac("", "");
            //你的OSS的名称
            string bucket = "mysso";

            string saveKey = Guid.NewGuid() + ".png";
            string localFile = "C:\\Users\\86551\\Desktop\\test.png";
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucket;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            //putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            UploadManager um = new UploadManager();
            HttpResult result = um.UploadFile(localFile, saveKey, token);
            return Json(result);
        }

        public IActionResult UploadByStream()
        {
            Mac mac = new Mac("", "");
            string bucket = "mysso";
            string saveKey = Guid.NewGuid() + ".png";
            string localFile = "C:\\Users\\86551\\Desktop\\test.png";
            FileStream fileStream = new FileStream(localFile, FileMode.Open);
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = bucket;
            putPolicy.SetExpires(3600);
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            UploadManager um = new UploadManager();
            HttpResult result = um.UploadStream(fileStream, saveKey, token);
            fileStream.Close();
            return Json(result);
        }

        public IActionResult UploadByByte()
        {
            Mac mac = new Mac("", "");
            string bucket = "mysso";
            string saveKey = Guid.NewGuid() + ".png";
            string localFile = "C:\\Users\\86551\\Desktop\\test.png";
            FileStream fileStream = new FileStream(localFile, FileMode.Open);
            byte[] myByte = new byte[fileStream.Length];
            fileStream.Read(myByte, 0, myByte.Length);
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = bucket;
            putPolicy.SetExpires(3600);
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            UploadManager um = new UploadManager();
            HttpResult result = um.UploadData(myByte, saveKey, token);
            fileStream.Close();
            return Json(result);
        }

        public IActionResult Download()
        {
            //如果是公用的，直接访问即可：http://p2skdbze8.bkt.clouddn.com/XXX.png
            //如果是私有的，需要使用token才能访问
            return Json("");
        }
    }
}