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
            FillCompetitions();
        }

        void FillWeeklySchedule(int competitionID)
        {
            Task<List<Fixture>> currentFixtures = Task.Run(async()=> await BCrawler.GetOpenFixtureByCompetition(competitionID, BCrawler.GetCurrentWeek(1)));
            DataTable dt = new DataTable();
            dt.Columns.Add("HomeTeam");
            dt.Columns.Add("AwayTeam");
            foreach (var item in currentFixtures.Result)
            {
                dt.Rows.Add(item.HomeTeam.TeamName, item.AwayTeam.TeamName);
            }
            gridFixture.DataSource = dt;            
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
            bool selected = false;
            foreach (DataGridViewRow row in gridFixture.Rows)
            {
                selected = Convert.ToBoolean(row.Cells["ToBeCalculated"].Value);
                if(selected)
                {
                    CalculateFixtureStats(row.Cells["HomeTeam"].Value.ToString(), row.Cells["AwayTeam"].Value.ToString());
                }
            }
        }

        void CalculateFixtureStats(string homeTeam, string awayTeam)
        {
            //int homeTeamID = DataHelper.GetTeamIDByName(homeTeam);
            
            List<Fixture> AwayTeamFixtures = BCrawler.GetCurrentSeasonFixturesByTeam(DataHelper.GetTeamIDByName(awayTeam));
            TeamStats AwayStats= new TeamStats();
            AwayStats.OverPercentageSeason = Convert.ToDouble(AwayTeamFixtures.Where(a => a.IsOver).Count()) / AwayTeamFixtures.Count;
            AwayStats.OverPercentageHome = Convert.ToDouble(AwayTeamFixtures.Where(a => a.IsOver && a.HomeTeam.TeamName == awayTeam).Count()) /
                AwayTeamFixtures.Where(a => a.HomeTeam.TeamName == awayTeam).Count();
            AwayStats.OverPercentageAway = Convert.ToDouble(AwayTeamFixtures.Where(a => a.IsOver && a.AwayTeam.TeamName == awayTeam).Count()) /
               AwayTeamFixtures.Where(a => a.AwayTeam.TeamName == awayTeam).Count();
            AwayStats.OverPercentageLast3 = Convert.ToDouble(AwayTeamFixtures.Skip(AwayTeamFixtures.Count() - 3).Where(a => a.IsOver).Count()) / 3;

            MessageBox.Show(awayTeam + "\r\n Sezonluk Üst Oran : " + AwayStats.OverPercentageSeason + "\r\n Evinde Üst Olma Oranı : " + AwayStats.OverPercentageHome + "\r\n " +
                "Son 3 maçında Üst Oranı :" + AwayStats.OverPercentageLast3 + " \r\n Deplasmanda Üst olma Oranı :" + AwayStats.OverPercentageAway);
            
        }
    }
}
