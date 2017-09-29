using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace WebsiteCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void runTest_Click(object sender, EventArgs e)
        {
            var html = @"https://www.metsales.com/MetropolitanSales/items/Products.aspx?store=&currpage=1&searchby=&lookfor=t88v&searchMethod=Contains";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var nodes = htmlDoc.DocumentNode.SelectNodes("//a[contains(@class, 'ItemNumberLink')]");

            foreach(var item in nodes)
            {
                outputBox.Text += item.InnerHtml + "\r\n";

            }
        }
    }
}
