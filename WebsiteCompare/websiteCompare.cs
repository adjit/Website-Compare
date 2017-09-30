using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebsiteCompare
{
    class websiteCompare
    {
        private String oldWeb = @"https://metsales.com/MetropolitanSales/items/Products.aspx?&lookfor=";
        private String newWeb = @"http://metsales.kecommerce11.net/";

        public int runWebsiteCompare()
        {
            int numErrors = 0;

            return numErrors;
        }

        private void pollSites(String partNumber)
        {
            string oldSite = oldWeb + partNumber;
            string newSite = partNumber + "-" + partNumber;

            var html = @"https://www.metsales.com/MetropolitanSales/items/Products.aspx?store=&currpage=1&searchby=&lookfor=t88v&searchMethod=Contains";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var nodes = htmlDoc.DocumentNode.SelectNodes("//a[contains(@class, 'ItemNumberLink')]");

            foreach (var item in nodes)
            {
               // outputBox.Text += item.InnerHtml + "\r\n";

            }
        }

        private async Task<List<Item>> pollNewSite(String url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            List<Item> items = new List<Item>();

            if(web.StatusCode == HttpStatusCode.OK)
            {
                Item it = new Item();

                HtmlNode itemNumberNode = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'product-details-code')]");
                HtmlNode mfgNumberNode = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'product-ms-code')]");
                HtmlNode listPriceNode = doc.DocumentNode.SelectSingleNode("//small[contains(@class, 'muted')]");
            }

            return items;
        }

        private async Task<List<Item>> pollOldSite(String url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            List<Item> items = new List<Item>();

            if(web.StatusCode == HttpStatusCode.OK)
            {
                HtmlNodeCollection productDivs = doc.DocumentNode.SelectNodes("//div[contains(@class, 'BrowseSearch_MainDiv')]");
                
                for(int i = 0; i < productDivs.Count; i++)
                {
                    Item it = new Item();
                    HtmlNode node = productDivs[i];
                    HtmlNode itemNumberNode = node.SelectSingleNode("//a[contains(@class, 'ItemNumberLink')]");
                    HtmlNode mfgNumberNode = node.SelectSingleNode("//a[contains(@class, 'MfgPartNumText')]");
                    HtmlNode listPriceNode = node.SelectSingleNode("//a[contains(@class, 'ListPriceText')]");

                    it.PartNumber = itemNumberNode.InnerText.Trim();
                    it.ManufactuerersNumber = mfgNumberNode.InnerText.Trim();
                    it.MSRP = Convert.ToDouble(listPriceNode.InnerText.Split('$')[1].Trim());
                    it._Origin = url;

                    items.Add(it);
                }
            }
            return items;
            //Will always return a List<Item> with no items in it if website cannot be polled
        }
    }
}
