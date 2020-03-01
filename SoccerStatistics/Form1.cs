using ClosedXML.Excel;
using SoccerStatistics.Models;
using StatsCrawler;
using StatsCrawler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoccerStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            //BCrawler.GetAllTeams();
            FillCompetitions();
        }

        void FillWeeklySchedule(int competitionID)
        {
            string currentWeek =  BCrawler.GetCurrentWeek(competitionID);
            Task<List<Fixture>> currentFixtures = Task.Run(async()=> await BCrawler.GetOpenFixtureByCompetition(competitionID, currentWeek));
            DataTable dt = new DataTable();
            dt.Columns.Add("HomeTeam");
            dt.Columns.Add("AwayTeam");
            foreach (var item in currentFixtures.Result)
            {
                dt.Rows.Add(item.HomeTeam.TeamName, item.AwayTeam.TeamName);
            }
            gridFixture.DataSource = dt;

            cmbStartWeek.Items.Clear();
            cmbEndWeek.Items.Clear();
            int maxwWeek = Int32.Parse(currentWeek);
            for (int i = 1; i <= maxwWeek; i++)
            {
                cmbStartWeek.Items.Add(i);
                cmbEndWeek.Items.Add(i);
            }
            cmbStartWeek.SelectedIndex = 0;
            cmbEndWeek.SelectedIndex = maxwWeek - 1;

        }

      
        void FillCompetitions()
        {
            List<Competition> competitions = DataHelper.GetCompetitions();
            cmbCompetitions.DataSource = competitions;
            cmbCompetitions.DisplayMember = "CompetitionName";
            cmbCompetitions.ValueMember = "CompetitionID";            
        }

        private void CmbCompetitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillWeeklySchedule(((Competition)cmbCompetitions.Items[cmbCompetitions.SelectedIndex]).CompetitionID);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            List<List<TeamStats>> results = new List<List<TeamStats>>();
            bool selected = false;
            foreach (DataGridViewRow row in gridFixture.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["ToBeCalculated"].Value);
                if(selected)
                {
                   results.Add(CalculateFixtureStats(row.Cells["HomeTeam"].Value.ToString(), row.Cells["AwayTeam"].Value.ToString()));
                }
            }
        }

        List<TeamStats> CalculateFixtureStats(string homeTeam, string awayTeam)
        {
            //int homeTeamID = DataHelper.GetTeamIDByName(homeTeam);
            List<TeamStats> teamStats = new List<TeamStats>();
            
            List<Fixture> AwayTeamFixtures = BCrawler.GetCurrentSeasonFixturesByTeam(DataHelper.GetTeamIDByName(awayTeam));
            TeamStats AwayStats= new TeamStats();
            AwayStats.OverPercentageSeason = Convert.ToDouble(AwayTeamFixtures.Where(a => a.IsOver).Count()) / AwayTeamFixtures.Count;
            AwayStats.OverPercentageHome = Convert.ToDouble(AwayTeamFixtures.Where(a => a.IsOver && a.HomeTeam.TeamName == awayTeam).Count()) /
                AwayTeamFixtures.Where(a => a.HomeTeam.TeamName == awayTeam).Count();
            AwayStats.OverPercentageAway = Convert.ToDouble(AwayTeamFixtures.Where(a => a.IsOver && a.AwayTeam.TeamName == awayTeam).Count()) /
               AwayTeamFixtures.Where(a => a.AwayTeam.TeamName == awayTeam).Count();

            

            List<Fixture> HomeTeamFixtures = BCrawler.GetCurrentSeasonFixturesByTeam(DataHelper.GetTeamIDByName(homeTeam));

            TeamStats HomeStats = new TeamStats();
            HomeStats.OverPercentageSeason = Convert.ToDouble(HomeTeamFixtures.Where(a => a.IsOver).Count()) / HomeTeamFixtures.Count;
            HomeStats.OverPercentageHome = Convert.ToDouble(HomeTeamFixtures.Where(a => a.IsOver && a.HomeTeam.TeamName == homeTeam).Count()) /
                AwayTeamFixtures.Where(a => a.HomeTeam.TeamName == awayTeam).Count();
            HomeStats.OverPercentageAway = Convert.ToDouble(HomeTeamFixtures.Where(a => a.IsOver && a.HomeTeam.TeamName == awayTeam).Count()) /
               AwayTeamFixtures.Where(a => a.AwayTeam.TeamName == awayTeam).Count();

            //HomeStats.OverPercentageSelectedWeeks = Convert.ToDouble(HomeTeamFixtures.Skip(HomeTeamFixtures.Count() - 3).Where(a => a.IsOver).Count()) / 3;
            HomeStats.OverPercentageSelectedWeeks = Convert.ToDouble(HomeTeamFixtures.Take(Convert.ToInt32(cmbEndWeek.SelectedItem)).
                Skip(Convert.ToInt32(cmbStartWeek.SelectedItem)).Where(a => a.IsOver).Count()) / 3;

            if (grpBetweenWeeks.Enabled)
            {
                List<Fixture> HomeFixturesSelected = HomeTeamFixtures.Skip(Convert.ToInt32(cmbStartWeek.SelectedItem)-1).
                    Take(Convert.ToInt32(cmbEndWeek.SelectedItem)+1 - Convert.ToInt32(cmbStartWeek.SelectedItem)).ToList();
                HomeStats.OverPercentageSelectedWeeks = Convert.ToDouble(HomeFixturesSelected.Where(a => a.IsOver).Count()) / HomeFixturesSelected.Count();

                List<Fixture> AwayFixturesSelected = AwayTeamFixtures.Skip(Convert.ToInt32(cmbStartWeek.SelectedItem)-1).
                    Take(Convert.ToInt32(cmbEndWeek.SelectedItem)+1 - Convert.ToInt32(cmbStartWeek.SelectedItem)).ToList();
                AwayStats.OverPercentageSelectedWeeks = Convert.ToDouble(AwayFixturesSelected.Where(a => a.IsOver).Count()) / AwayFixturesSelected.Count();
            }
            else
            {
                HomeStats.OverPercentageSelectedWeeks = Convert.ToDouble(HomeTeamFixtures.Skip(HomeTeamFixtures.Count() - 3).Where(a => a.IsOver).Count()) / 3;
                AwayStats.OverPercentageSelectedWeeks = Convert.ToDouble(AwayTeamFixtures.Skip(AwayTeamFixtures.Count() - 3).Where(a => a.IsOver).Count()) / 3;
            }
            teamStats.Add(HomeStats);
            teamStats.Add(AwayStats);

            return teamStats;

           

            /*
            DataTable dt = new DataTable();
            dt.Columns.Add("Takım");
            dt.Columns.Add("Sezonluk üst oranı");
            dt.Rows.Add(awayTeam, AwayStats.OverPercentageAway);
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "Stats");
            wb.SaveAs(AppDomain.CurrentDomain.BaseDirectory + "\\" + homeTeam + "_" + awayTeam + ".xlsx");
              */ 
            
            
        }

        private void ChxBetweenWeeks_CheckedChanged(object sender, EventArgs e)
        {
            if (chxBetweenWeeks.Checked)
                grpBetweenWeeks.Enabled = true;
            else
                grpBetweenWeeks.Enabled = false;
        }
    }
}
