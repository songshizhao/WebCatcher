namespace WebCatcher
{
    partial class EOBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._browser = new EO.WebBrowser.WebView();
            this.SuspendLayout();
            // 
            // _browser
            // 
            this._browser.InputMsgFilter = null;
            this._browser.ObjectForScripting = null;
            this._browser.Title = null;
            this._browser.Url = "https://www.9ku.com//play/1004197.htm";
            // 
            // EOBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "EOBrowser";
            this.Text = "EOBrowser";
            this.ResumeLayout(false);

        }

        #endregion

        private EO.WebBrowser.WebView _browser;
    }
}