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
            this.ToBeCalculated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HomeTeam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AwayTeam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCalculate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridFixture)).BeginInit();
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
            this.gridFixture.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFixture.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ToBeCalculated,
            this.HomeTeam,
            this.AwayTeam});
            this.gridFixture.Location = new System.Drawing.Point(40, 133);
            this.gridFixture.Name = "gridFixture";
            this.gridFixture.RowHeadersWidth = 51;
            this.gridFixture.RowTemplate.Height = 24;
            this.gridFixture.Size = new System.Drawing.Size(637, 371);
            this.gridFixture.TabIndex = 2;
            // 
            // ToBeCalculated
            // 
            this.ToBeCalculated.HeaderText = "Hesaplanacak";
            this.ToBeCalculated.MinimumWidth = 6;
            this.ToBeCalculated.Name = "ToBeCalculated";
            this.ToBeCalculated.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ToBeCalculated.Width = 125;
            // 
            // HomeTeam
            // 
            this.HomeTeam.DataPropertyName = "HomeTeam";
            this.HomeTeam.HeaderText = "Ev Sahibi";
            this.HomeTeam.MinimumWidth = 6;
            this.HomeTeam.Name = "HomeTeam";
            this.HomeTeam.Width = 125;
            // 
            // AwayTeam
            // 
            this.AwayTeam.DataPropertyName = "AwayTeam";
            this.AwayTeam.HeaderText = "Deplasman";
            this.AwayTeam.MinimumWidth = 6;
            this.AwayTeam.Name = "AwayTeam";
            this.AwayTeam.Width = 125;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(509, 534);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(168, 35);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "Seçilenleri Hesapla";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 742);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.gridFixture);
            this.Controls.Add(this.cmbCompetitions);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridFixture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbCompetitions;
        private System.Windows.Forms.DataGridView gridFixture;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ToBeCalculated;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeTeam;
        private System.Windows.Forms.DataGridViewTextBoxColumn AwayTeam;
        private System.Windows.Forms.Button btnCalculate;
    }
}

