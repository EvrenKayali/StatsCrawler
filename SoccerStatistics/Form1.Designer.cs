namespace SoccerStatistics
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbCompetitions = new System.Windows.Forms.ComboBox();
            this.gridFixture = new System.Windows.Forms.DataGridView();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.cmbStartWeek = new System.Windows.Forms.ComboBox();
            this.cmbEndWeek = new System.Windows.Forms.ComboBox();
            this.grpBetweenWeeks = new System.Windows.Forms.GroupBox();
            this.chxBetweenWeeks = new System.Windows.Forms.CheckBox();
            this.ToBeCalculated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HomeTeam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AwayTeam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridFixture)).BeginInit();
            this.grpBetweenWeeks.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCompetitions
            // 
            this.cmbCompetitions.FormattingEnabled = true;
            this.cmbCompetitions.Location = new System.Drawing.Point(40, 61);
            this.cmbCompetitions.Name = "cmbCompetitions";
            this.cmbCompetitions.Size = new System.Drawing.Size(398, 24);
            this.cmbCompetitions.TabIndex = 1;
            this.cmbCompetitions.SelectedIndexChanged += new System.EventHandler(this.CmbCompetitions_SelectedIndexChanged);
            // 
            // gridFixture
            // 
            this.gridFixture.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.gridFixture.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFixture.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ToBeCalculated,
            this.HomeTeam,
            this.AwayTeam});
            this.gridFixture.GridColor = System.Drawing.Color.MediumSeaGreen;
            this.gridFixture.Location = new System.Drawing.Point(40, 133);
            this.gridFixture.Name = "gridFixture";
            this.gridFixture.RowHeadersWidth = 51;
            this.gridFixture.RowTemplate.Height = 24;
            this.gridFixture.Size = new System.Drawing.Size(524, 304);
            this.gridFixture.TabIndex = 2;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(570, 402);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(168, 35);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "Seçilenleri Hesapla";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // cmbStartWeek
            // 
            this.cmbStartWeek.FormattingEnabled = true;
            this.cmbStartWeek.Location = new System.Drawing.Point(6, 33);
            this.cmbStartWeek.Name = "cmbStartWeek";
            this.cmbStartWeek.Size = new System.Drawing.Size(64, 24);
            this.cmbStartWeek.TabIndex = 4;
            // 
            // cmbEndWeek
            // 
            this.cmbEndWeek.FormattingEnabled = true;
            this.cmbEndWeek.Location = new System.Drawing.Point(6, 63);
            this.cmbEndWeek.Name = "cmbEndWeek";
            this.cmbEndWeek.Size = new System.Drawing.Size(64, 24);
            this.cmbEndWeek.TabIndex = 5;
            // 
            // grpBetweenWeeks
            // 
            this.grpBetweenWeeks.Controls.Add(this.cmbStartWeek);
            this.grpBetweenWeeks.Controls.Add(this.cmbEndWeek);
            this.grpBetweenWeeks.Enabled = false;
            this.grpBetweenWeeks.Location = new System.Drawing.Point(581, 167);
            this.grpBetweenWeeks.Name = "grpBetweenWeeks";
            this.grpBetweenWeeks.Size = new System.Drawing.Size(200, 107);
            this.grpBetweenWeeks.TabIndex = 7;
            this.grpBetweenWeeks.TabStop = false;
            this.grpBetweenWeeks.Text = "Haftalar";
            // 
            // chxBetweenWeeks
            // 
            this.chxBetweenWeeks.AutoSize = true;
            this.chxBetweenWeeks.Location = new System.Drawing.Point(587, 144);
            this.chxBetweenWeeks.Name = "chxBetweenWeeks";
            this.chxBetweenWeeks.Size = new System.Drawing.Size(187, 21);
            this.chxBetweenWeeks.TabIndex = 6;
            this.chxBetweenWeeks.Text = "Hafta Aralığında Hesapla";
            this.chxBetweenWeeks.UseVisualStyleBackColor = true;
            this.chxBetweenWeeks.CheckedChanged += new System.EventHandler(this.ChxBetweenWeeks_CheckedChanged);
            // 
            // ToBeCalculated
            // 
            this.ToBeCalculated.HeaderText = "";
            this.ToBeCalculated.MinimumWidth = 6;
            this.ToBeCalculated.Name = "ToBeCalculated";
            this.ToBeCalculated.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ToBeCalculated.Width = 30;
            // 
            // HomeTeam
            // 
            this.HomeTeam.DataPropertyName = "HomeTeam";
            this.HomeTeam.HeaderText = "Ev Sahibi";
            this.HomeTeam.MinimumWidth = 6;
            this.HomeTeam.Name = "HomeTeam";
            this.HomeTeam.Width = 180;
            // 
            // AwayTeam
            // 
            this.AwayTeam.DataPropertyName = "AwayTeam";
            this.AwayTeam.HeaderText = "Deplasman";
            this.AwayTeam.MinimumWidth = 6;
            this.AwayTeam.Name = "AwayTeam";
            this.AwayTeam.Width = 180;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 742);
            this.Controls.Add(this.chxBetweenWeeks);
            this.Controls.Add(this.grpBetweenWeeks);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.gridFixture);
            this.Controls.Add(this.cmbCompetitions);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridFixture)).EndInit();
            this.grpBetweenWeeks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbCompetitions;
        private System.Windows.Forms.DataGridView gridFixture;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.ComboBox cmbStartWeek;
        private System.Windows.Forms.ComboBox cmbEndWeek;
        private System.Windows.Forms.GroupBox grpBetweenWeeks;
        private System.Windows.Forms.CheckBox chxBetweenWeeks;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ToBeCalculated;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeTeam;
        private System.Windows.Forms.DataGridViewTextBoxColumn AwayTeam;
    }
}

