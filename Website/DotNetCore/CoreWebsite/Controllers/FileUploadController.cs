using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 上传图片
        /// nz-upload nzName="image"与后端方法的IFormFile[] image参数对应
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [RequestFormSizeLimit(valueCountLimit: 2147483647)]
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile[] image, string fileName, Guid name)
        {
            if (image == null || image.Count() == 0)
            {
                throw new Exception("没有检测到上传的图片!");
            }
            if (image.Count() > 1)
            {
                throw new Exception("一次只能上传一张图片!");
            }
            string folderName = "Resources";
            var imageUrl = "";
            foreach (var formFile in image)
            {
                if (formFile.Length > 0)
                {
                    //formData.append('','',item.file.name); 第三个参数=formFile.FileName
                    string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    name = name == Guid.Empty ? Guid.NewGuid() : name;
                    string newName = name + fileExt; //新的文件名
                    var fileDire = $"{_hostingEnvironment.WebRootPath}/{folderName}/";
                    if (!Directory.Exists(fileDire))
                    {
                        Directory.CreateDirectory(fileDire);
                    }

                    var filePath = fileDire + newName;

                    //这里可以根据业务需求，改为上传至云服务商
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    //返回完整Url
                    var apiBaseUrl = "";
                    imageUrl = $"{apiBaseUrl}/{folderName}/{newName}";
                }
            }
            //imageUrl与前端callback方法的.imageUrl对应
            return Ok(new { imageUrl });
        }
    }
}