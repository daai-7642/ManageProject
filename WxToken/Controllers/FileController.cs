using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WxToken.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        /// <summary>
        /// 前台地址（小程序上传）
        /// https://github.com/764297968/WxSmallProject/tree/master/WxSmallProject
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            var files = Request.Files;
            try
            {
                for (int i = 0; i < files.Count; i++)
                {
                    files[i].SaveAs(Server.MapPath("~/Files") + "/" + DateTime.Now.ToString("yyyyMMddHHmmss")+files[i].FileName.Substring(files[i].FileName.LastIndexOf(".")));
                }
                return Json(new { code = 1, data ="成功"},JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code =0, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}