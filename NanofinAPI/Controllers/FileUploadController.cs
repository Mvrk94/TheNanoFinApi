using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace NanofinAPI.Controllers
{
    public class FileUploadController : ApiController
    {

        [HttpPost()]
        public string UpLoadFiles()
        {
            int iUploadedCnt = 0;

            string sPath = "";

            sPath = System.Web.Hosting.HostingEnvironment.MapPath("/UploadFiles/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            //check num files:
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " Files Uploaded Successfully";
            }
            else
            {
                return "Upload Failed";
            }

        }

        [HttpPost()]
        public string uploadToNewDirectory(string strDirectory)
        {

            int iUploadedCnt = 0;

            string fileUploadDir = ""; 

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            //if there are actually files to upload, then only create the directory
            if (((hfc.Count) > 0))
            {
                fileUploadDir = System.Web.Hosting.HostingEnvironment.MapPath("/UploadFiles/" + strDirectory + "/");
                if (!System.IO.Directory.Exists(fileUploadDir))
                {
                    System.IO.Directory.CreateDirectory(fileUploadDir);
                }

            }



            //check num files:
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(fileUploadDir + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(fileUploadDir + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " Files Uploaded to new Directory Successfully";
            }
            else
            {
                return "Upload Failed";
            }

        }

        public HttpResponseMessage GetTestFile()
        {
            HttpResponseMessage result = null;
            var localFilePath = HttpContext.Current.Server.MapPath("/UploadFiles/claims/Khaya Cover/12491/MF passport.pdf");

            if (!File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                // Serve the file to the client
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "MF passport.pdf";
            }

            

            return result;

        }

        [HttpGet]
        public List<FileInfo> getFileInfoInDirectory(string filepath)
        {
            var localFilePath = HttpContext.Current.Server.MapPath("/UploadFiles/"+filepath);
            DirectoryInfo directory = new DirectoryInfo(localFilePath);
            var files = directory.GetFiles().ToList();

         
            return files;
        }

        [HttpGet]
        public List<string> getFileNamesInDirectory(string filepath)
        {
            var localFilePath = HttpContext.Current.Server.MapPath("/UploadFiles/" + filepath);
            DirectoryInfo directory = new DirectoryInfo(localFilePath);
            var files = directory.GetFiles().ToList();
            List<string> namesList = new List<string>();

            foreach (FileInfo f in files)
            {  
                namesList.Add(f.Name);
            }

            return namesList;
        }


        [HttpGet]
        public string rootpath()
        {
            String rootPath = HttpContext.Current.Server.MapPath("~");
            return rootPath;
        }

        ////to download files...Content still to fix
        //[HttpGet]
        //public HttpResponseMessage getFile()
        //{
        //    var path = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/letter.pdf"); ;
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        //    result.Content = new StreamContent(stream);
        //    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //    result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    result.Content.Headers.ContentLength = stream.Length;
        //    return result;
        //}





        [HttpDelete]
        public HttpResponseMessage deleteFile(string filePath)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);


            

            var path = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/"); ;
            if (File.Exists(path+filePath))
            {
                File.Delete(path+filePath);
                return result;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            

        }

        //folder path relative to UpoadFiles Directory
        [HttpDelete]
        public HttpResponseMessage deleteFolder(string folderPath)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);


            var path = System.Web.HttpContext.Current.Server.MapPath("/UploadFiles/"); ;
            if (Directory.Exists(path + folderPath))
            {
                Directory.Delete(path + folderPath, true);
                return result;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }



        }
    }
