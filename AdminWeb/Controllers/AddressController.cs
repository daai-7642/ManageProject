using Entity;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Utility;
using Utility.Cache;
using ViewModel;

namespace AdminWeb.Controllers
{
    public class AddressController : Controller
    {
        static AddressLogic addressLogic = new AddressLogic();
        // GET: Address
        public ActionResult Index(string url="")
        {
            if(url=="")
            {
                url = "http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2016/index.html";
                //url = "http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2016/11.html";
            }
            var lstImg= GetHtml(url);
            //List<HtmlInfo> list= xh.Descendants("a");
            //List<HtmlInfo> alist= list.ChildDescendants("a");
            string html = HttpHelper.Get(url);
            Log4net.LogHelper.WriteLog("gethtml", html);
            return View(lstImg);
        }
        private static List<string> GetHtml(string url)
        {
            
            List<string> lstImg = new List<string>();
            var obj=CacheHelper.Get("address");
            if (obj==null) 
            {
                CacheHelper.AddPermanent("address", lstImg);
                obj = lstImg;
            }
            string html = HttpHelper.Get(url);
            if (html == "err")
            {
                System.Threading.Thread.Sleep(1000);
                //GetHtml(url);
                html = HttpHelper.Get(url);
                if(html=="err")
                {
                    throw new Exception();
                }
            }
             if(url.IndexOf("Html/Address") > -1)
            {
                url = "http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2016" + url.Substring(url.LastIndexOf("/"));
            }
            Regex reg = new Regex("<tr class='(.*?)'>(.*?)</tr>", RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);
            foreach (Match match in matches)
            {
                string entityCode= url.Substring(url.LastIndexOf('/') + 1, url.Length - url.LastIndexOf('/') - 6).PadRight(12, '0'); 
                Regex hrefReg = new Regex("<a href='(.*?)'>");
                Regex phrefReg = new Regex("<a href='(.*?)'>(.*?)</a>");
                MatchCollection hrefs = phrefReg.Matches(match.Value);
                if (match.Value.IndexOf(EnumEntity.AddressType.provincetr.ToString()) >-1)
                {
                    foreach (Match hrefm in hrefs)
                    {
                        //(obj as List<string>).Add(hrefm.Value);
                        Address_Province address = new Entity.Address_Province();
                        string[] hrefarr = hrefm.Value.Replace("<a href='", "").Replace("'>", ";").Replace("<br/></a>", "").Split(';');
                        address.ProvinceCode = hrefarr[0].Substring(hrefarr[0].LastIndexOf('/') + 1, hrefarr[0].Length - hrefarr[0].LastIndexOf('/') - 6).PadRight(12, '0');
                        address.ProvinceName = hrefarr[1];
                        addressLogic.AddDbProvince(address);
                        if (!string.IsNullOrWhiteSpace(hrefm.Value))
                        {
                            GetHtml(url.Substring(0, url.LastIndexOf('/') + 1) + hrefarr[0].Substring(0,2)+".html");
                            int result = addressLogic.SaveDbAddress();
                            Log4net.LogHelper.WriteLog("地址采集入库", url + ";共计:" + result.ToString());
                        }
                    }
                }
                //else if(match.Value.IndexOf("villagetr") > -1)
                //{

                //    Regex tdReg = new Regex("<td>(.*?)</td>");
                //    MatchCollection hmatchs = tdReg.Matches(match.Value);
                //    string[] hrefarr = new string[2];
                //    hrefarr[0] = hmatchs[0].Value.Substring("<td>".Length,12);
                //    hrefarr[1] = hmatchs[2].Value.Substring("<td>".Length, hmatchs[2].Value.IndexOf("</td>")- "<td>".Length);

                //    Address_Village address = new Address_Village();
                //    address.VillageCode = hrefarr[0];
                //    address.VillageName = hrefarr[1];
                //    address.TownCode = entityCode;
                //    addressLogic.AddDbVillage(address);
                //}
                else
                {
                    if(hrefs.Count!=0)
                    {
                        string type=match.Value.Substring("<tr class='".Length, match.Value.IndexOf("'>") - "<tr class='".Length);
                        string[] hrefarr = new string[2];
                        hrefarr[0] = hrefs[0].Value.Replace("<a href='", "").Replace("'>", ";").Replace("</a>", "").Split(';')[1];
                        hrefarr[1] = hrefs[1].Value.Replace("<a href='", "").Replace("'>", ";").Replace("</a>", "").Split(';')[1];
                        Match hmatch = hrefReg.Match(match.Value);
                        if(type == EnumEntity.AddressType.citytr.ToString())
                        {
                            Address_City address = new Entity.Address_City();
                            address.CityCode = hrefarr[0];
                            address.CityName = hrefarr[1];
                            address.ProvinceCode = entityCode;
                            addressLogic.AddDbCity(address);
                        }else if(type == EnumEntity.AddressType.countytr.ToString())
                        {
                            Address_County address = new Entity.Address_County();
                            address.CountyCode = hrefarr[0];
                            address.CountyName = hrefarr[1];
                            address.CityCode = entityCode;
                            addressLogic.AddDbCounty(address);
                        }else if(type==EnumEntity.AddressType.towntr.ToString())
                        {
                            Address_Town address = new Entity.Address_Town();
                            address.TownCode = hrefarr[0];
                            address.TownName = hrefarr[1];
                            address.CountyCode = entityCode;
                            addressLogic.AddDbTown(address);
                        }

                        if (type == EnumEntity.AddressType.towntr.ToString())
                            {

                        }
                         if (!string.IsNullOrWhiteSpace(hmatch.Value)&& type != EnumEntity.AddressType.towntr.ToString())
                        {
                            GetHtml(url.Substring(0, url.LastIndexOf('/') + 1) + hmatch.Value.Replace("<a href='", "").ToString().Replace("'>", ""));
                        }
                        int result = addressLogic.SaveDbAddress();
                        Log4net.LogHelper.WriteLog("地址采集入库:" + hrefarr[0] + ";" + hrefarr[1], url + ";共计:" + result.ToString());
                    } 
                } 
            }

            return obj as List<string>;
        }

        public ActionResult CheckCount(string url="")
        {
            if(url=="")
            {
                url = "http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2016/index.html";
            }
            int count=GetAddress(url);
            return Content(count.ToString());
        }
        private static int count = 0;
        private static int GetAddress(string url)
        {
            string html = HttpHelper.Get(url);
            if (html == "err")
            {
                System.Threading.Thread.Sleep(1000);
                //GetHtml(url);
                html = HttpHelper.Get(url);
                if (html == "err")
                {
                    throw new Exception();
                }
            }
            Regex reg = new Regex("<tr class='(.*?)'>(.*?)</tr>", RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(html);
            
            foreach (Match match in matches)
            {
                string entityCode = url.Substring(url.LastIndexOf('/') + 1, url.Length - url.LastIndexOf('/') - 6).PadRight(12, '0');
                Regex hrefReg = new Regex("<a href='(.*?)'>");
                Regex phrefReg = new Regex("<a href='(.*?)'>(.*?)</a>");
                MatchCollection hrefs = phrefReg.Matches(match.Value);

                if (match.Value.IndexOf(EnumEntity.AddressType.provincetr.ToString()) > -1)
                {
                    foreach (Match hrefm in hrefs)
                    {
                        //(obj as List<string>).Add(hrefm.Value);
                        AddressBase address = new Entity.AddressBase();
                        string[] hrefarr = hrefm.Value.Replace("<a href='", "").Replace("'>", ";").Replace("<br/></a>", "").Split(';');
                        address.Code = hrefarr[0].Substring(hrefarr[0].LastIndexOf('/') + 1, hrefarr[0].Length - hrefarr[0].LastIndexOf('/') - 6).PadRight(12, '0');
                        address.Text = hrefarr[1];
                        address.Type = "provincetr";
                        addressLogic.AddDbAddress(address);
                        if (!string.IsNullOrWhiteSpace(hrefm.Value))
                        {
                            GetAddress(url.Substring(0, url.LastIndexOf('/') + 1) + hrefarr[0].Substring(0, 2) + ".html");
                        }
                    }
                }
                else
                {
                    if (hrefs.Count != 0)
                    {
                        string type = match.Value.Substring("<tr class='".Length, match.Value.IndexOf("'>") - "<tr class='".Length);
                        string[] hrefarr = new string[2];
                        hrefarr[0] = hrefs[0].Value.Replace("<a href='", "").Replace("'>", ";").Replace("</a>", "").Split(';')[1];
                        hrefarr[1] = hrefs[1].Value.Replace("<a href='", "").Replace("'>", ";").Replace("</a>", "").Split(';')[1];
                        Match hmatch = hrefReg.Match(match.Value);
                        AddressBase address = new Entity.AddressBase();
                        address.Code = hrefarr[0];
                        address.Text = hrefarr[1];
                        address.Type = type;
                        addressLogic.AddDbAddress(address);
                        if (!string.IsNullOrWhiteSpace(hmatch.Value) && type != EnumEntity.AddressType.towntr.ToString())
                        {
                            GetAddress(url.Substring(0, url.LastIndexOf('/') + 1) + hmatch.Value.Replace("<a href='", "").ToString().Replace("'>", ""));
                        }
                    }
                }
            }
            int result = addressLogic.SaveDbAddress();
            count += result;
            Log4net.LogHelper.WriteLog("地址统计:" , url + ";共计:" + result.ToString());

            return count;
        }
    }
}