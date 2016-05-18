using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Web.Controllers
{
    [Authorize]
    public class FDemoController : BaseController
    {
        private readonly string _filePath = "~/Content/Uploads";

        // GET: Admin/Files
        public ActionResult Index(int? pageNumber, int? pageSize, string sortOrder)
        {
            IQueryable<FileInfo> files = null;

            try
            {
                var filesPath = Server.MapPath(_filePath);
                Directory.CreateDirectory(filesPath);

                var fileDirectory = new DirectoryInfo(filesPath);
                files = fileDirectory.GetFiles().AsQueryable();
            }
            catch (DirectoryNotFoundException exp)
            {
                throw new Exception("Could not open the directory", exp);
            }
            catch (IOException exp)
            {
                throw new Exception("Failed to access directory", exp);
            }

            //sorting
            ViewBag.SortOrder = sortOrder;
            switch (sortOrder)
            {
                case "_FileName":
                    files = files.OrderBy(e => e.Name);
                    break;
                case "FileName":
                    files = files.OrderByDescending(e => e.Name);
                    break;


                case "_DateTime":
                    files = files.OrderBy(e => e.CreationTime).ThenBy(f => f.Name);
                    break;
                case "DateTime":
                default:
                    files = files.OrderByDescending(e => e.CreationTime).ThenBy(f => f.Name);
                    ViewBag.SortOrder = "DateTime";
                    break;
            }

            var pageNumberFixed = pageNumber ?? 1;
            var pageSizeFixed = pageSize ?? 20;
            var res = files.ToPagedList(pageNumberFixed, pageSizeFixed);


            return View(res);
        }

        public ActionResult DeleteFile(string fileName)
        {
            var fileNameFull = Server.MapPath(_filePath) + "/" + fileName;

            FileInfo fileInfo = new FileInfo(fileNameFull);

            return View(fileInfo);
        }

        [HttpPost, ActionName("DeleteFile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFileConfirmed(string fileName)
        {
            var fileNameFull = Server.MapPath(_filePath) + "/" + fileName;
            System.IO.File.Delete(fileNameFull);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult UpLoadFiles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpLoadFiles(List<HttpPostedFileBase> upload)
        {
            var allowedExtensions = new[]
            {
                ".pdf",
                ".doc", "docx", ".xlsx", ".txt",
                ".jpeg", ".jpg", ".png", ".bmp", ".gif"
            };

            var filePath = Server.MapPath(_filePath);
            Directory.CreateDirectory(filePath);

            foreach (HttpPostedFileBase item in upload)
            {
                var extension = Path.GetExtension(item.FileName);
                if (allowedExtensions.Contains(extension))
                {
                    var path = Path.Combine(filePath, item.FileName);
                        item.SaveAs(path);
                }
            }

            return RedirectToAction(nameof(Index));
        }


    }

}