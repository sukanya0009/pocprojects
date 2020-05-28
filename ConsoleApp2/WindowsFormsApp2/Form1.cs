using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Util.Store;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        static string[] Scopes = {
                                    //DocsService.Scope.DocumentsReadonly,
                                    DocsService.Scope.Documents,
                                    DocsService.Scope.Drive,
                                    DocsService.Scope.DriveFile };

        MemoryStream str = new MemoryStream();

        public Form1()
        {
            InitializeComponent();
            InitBrowser();
        }

        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.ReadWrite))
            {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                    ).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }


            //CefSettings settings = new CefSettings();
            //Cef.Initialize(settings);
            //browser = new ChromiumWebBrowser("www.google.com", null);
            //this.Controls.Add(browser);
            //browser.Dock = DockStyle.Fill;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
