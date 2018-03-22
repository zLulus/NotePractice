using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreWebsite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections;
using Microsoft.AspNetCore.Hosting;

namespace CoreWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost("UploadFiles")]
        //[Produces("application/json")]
        //public async Task<IActionResult> Post(List<IFormFile> files)
        //{
        //    // Get the file from the POST request
        //    var theFile = HttpContext.Request.Form.Files.GetFile("file");

        //    // Get the server path, wwwroot
        //    string webRootPath = _hostingEnvironment.WebRootPath;

        //    // Building the path to the uploads directory
        //    var fileRoute = Path.Combine(webRootPath, "uploads");

        //    // Get the mime type
        //    var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

        //    // Get File Extension
        //    string extension = System.IO.Path.GetExtension(theFile.FileName);

        //    // Generate Random name.
        //    string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

        //    // Build the full path inclunding the file name
        //    string link = Path.Combine(fileRoute, name);

        //    // Create directory if it does not exist.
        //    FileInfo dir = new FileInfo(fileRoute);
        //    dir.Directory.Create();

        //    // Basic validation on mime types and file extension
        //    string[] imageMimetypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
        //    string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

        //    try
        //    {
        //        if (Array.IndexOf(imageMimetypes, mimeType) >= 0 && (Array.IndexOf(imageExt, extension) >= 0))
        //        {
        //            // Copy contents to memory stream.
        //            Stream stream;
        //            stream = new MemoryStream();
        //            theFile.CopyTo(stream);
        //            stream.Position = 0;
        //            String serverPath = link;

        //            // Save the file
        //            using (FileStream writerFileStream = System.IO.File.Create(serverPath))
        //            {
        //                await stream.CopyToAsync(writerFileStream);
        //                writerFileStream.Dispose();
        //            }

        //            // Return the file path as json
        //            Hashtable imageUrl = new Hashtable();
        //            imageUrl.Add("link", "/uploads/" + name);

        //            return Json(imageUrl);
        //        }
        //        throw new ArgumentException("The image did not pass the validation");
        //    }

        //    catch (ArgumentException ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}
    }
}
