using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HtmlAgilityPack;

namespace StatsCrawler
{
    public static class BCrawler
    {

        public static void GetFromWeb(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            List<HtmlNode> hnodes = doc.DocumentNode.Descendants("table").ToList();
            var tblNode = doc.DocumentNode.Descendants("table").Select(tbl => tbl.Attributes["id"].Value == "tblFixture");
        }
    }
}
