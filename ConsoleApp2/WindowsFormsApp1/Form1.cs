using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser("www.google.com",null);
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }
    }
}
