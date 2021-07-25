using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCatcher
{
    public partial class WebViewForm : Form, IDisposable
    {

        public EventHandler<string> OnLoadFinished;

        public static bool IsLoading { get; set; } = true;
        
        public double TimeWaited { get; set; }
        public WebBrowser _browser { get; set; } = new WebBrowser();

        public WebViewForm()
        {
            InitializeComponent();

            
            _browser.Width = 450;
            _browser.Height = 900;
            UserAgentHelper.AppendUserAgent("");
            _browser.ScriptErrorsSuppressed = true;


            _browser.Visible = false;

            this.Visible = false;

        }

        public new void Dispose()
        {
            _browser.Navigate("");

            _browser.Dispose();

        }

        public void GetHtmlAfterJs(string url, string encode = "utf-8")
        {
            //Debug.WriteLine(url);
            _browser.Url = new Uri(url);
            WebViewForm.IsLoading = true;
            _browser.DocumentCompleted += MyBrowser_DocumentCompleted;
        }
        
        private void MyBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            
            string final_html = (sender as WebBrowser).Document.Body.InnerHtml;

            Debug.WriteLine(_browser.Url.ToString());

            OnLoadFinished?.Invoke(_browser, final_html);


            

        }


    }
}
