using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeroes.WeixinSDK.Helpers;

namespace Zeroes.WeixinMenu.Helper
{
    public class WeixinHelper
    {
        public static string Token { private set; get; }
        public static string EncodingAESKey { private set; get; }
        public static string AppID { private set; get; }
        public static string AppSecret { private set; get; }

        public static TokenHelper TokenHelper { private set; get; }

        static WeixinHelper()
        {

            Token = System.Configuration.ConfigurationManager.AppSettings["Token"];
            EncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["EncodingAESKey"];
            AppID = System.Configuration.ConfigurationManager.AppSettings["AppID"];
            AppSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];
            TokenHelper = new TokenHelper(6000, AppID, AppSecret, false);
            TokenHelper.ErrorEvent += TokenHelper_ErrorEvent;
            TokenHelper.Run();
        }

        static void TokenHelper_ErrorEvent(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            throw new Exception("获取微信Toker失败", e.Exception);
        }
    }
}