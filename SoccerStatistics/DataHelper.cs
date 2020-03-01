using ClosedXML.Excel;
using Newtonsoft.Json;
using SoccerStatistics.Models;
using StatsCrawler.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SoccerStatistics
{
    public static class DataHelper
    {
        static string CompetitionsPath = AppDomain.CurrentDomain.BaseDirectory + "\\competitions.json";
        static string TeamsPath = AppDomain.CurrentDomain.BaseDirectory + "\\teams.json";
        static string JsonCompetitions = string.Empty;
        static string JsonTeams = string.Empty;
        public static List<Competition> GetCompetitions()
        {   
            var competitions = JsonConvert.DeserializeObject<List<Competition>>(GetJsonCompetitions());
            return competitions;
        }

        public static int GetTeamIDByName(string teamName)
        {
            return ((Team)GetTeams().Where(t => t.TeamName.Contains(teamName)).First()).TeamID;            
        }


        public static void ExportGameStats(DataTable dt,string fileName)
        {
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "Stats");
            wb.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName + ".xlsx");
        }

        #region private
        static string GetJsonCompetitions()
        {
            JsonCompetitions = (JsonCompetitions == string.Empty ? File.ReadAllText(CompetitionsPath) : JsonCompetitions);
            return JsonCompetitions;           
        }

        static string GetJsonTeams()
        {
            JsonTeams = (JsonTeams == string.Empty ? File.ReadAllText(TeamsPath) : JsonTeams);
            return JsonTeams;
        }

        public static List<Team> GetTeams()
        {
            return JsonConvert.DeserializeObject<List<Team>>(GetJsonTeams());
        }
        #endregion
    }
}
