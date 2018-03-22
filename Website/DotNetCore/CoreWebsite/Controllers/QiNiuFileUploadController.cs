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
using System.Web;
using Newtonsoft.Json;
using CoreWebsite.Models.QiNiuFileUpload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Collections;

namespace CoreWebsite.Controllers
{
    public class QiNiuFileUploadController : Controller
    {
        //引用：
        //Install-Package Newtonsoft.Json
        //Install-Package Qiniu.Shared
        //参考资料：https://developer.qiniu.com/kodo/sdk/4056/c-sdk-v7-2-15#5
        private readonly IHostingEnvironment _hostingEnvironment;
        private string baseUrl = "http://p2skdbze8.bkt.clouddn.com/";
        private string accessKey = "";
        private string secretKey = "";
        private string bucket = "";

        public QiNiuFileUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult UploadFileTest()
        {

            return Json("ok");
        }

        //资料
        //https://www.froala.com/wysiwyg-editor/docs/concepts/image/upload
        //https://www.froala.com/wysiwyg-editor/docs/server/dotnet_core/image-upload

        [HttpPost("UploadFiles")]
        [Produces("application/json")]
        public async Task<IActionResult> FroalaEditorUploadTest(List<IFormFile> files)
        {
            // Get the file from the POST request
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            // Building the path to the uploads directory
            var fileRoute = Path.Combine(webRootPath, "uploads");

            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(fileRoute);
            dir.Directory.Create();

            // Basic validation on mime types and file extension
            string[] imageMimetypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
            string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

            try
            {
                if (Array.IndexOf(imageMimetypes, mimeType) >= 0 && (Array.IndexOf(imageExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    //using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    //{
                    //    await stream.CopyToAsync(writerFileStream);
                    //    writerFileStream.Dispose();
                    //}
                    var key = UploadStream(stream);

                    // Return the file path as json
                    Hashtable imageUrl = new Hashtable();
                    imageUrl.Add("link", $"{baseUrl}/{key}");

                    return Json(imageUrl);
                }
                throw new ArgumentException("The image did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
        }


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
            QiNiuFileUploadResponse response = JsonConvert.DeserializeObject<QiNiuFileUploadResponse>(result.Text);
            return Json(result);
        }

        public IActionResult UploadByStream()
        {
            string localFile = "C:\\Users\\86551\\Desktop\\test.png";
            FileStream fileStream = new FileStream(localFile, FileMode.Open);
            string key= UploadStream(fileStream);
            return Json($"{baseUrl}/{key}");
        }

        private string UploadStream(Stream fileStream)
        {
            Mac mac = new Mac(accessKey, secretKey);
            string bucket = this.bucket;
            string saveKey = Guid.NewGuid() + ".png";
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = bucket;
            putPolicy.SetExpires(3600);
            string jstr = putPolicy.ToJsonString();
            string token = Auth.CreateUploadToken(mac, jstr);
            UploadManager um = new UploadManager();
            HttpResult result = um.UploadStream(fileStream, saveKey, token);
            fileStream.Close();
            QiNiuFileUploadResponse response = JsonConvert.DeserializeObject<QiNiuFileUploadResponse>(result.Text);
            return response.key;
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
            //如果是公用的，直接访问即可：http://xxx.clouddn.com/XXX.png
            //如果是私有的，需要使用token才能访问
            return Json("");
        }
    }
}