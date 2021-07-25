using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinApi.User32;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace WebCatcher
{
    class Program
    {
        
        static List<string> _sourceLinkList = new List<string>();
        //private static EOBrowser _webViewForm = new EOBrowser();
        //private static WebViewForm _webViewForm = new WebViewForm();
        [STAThread]
        static  void Main(string[] args)
        {


            Console.WriteLine("start");

            var data=GetHtml("https://www.9ku.com/");

            
            var linkList=GetSongLinks(data);


           



            foreach (var item in linkList)
            {

                string url = "https://www.9ku.com/" + item;


                WebViewForm _webViewForm = new WebViewForm();

                _webViewForm.OnLoadFinished += (s, _html) => {


                    //Console.WriteLine(_html);

                    var doc = new HtmlDocument();
                    doc.LoadHtml(_html);
                    var token = doc.DocumentNode.SelectNodes("//audio");

                    if (token!=null)
                    {
                        foreach (HtmlNode row in token)
                        {
                            var src = row.GetAttributeValue("src", "");
                            if (src != "")
                            {
                                if (_sourceLinkList.Contains(src))
                                {
                                    Console.Write(".");
                                }
                                else
                                {
                                    _sourceLinkList.Add(src);
                                    Console.WriteLine(src);
                                    WebViewForm.IsLoading = false;

                                    _webViewForm.Dispose();
                                    _webViewForm.Close();

                                    
                                }
                                
                                
                            }

                        }
                    }
                };
                _webViewForm.GetHtmlAfterJs(url);
                
                _webViewForm.ShowDialog();
                

                User32Methods.ShowWindow(_webViewForm.Handle, ShowWindowCommands.SW_HIDE);

                do
                {
                    Task.Delay(100);
                }
                while (WebViewForm.IsLoading == true);

            }

            Console.WriteLine("end");
            Console.ReadKey();

        }



        static string GetHtml(string url, string encode="utf-8") {

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //设置访问页面的标头
            request.Method = "get";
            request.Accept = "";
            request.ContentType = "";
            request.UserAgent = "";

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {

                //请求下来的HTML页面
                String data = stream.ReadToEnd();

                return data;
                

            }




            


        }


        static List<string> GetSongLinks(string data)
        {


            //Hashtable hashtable = new Hashtable();// 网页中元素对象
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(data);//解析




            //""[@class='songName']
            var token = htmlDoc.DocumentNode.SelectNodes("//a");

            List<String> li = new List<string>();

            //遍历其中符合条件的数据
            foreach (HtmlNode row in token)
            {
                var link = row.GetAttributeValue("href", "");


                if (link!="")
                {

                    var classname=row.GetAttributeValue("class", "");
                    if (classname == "songName ")
                    {
                        li.Add(link);
                        Console.WriteLine(link);
                        //Console.WriteLine($"{classname}:link");
                    }


                }
               
                

            }

            return li;


        }





    }
}
