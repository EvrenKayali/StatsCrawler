using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HtmlAgilityPack;
using StatsCrawler.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
            HtmlNode node =  doc.DocumentNode.Descendants("table").Where(tbl => tbl.Attributes["class"].Value == "kulup-tbl").ToList().First();
            HtmlNode node2 = doc.DocumentNode.Descendants("table").Where(tbl => tbl.Attributes["class"].Value == "list-table").ToList().First();



            var a = node2.Descendants().Where(n => n.Name == "tr");

            var alist = node2.Descendants().Where(n => n.Name == "tr").ToList();
            //List<HtmlNode> rows = node2.ChildNodes.Where(n => n.Attributes["Class"].Value.StartsWith("row alt")).ToList();
            //List<HtmlNode> rows = node2.DescendantNodes(1).Where(n => n.Attributes["Class"].Value.StartsWith("row alt")).ToList();


        }


        public static void GetFromWeb2(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            List<HtmlNode> hnodes = doc.DocumentNode.Descendants("table").ToList();
            HtmlNode aa = hnodes.Where(h => h.Attributes["id"] != null && h.Attributes["id"].Value =="tblFixture").ToList().First();
            var a = aa.Descendants().Where(n => n.Name == "tr").ToList();

        }


        public static List<Fixture> GetCurrentSeasonFixturesByTeam()
        {
            string url = "http://arsiv.mackolik.com/Team/Default.aspx?id=3";
            List<Fixture> lstFixtures = new List<Fixture>();
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var rows = doc.DocumentNode.Descendants("table").ToList().Where(tbl => tbl.Attributes["id"] != null && tbl.Attributes["id"].Value == "tblFixture").First()
                .Descendants().Where(node => node.Name == "tr").ToList();

            Fixture fxture;
            foreach (var item in rows)
            {
                if(item.Attributes["class"]!=null && item.Attributes["class"].Value.StartsWith("row alt"))
                {
                    fxture = new Fixture();
                    fxture.SetFixtureProps(item);
                    if (fxture.HomeTeam == null)
                        break;
                    lstFixtures.Add(fxture);
                }


            }

            return lstFixtures;
        }


        private static Fixture SetFixtureProps(this Fixture fxture, HtmlNode item)
        {
            if(item.ChildNodes[9].InnerText.StripHTML() =="v")
            {
                return null;
            }

            try
            {
                try
                {
                    fxture.GameDate = DateTime.ParseExact(item.ChildNodes[1].InnerText, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                catch(FormatException)
                {
                    fxture.GameDate = DateTime.ParseExact(item.ChildNodes[1].InnerText, "d.MM.yyyy", CultureInfo.InvariantCulture);
                }
                fxture.HomeTeam = new Team() { TeamName = item.ChildNodes[5].InnerText.StripHTML() };
                fxture.HomeGoals = Int32.Parse(item.ChildNodes[9].InnerText.StripHTML().Split('-')[0]);
                fxture.AwayGoals = Int32.Parse(item.ChildNodes[9].InnerText.StripHTML().Split('-')[1]);
                fxture.AwayTeam = new Team() { TeamName = item.ChildNodes[13].InnerText.StripHTML() };
                fxture.HomeGoalsFirstHalf = Int32.Parse(item.ChildNodes[17].InnerText.StripHTML().Split('-')[0]);
                fxture.AwayGoalsFirstHalf = Int32.Parse(item.ChildNodes[17].InnerText.StripHTML().Split('-')[1]);
                fxture.IsOver = (item.ChildNodes[21].InnerText.StripHTML() == "Ü" ? true : false);
                fxture.BothSideScored = (item.ChildNodes[23].InnerText.StripHTML() == "-" ? false : true);
                fxture.GameResult = item.ChildNodes[25].InnerText.StripHTML();
            }
            catch(Exception ex)
            {

            }

            return fxture;
        }

        public static void GetAllTeams()
        {
            int i = 1;
            string url = "http://arsiv.mackolik.com/Takim/{0}/";
            Team team;

            while (true)
            {
                var web = new HtmlWeb();
                var doc = web.Load(string.Format(url,i));

                team = new Team();
                team.TeamID = i;


            }
        }
    }
}
