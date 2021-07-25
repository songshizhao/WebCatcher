using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCatcher
{
    
    public class GetHtmlHelper:Form
    {

        public EventHandler<string> OnLoadFinished;

        public bool IsLoading { get; set; }

        public WebBrowser _browser { get; set; }
       
        public GetHtmlHelper()
        {



            
            _browser = new WebBrowser();
            _browser.Width = 1920;
            _browser.Height = 1080;
            _browser.ScriptErrorsSuppressed = true;


            _browser.Visible = false;


        }

        [STAThread]
        public void GetHtmlAfterJs(string url, string encode="utf-8")
        {
            Debug.WriteLine(url);
            _browser.Url = new Uri(url);
            _browser.DocumentCompleted += MyBrowser_DocumentCompleted;
        }
        [STAThread]
        private void MyBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            IsLoading = false;
            string final_html = (sender as WebBrowser).Document.Body.InnerHtml;
            Debug.WriteLine(final_html);
            OnLoadFinished?.Invoke(_browser, final_html);

        }

    }
}
