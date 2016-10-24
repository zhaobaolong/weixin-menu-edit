using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Zeroes.WeixinMenu.Helper;
using Zeroes.WeixinSDK;

namespace Zeroes.WeixinMenu.Controllers
{
    public class HomeController : Controller
    {
        public static string MenuFilePath = System.Web.HttpContext.Current.Server.MapPath("~/weixin-menu.json");



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Config()
        {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
 
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        ///查询公众号菜单并保存到文件中
        /// </summary>
        /// <returns></returns>
        public ContentResult QueryMenu()
        {
            var token = WeixinHelper.TokenHelper.GetToken(false);
            var json = CustomMenuAPI.Query(token);

            FileHelper.ShareWrite(json.menu.ToString(), MenuFilePath, Encoding.Default, FileMode.OpenOrCreate);
            return new ContentResult() { Content = json.menu.ToString() };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ContentResult GetMenu()
        {
            var content = FileHelper.ShareRead(MenuFilePath, Encoding.Default);
            if (!string.IsNullOrEmpty(content))
                return new ContentResult() { Content = content };

            return new ContentResult() { Content = @"{""button"":[]}" };
        }

        /// <summary>
        /// 保存菜单到文件
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public ContentResult SaveMenu(string button)
        {
            FileHelper.ShareWrite(button, MenuFilePath, Encoding.Default, FileMode.OpenOrCreate);
            return new ContentResult() { Content = "保存成功" };
        }


        /// <summary>
        /// 读取菜单文件发布到公众号
        /// </summary>
        /// <returns></returns>
        public ContentResult CreateMenu()
        {
            if (!System.IO.File.Exists(MenuFilePath))
                return new ContentResult() { Content = "菜单文件不存在,请先创建菜单" };

            var button = FileHelper.ShareRead(MenuFilePath, Encoding.Default);
            var token = WeixinHelper.TokenHelper.GetToken(false);
            CustomMenuAPI.Create(token, button);

            return new ContentResult() { Content = "创建菜单成功" };
        }
    }
}