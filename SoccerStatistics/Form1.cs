﻿using StatsCrawler;
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

        private void BtnTest_Click(object sender, EventArgs e)
        {
            //BCrawler.GetFromWeb("http://arsiv.mackolik.com/Team/Default.aspx?id=3&season=2015/2016");
            //BCrawler.GetFromWeb2("http://arsiv.mackolik.com/Team/Default.aspx?id=3");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BCrawler.GetCurrentSeasonFixturesByTeam();
        }
    }
}
