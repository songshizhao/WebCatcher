using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");

            var data=GetHtml("https://www.9ku.com/");


            Getc(data);


            Console.WriteLine("end");
            Console.Read();
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


        static List<string> Getc(string data)
        {


            Hashtable hashtable = new Hashtable();// 网页中元素对象
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





        //如果请求下来的页面是zip格式
        //Stream ResStream = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
        //Encoding encoding = Encoding.GetEncoding(“utf - 8”);
        //StreamReader streamReader = new StreamReader(ResStream, encoding);


        //internal void GetHtmlAfterJs(string url, string encode)
        //{
        //    //using (WebBrowser myBrowser = new WebBrowser())
        //    //{
        //    //    myBrowser.Url = new Uri(url);
        //    //    myBrowser.DocumentCompleted += MyBrowser_DocumentCompleted;
        //    //}
        //    WebBrowser myBrowser = new WebBrowser();

        //    myBrowser.Url = new Uri(url);
        //    myBrowser.DocumentCompleted += MyBrowser_DocumentCompleted;
        //}

        //private void MyBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    List<HeroLink> _heroLinks = new List<HeroLink>();

        //    var b = sender as WebBrowser;
        //    string final_html = b.Document.Body.InnerHtml;
        //    var Ul_div = b.Document.GetElementById("jSearchHeroDiv");
        //    foreach (HtmlElement li_item in Ul_div.Children)
        //    {
        //        HeroLink heroLink = new HeroLink();
        //        var lnk = li_item.Children[0];
        //        heroLink.DisplayName = lnk.InnerText;
        //        heroLink.LinkUrl = lnk.GetAttribute("href");
        //        foreach (HtmlElement ele in lnk.Children)
        //        {
        //            if (ele.TagName == "IMG")
        //            {
        //                heroLink.ImgSrc = ele.GetAttribute("src");
        //                break;
        //            }

        //        }
        //        hero_count += 1;

        //        Debug.WriteLine(String.Format("列表中获取的第{0}个英雄链接,英雄名字:{1},链接地址:{2}", hero_count, heroLink.DisplayName, heroLink.LinkUrl));


        //        _heroLinks.Add(heroLink);
        //    }


        //    //Parallel.ForEach<HeroLink>(heroLinks,
        //    //    (heroLink) => {
        //    //        LoadPage(heroLink);
        //    //    });

        //    foreach (HeroLink heroLink in _heroLinks)
        //    {
        //        if (heroLink.LinkUrl == "")
        //        {
        //            return;
        //        }
        //        WebBrowser myBrowser = new WebBrowser();
        //        myBrowser.Height = 2000;
        //        myBrowser.Tag = heroLink;
        //        UserAgentHelper.AppendUserAgent("");
        //        myBrowser.ScriptErrorsSuppressed = true;
        //        myBrowser.Url = new Uri(heroLink.LinkUrl);
        //        myBrowser.DocumentCompleted += MyBrowser_DocumentCompleted_Detail;
        //        //myBrowser.Navigate(new Uri(heroLink.LinkUrl), "", null, "Accept-Language:zh-CN,q=0.5\nMozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; rv:11.0) like Gecko");
        //        //myBrowser.Document.Cookie=
        //        //var acceptLanguageHeader = "Accept-Language:zh-CN,q=0.5\nUser-Agent:MyCoustomBrowser";
        //        //myBrowser.Navigate(new Uri(heroLink.LinkUrl), null, null, acceptLanguageHeader);
        //    }

        //    b.Dispose();
        //}


        //int HeroCount = 0;
        //private void MyBrowser_DocumentCompleted_Detail(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    var b = sender as WebBrowser;
        //    var heroLink = (HeroLink)b.Tag;
        //    if (!e.Url.Equals(b.Url))
        //    {
        //        Debug.WriteLine(String.Format("网页进行了重定向,是想要的"));
        //        //return;
        //    }
        //    else
        //    {
        //        Debug.WriteLine(String.Format("网页没有重定向,可能不是想要的"));
        //        //return;
        //    }
        //    var heroDetail = new HeroDetail();
        //    heroDetail.HeroName = b.Document.GetElementById("DATAname").InnerText;
        //    heroDetail.PersonName = b.Document.GetElementById("DATATitle").InnerText;

        //    string text = heroDetail.HeroName;
        //    if (!Regex.IsMatch(text, @"[\u4e00-\u9fa5]"))
        //    {
        //        Debug.WriteLine(String.Format("抓取到了不合格的网页"));
        //        return;
        //    }
        //    else
        //    {
        //        HeroCount += 1;
        //        Debug.WriteLine(String.Format("这是抓取成功的第{0}个英雄", HeroCount));
        //    }

        //    var tagContainer = b.Document.GetElementById("DATAtags");
        //    foreach (HtmlElement span in tagContainer.Children)
        //    {
        //        heroDetail.Types.Add(span.InnerText);
        //    }
        //    var abilityContainer = b.Document.GetElementById("DATAinfo");
        //    string[] data_info_header = new string[] { "物理攻击", "魔法攻击", "防御能力", "上手难度" };
        //    foreach (HtmlElement dd in abilityContainer.Children)
        //    {
        //        if (dd.TagName == "DD")
        //        {
        //            string str_width = dd.InnerHtml;

        //            int y1 = str_width.IndexOf("up up");
        //            int i = Convert.ToInt32(str_width.Substring(y1 + 5, 1)) - 1;
        //            int x1 = str_width.IndexOf(":");
        //            int x2 = str_width.IndexOf("%");
        //            str_width = str_width.Substring(x1 + 1, x2 - x1 - 1);
        //            Debug.WriteLine(heroDetail.HeroName + str_width + b.Url + heroDetail.PersonName);
        //            Ability ability = new Ability();
        //            ability.AbilityName = data_info_header[i];
        //            ability.AbilityValue = Convert.ToDouble(str_width);
        //            heroDetail.Abilitys.Add(ability);
        //        }


        //    }

        //    var skinNAV = b.Document.GetElementById("skinNAV");

        //    foreach (HtmlElement li in skinNAV.Children)
        //    {
        //        Skin skin = new Skin();
        //        skin.SkinName = li.Children[0].GetAttribute("title");
        //        skin.SkinImgUrl = li.Children[0].Children[0].GetAttribute("src").Replace("small", "big"); ;
        //        heroDetail.Skins.Add(skin);
        //        Debug.WriteLine(String.Format("皮肤{0}的图片Url:{1}", skin.SkinName, skin.SkinImgUrl));

        //    }

        //    //var li_skin= b.Document.GetElementById("skinNAV").Children[1].Click();

        //    //背景故事
        //    heroDetail.Story = b.Document.GetElementById("DATAlore").InnerText;
        //    Debug.WriteLine(heroDetail.Story + "???");
        //    //技能图标
        //    var skill_Ul = b.Document.GetElementById("DATAspellsNAV");
        //    foreach (HtmlElement li in skill_Ul.Children)
        //    {
        //        Skill skill = new Skill();
        //        skill.SkillImageUrl = li.Children[0].GetAttribute("src");
        //        heroDetail.Skills.Add(skill);
        //    }
        //    //使用技巧
        //    heroDetail.TipIfUse = b.Document.GetElementById("DATAallytips").InnerText;
        //    heroDetail.TipIfRival = b.Document.GetElementById("DATAenemytips").InnerText;

        //    Debug.WriteLine(tagContainer.InnerHtml);


        //    heroLink.Detail = heroDetail;



        //    heroLinks.Add(heroLink);


        //    if (HeroCount >= hero_count - 1)
        //    {
        //        string OutputXmlString = "";
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            var setting = new XmlWriterSettings()
        //            {
        //                Encoding = new UTF8Encoding(false),
        //                Indent = true,
        //            };

        //            using (XmlWriter writer = XmlWriter.Create(ms, setting))
        //            {
        //                XmlSerializer xmlSearializer = new XmlSerializer(typeof(List<HeroLink>));
        //                xmlSearializer.Serialize(writer, heroLinks);
        //                OutputXmlString = Encoding.UTF8.GetString(ms.ToArray());
        //            }



        //            //gh
        //        }
        //        textBox1.Text = OutputXmlString;
        //        //Debug.WriteLine(OutputXmlString);
        //    }


        //    b.Dispose();

        //}


    }
}
