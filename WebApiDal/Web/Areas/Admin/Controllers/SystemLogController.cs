using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Admin.ViewModels;
using Web.Controllers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SystemLogController : BaseController
    {
        // GET: Admin/SystemLog
        public ActionResult Index(string logFileName)
        {
            var logPath = Server.MapPath("~") + @"\logs\";

            var fileEntries =
                Directory.GetFiles(logPath)
                    .ToList()
                    .Select(p => new {FileNameId = Path.GetFileName(p), FileName = Path.GetFileName(p)})
                    .OrderByDescending(a => a.FileName)
                    .ToList();
            var e = fileEntries.FirstOrDefault();
            var vm = new SystemLogIndexViewModel();
            vm.LogFileSelectList = logFileName == null
                ? new SelectList(fileEntries, nameof(e.FileNameId), nameof(e.FileName))
                : new SelectList(fileEntries, nameof(e.FileNameId), nameof(e.FileName), logFileName);

            logFileName = logPath + logFileName?.Trim();

            if (System.IO.File.Exists(logFileName))
            {
                vm.LogFileContent = System.IO.File.ReadAllText(logFileName);
            }

            return View(vm);
        }

        public ActionResult DeleteAllLogFiles()
        {
            return View();
        }

        [HttpPost, ActionName("DeleteAllLogFiles")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAllLogFilesConfirmed()
        {

            var logPath = Server.MapPath("~") + @"\logs\";

            var fileEntries =
                Directory.GetFiles(logPath);

            foreach (var fileEntry in fileEntries)
            {
                System.IO.File.Delete(fileEntry);
            }

            return Redirect(nameof(Index));
        }

    }
}