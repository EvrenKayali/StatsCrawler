using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HtmlAgilityPack;
using StatsCrawler.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;


namespace StatsCrawler
{
    public static class BCrawler
    {   

        public static async  Task<List<Fixture>> GetOpenFixtureByCompetition(int competitionID,string currentWeek)
        {
            List<Fixture> lstFixtures = new List<Fixture>();
            string url = string.Format("http://arsiv.mackolik.com/Puan-Durumu/{0}/",competitionID.ToString());
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var client = new HttpClient();
            var reqId =doc.GetElementbyId("cboSeason").ChildNodes[1].Attributes[0].Value;
            string ajaxRequestUrl = string.Format("http://arsiv.mackolik.com/AjaxHandlers/FixtureHandler.aspx?command=getMatches&id={0}&week={1}", reqId, currentWeek);

            var message = new HttpRequestMessage(HttpMethod.Get, ajaxRequestUrl);
            message.Headers.Referrer = new Uri(url);
            var result = await client.SendAsync(message);
            var data = await result.Content.ReadAsStringAsync();
            var rows = JsonConvert.DeserializeObject<object[][]>(data);
            Fixture fixture;
            foreach (var item in rows)
            {
                fixture = new Fixture();
                fixture.HomeTeam = new Team { TeamName = item[4].ToString() };
                fixture.AwayTeam = new Team { TeamName = item[6].ToString() };
                lstFixtures.Add(fixture);
            }
            return  lstFixtures;

        }

        public static string GetCurrentWeek(int competitionID)
        {
            string url = string.Format("http://arsiv.mackolik.com/Puan-Durumu/{0}/", competitionID.ToString());
            var web = new HtmlWeb();
            var doc = web.Load(url);
            HtmlNode script = doc.DocumentNode.Descendants("script").Where(s => s.InnerText.Contains("currentWeek")).First();
            var week = script.InnerHtml.Substring(script.InnerHtml.IndexOf("currentWeek") + 14, 3);
            return week.Substring(0, week.IndexOf(';'));
        }
        public static List<Fixture> GetCurrentSeasonFixturesByTeam(int teamID)
        {
            string url = "http://arsiv.mackolik.com/Team/Default.aspx?id="+teamID;
            List<Fixture> lstFixtures = new List<Fixture>();
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var rows = doc.GetElementbyId("tblFixture").Descendants().Where(node => node.Name == "tr").ToList();

            //lstFixtures = doc.GetElementbyId("tblFixture")
            //   .Descendants()
            //   .Where(node => node.Name == "tr" && node.Attributes["class"].Value.StartsWith("row alt"))
            //   .Select(i => SetFixtureProps(i)).ToList();


            lstFixtures = doc.DocumentNode.SelectNodes(@"//table[@id='tblFixture']/tr[@class='competition'][2]/
                                    preceding-sibling::tr[contains(@class, 'row alt')]").Select(i => SetFixtureProps(i)).ToList(); 

            return lstFixtures.Take(lstFixtures.FindIndex(f=>f==null)).ToList();
            //return lstFixtures.Where(f => f != null && f.GameDate < DateTime.Now).ToList();

        }


        private static Fixture SetFixtureProps(HtmlNode item)
        {
            var fxture = new Fixture();
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
                Logger logger = new Logger();
                logger.InsertErrorLog("SetFixtureProps", ex.Message, Logger.EnLogType.Error);
            }

            return fxture;
        }

        public static void GetAllTeams()
        {
            int i = 1;
            int noRecordsCount = 0;
            string url = "http://arsiv.mackolik.com/Takim/{0}/";
            Team team;
            List<Team> lstTeams = new List<Team>();
            while (true)
            {
                var web = new HtmlWeb();
                var doc = web.Load(string.Format(url,i));

                if (doc.DocumentNode.ChildNodes.Count == 0)
                {
                    noRecordsCount++;
                    i++;
                    if (noRecordsCount == 50)
                        break;
                    continue;
                }
                noRecordsCount = 0;   

                team = new Team();
                team.TeamID = i;
                team.TeamName = doc.DocumentNode.Descendants("h1").First().InnerText.Trim();
                team.Competition = doc.DocumentNode.Descendants("a").ToList()[3].InnerText.StripHTML();
                lstTeams.Add(team);
                i++;
            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\teams.json", JsonConvert.SerializeObject(lstTeams));
            //SerializeObject(lstTeams, AppDomain.CurrentDomain.BaseDirectory+"\\teams.xml");            
        }

        public static void GetAllCompetitions()
        {
            int i = 0;
            int noRecordsCount = 0;
            string url = "http://arsiv.mackolik.com/Puan-Durumu/{0}/";
            Competition competition;
            List<Competition> lstCompetitions = new List<Competition>();
            while (true)
            {
                i++;
                var web = new HtmlWeb();
                var doc = web.Load(string.Format(url, i));
                if (doc.DocumentNode.ChildNodes.Count == 0)
                {
                    noRecordsCount++;                   
                    if (noRecordsCount == 50)
                        break;
                    continue;
                }
                noRecordsCount = 0;

                competition = new Competition();
                if (doc.DocumentNode.Descendants("h1").Count() > 0)
                {
                    competition.CompetitionID = i;
                    competition.CompetitionName = doc.DocumentNode.Descendants("h1").First().InnerText.Trim();
                }
                else
                    continue;

                lstCompetitions.Add(competition);
                
                if (i >= 120) //120'den sonra geçmiş avrupa-dünya şampiyonaları veya play-off'lar gibi competition'lara geçiyor
                    break;

            }
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\competitions.json", JsonConvert.SerializeObject(lstCompetitions));
            //SerializeObject(lstCompetitions, AppDomain.CurrentDomain.BaseDirectory + "\\competitions.xml");

        }



        static void SerializeObjectToXml<T>(this List<T> list, string fileName)
        {   
            var serializer = new XmlSerializer(typeof(List<T>));
            using (var stream = File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, list);
            }
        }
    }
}
