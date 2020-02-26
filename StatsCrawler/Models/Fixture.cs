using System;
using System.Collections.Generic;
using System.Text;

namespace StatsCrawler.Models
{
    public class Fixture
    {
        public string Season { get; set; }
        public DateTime GameDate { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int HomeGoalsFirstHalf { get; set; }
        public int AwayGoalsFirstHalf { get; set; }
        public bool BothSideScored { get; set; }
        public bool IsOver { get; set; }
        public string GameResult { get; set; }
    }

    
}
