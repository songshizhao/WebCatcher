using EO.WebBrowser;
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
    public partial class EOBrowser : Form
    {


        //WebView _browser;

        public EventHandler<string> OnLoadFinished;

        public bool IsLoading { get; set; }


        public EOBrowser()
        {
            InitializeComponent();

            //_browser = this.webView1;
            //this.Controls.Add(_browser);

            _browser = new WebView();
            _browser.NeedClientCertificate+= (s, e) => {

                Debug.WriteLine("Need Client Certificate");

            };
            _browser.BeforeSendHeaders += (s,e) => {

                Debug.WriteLine("Before Send Headers");

            };
            _browser.NewWindow += (s, e) => {
                Debug.WriteLine("New Window");
            };
            _browser.CertificateError += (s, e) => {

                Debug.WriteLine("Certificate Error");
            };

        }

       
        public void GetHtmlAfterJs(string url, string encode = "utf-8")
        {
            Debug.WriteLine(url);
            _browser.Url=url;
            _browser.LoadCompleted += _browser_LoadCompleted;
        }

        private void _browser_LoadCompleted(object sender, LoadCompletedEventArgs e)
        {

            IsLoading = false;
            string final_html = _browser.GetHtml();
            
            OnLoadFinished?.Invoke(_browser, final_html);
        }




    }
}
