using FMCG.Utility.RedisCache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace AdminWeb.Controllers
{
    public class ExportExcelChartController : Controller
    {
        // GET: ExportExcelChart
        public ActionResult Index()
        {
            List<Merchandise> list = new List<Merchandise>()
            {
                new Merchandise() {MerchandiseName="启赋商品",MerchandiseNum=200 },
                new Merchandise() {MerchandiseName="金装商品",MerchandiseNum=500 },
                new Merchandise() {MerchandiseName="妈妈奶粉",MerchandiseNum=300 },
                new Merchandise() {MerchandiseName="妈妈奶粉200g",MerchandiseNum=310 },
                new Merchandise() {MerchandiseName="妈妈奶粉150g",MerchandiseNum=280 },
            };
            RedisHelper.Set<List<Merchandise>>("MerchandiseList",list);
            return View(list);
        }
        public ActionResult Export()
        {

            string url=Request["image"];
            List<Merchandise> list = RedisHelper.Get< List<Merchandise>>("MerchandiseList");
            DataTable dt = new DataTable();
            dt.Columns.Add("MerchandiseName");
            dt.Columns.Add("MerchandiseNum");
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                dr["MerchandiseName"] = item.MerchandiseName;
                dr["MerchandiseNum"] = item.MerchandiseNum;
                dt.Rows.Add(dr);
            }
            Dictionary<string, string> dicList = new Dictionary<string, string>();
            dicList.Add("MerchandiseName","商品名称");
            dicList.Add("MerchandiseNum", "商品数量");
            string filepath = Server.MapPath("~/FileContent") + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            NPOIDataAccess.ExcelOut(dt, dicList,filepath);
            //data:image/png;base64,
            string base64 = url.Substring(url.IndexOf("data:image/png;base64,")+ "data:image/png;base64,".Length);
            byte[] arr = Convert.FromBase64String(base64);
            NPOIDataAccess.AddPicture(filepath,arr,false);
            return Redirect("index");

        }
        public class Merchandise
        {
            public string MerchandiseName { get; set; }
            public int MerchandiseNum { get; set; }
        }
    }
}