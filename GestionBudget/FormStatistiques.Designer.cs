namespace GestionBudget
{
    partial class FormStatistique
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
            this.label1 = new System.Windows.Forms.Label();
            this.pieChartBudget = new LiveCharts.WinForms.PieChart();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Budget par categorie avec valeur  restant";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pieChartBudget
            // 
            this.pieChartBudget.Location = new System.Drawing.Point(157, 70);
            this.pieChartBudget.Name = "pieChartBudget";
            this.pieChartBudget.Size = new System.Drawing.Size(400, 400);
            this.pieChartBudget.TabIndex = 2;
            this.pieChartBudget.Text = "pieChart1";
            this.pieChartBudget.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.pieChart1_ChildChanged);
            // 
            // FormStatistique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 499);
            this.Controls.Add(this.pieChartBudget);
            this.Controls.Add(this.label1);
            this.Name = "FormStatistique";
            this.Text = "FormStatistiques";
            this.Load += new System.EventHandler(this.FormStatistiques_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.PieChart pieChartBudget;
    }
}