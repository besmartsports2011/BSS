using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using ImageResizer;

namespace BSS_WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class FileController : ApiController
    {
        [HttpPost]
        [Route("api/v1/uploadFile")]
        public IHttpActionResult UploadFile()
        {
            var dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 1)
                {
                    var message = string.Format("Multiple files are not allowed to upload");

                    dict.Add("error", message);
                }

                foreach (string file in httpRequest.Files)
                {

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 2; //Size = 2 MB limit

                        IList<string> allowedFileExtensions = new List<string> { ".doc", ".docx", ".pdf" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!allowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            //return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            // return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            var guid = Guid.NewGuid().ToString();
                            var fileName = guid + extension;
                            //  where you want to attach your imageurl

                            //if needed write the code to update the table

                            var filePath = "~/Archive/Files/" + fileName;
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                            return Json($"/api/v1/files/{fileName}/0");

                        }
                    }

                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                //return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                //return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            return Json(" ");
        }

        [HttpPost]
        [Route("api/v1/uploadImage")]
        public IHttpActionResult UploadImage()
        {
            var dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 1)
                {
                    var message = string.Format("Multiple files are not allowed to upload");

                    dict.Add("error", message);
                }

                foreach (string file in httpRequest.Files)
                {

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB limit

                        IList<string> allowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!allowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            //return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            // return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            var guid = Guid.NewGuid().ToString();
                            var imageName = guid + extension;
                            //  where you want to attach your imageurl

                            //if needed write the code to update the table

                            var filePath = "~/Archive/Images/" + imageName;
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(HttpContext.Current.Server.MapPath(filePath));

                            return Json($"/api/v1/images/{imageName}/0/0");


                        }
                    }

                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                //return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                //return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            return Json(" ");
        }

        [HttpGet]
        [Route("api/v1/images/default")]
        public IHttpActionResult GetImage()
        {
            var filePath = "~/Archive/Images/default-user.png";
            var serverPath = HttpContext.Current.Server.MapPath(filePath);

            var fileInfo = new FileInfo(serverPath);
            return !fileInfo.Exists ? (IHttpActionResult)NotFound()
                : new FileResult(fileInfo.FullName);
        }

        [HttpGet]
        [Route("api/v1/images/{imageName}/{width}/{height}")]
        public IHttpActionResult GetImage(string imageName, int width, int height)
        {
            var filePath = "~/Archive/Images/" + imageName;
            var serverPath = HttpContext.Current.Server.MapPath(filePath);

            var fileInfo = new FileInfo(serverPath);
            var memoryStream = ResizeImage(File.ReadAllBytes(fileInfo.FullName), width, height);

            return !fileInfo.Exists ? (IHttpActionResult)NotFound()
                : new FileResult(fileInfo.FullName, memoryStream);
        }


        private static MemoryStream ResizeImage(byte[] downloaded, int width, int height)
        {
            var inputStream = new MemoryStream(downloaded);
            if (width == 0 || height == 0)
                return inputStream;

            var memoryStream = new MemoryStream();

            var settings = $"width={width}&height={height}";
            ImageBuilder.Current.Build(new ImageJob(inputStream, memoryStream, new ResizeSettings(settings), false, true));

            return memoryStream;
        }

        [HttpGet]
        [Route("api/v1/files/{fileName}/{x}")]
        public IHttpActionResult GetFile(string fileName, int x)
        {
            var filePath = "~/Archive/Files/" + fileName;
            var serverPath = HttpContext.Current.Server.MapPath(filePath);

            var fileInfo = new FileInfo(serverPath);

            return !fileInfo.Exists
                ? (IHttpActionResult)NotFound()
                : new FileResult(fileInfo.FullName);
        }
    }

    class FileResult : IHttpActionResult
    {
        private readonly string _filePath;
        private readonly string _contentType;
        private readonly Stream _content;

        public FileResult(string filePath, Stream content = null, string contentType = null)
        {
            if (filePath == null) throw new ArgumentNullException("filePath");

            _filePath = filePath;
            _contentType = contentType;
            _content = content ?? File.OpenRead(_filePath);
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(_content)
            };

            var contentType = _contentType ?? MimeMapping.GetMimeMapping(Path.GetExtension(_filePath));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return Task.FromResult(response);
        }

    }



}
