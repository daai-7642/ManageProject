using FMCG.Utility.RedisCache;
using Log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;
using Utility;

namespace AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Gen(string url,string type)
        {
            //string url = Request.Url.AbsoluteUri;


            string contentType = "application/pdf";
            string ext = ".pdf";
            string folder = "Pdfs";
            string genExe = "wkhtmltopdf.exe ";

            if ("image".Equals(type, StringComparison.InvariantCultureIgnoreCase))
            {
                contentType = "image/jpeg";
                ext = ".jpg";
                folder = "Images";
                genExe = "wkhtmltoimage.exe ";
            }

            var rootUrl = Server.MapPath("/");

            var file = rootUrl + @"wkhtmltoX\" + genExe;

            string fName = Guid.NewGuid().ToString();

            string flileName = folder + "/" + fName + ext;

            try
            {
                List<string> list = new List<string>();
                list.Add("d:");
                list.Add("cd " + Path.GetDirectoryName(file));
                list.Add(genExe + string.Format(" {0} {1}", url, flileName));
               string result= CmdHandle.ExeCommand(list.ToArray());
                return Content(result);
            }
            catch (Exception ex)
            {
                ErrorLogHelper.WriteLog(ex);
                return Content("生成失败. 请多试几次...");
            }

            if (System.IO.File.Exists(flileName))
            {
                var f = new FileStreamResult(new FileStream(flileName, FileMode.Open), contentType);
                f.FileDownloadName = fName + ext;

                return f;
            }

            return Content("生成失败. 请多试几次...");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ClearCache()
        {
            RedisHelper.FlushAll();
            return Content("<script>alert('清除缓存')</script>");
        }
        public ActionResult Ajax(string url, string postDataStr)
        {
            return Content(Utility.HttpHelper.Post(url, postDataStr));
        }
        public ActionResult Log()
        {
            Log4net.LogHelper.WriteLog("测试Log", "测试" + DateTime.Now.ToString());
            return Content("ok");
        }
    }
}