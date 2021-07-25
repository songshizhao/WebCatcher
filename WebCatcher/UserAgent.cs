using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCatcher
{
    public class UserAgentHelper
    {
        private static string defaultUserAgent = null;
        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);
        const int URLMON_OPTION_USERAGENT = 0x10000001;
        /// <summary> 
        /// 在默认的UserAgent后面加一部分 
        /// </summary> 
        public static void AppendUserAgent(string appendUserAgent)
        {
            if (string.IsNullOrEmpty(defaultUserAgent))
                defaultUserAgent = GetDefaultUserAgent();
            string ua = defaultUserAgent + ";" + appendUserAgent;
            ChangeUserAgent(ua);
        }
        /// <summary> 
        /// 修改UserAgent 
        /// </summary> 
        public static void ChangeUserAgent(string userAgent)
        {
            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, userAgent, userAgent.Length, 0);
        }
        /// <summary> 
        /// 一个很BT的获取IE默认UserAgent的方法 
        /// </summary> 
        private static string GetDefaultUserAgent()
        {
            WebBrowser wb = new WebBrowser();
            wb.Navigate("about:blank");
            while (wb.IsBusy) Application.DoEvents();
            object window = wb.Document.Window.DomWindow;
            Type wt = window.GetType();
            object navigator = wt.InvokeMember("navigator", BindingFlags.GetProperty,
                null, window, new object[] { });
            Type nt = navigator.GetType();
            object userAgent = nt.InvokeMember("userAgent", BindingFlags.GetProperty,
                null, navigator, new object[] { });
            return userAgent.ToString();
        }
    }
}
