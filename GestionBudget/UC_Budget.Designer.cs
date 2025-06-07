namespace GestionBudget
{
    partial class UC_Budget
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnAjouterBudget = new System.Windows.Forms.Button();
            this.lblTotalBudget = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelChart = new System.Windows.Forms.Panel();
            this.dataGridViewBudgets = new System.Windows.Forms.DataGridView();
            this.lblBudgetRestant = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBudgets)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 31);
            this.label1.TabIndex = 10;
            this.label1.Text = "Gestion du Budget";
            // 
            // btnAjouterBudget
            // 
            this.btnAjouterBudget.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAjouterBudget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterBudget.Location = new System.Drawing.Point(18, 176);
            this.btnAjouterBudget.Margin = new System.Windows.Forms.Padding(2);
            this.btnAjouterBudget.Name = "btnAjouterBudget";
            this.btnAjouterBudget.Size = new System.Drawing.Size(104, 29);
            this.btnAjouterBudget.TabIndex = 12;
            this.btnAjouterBudget.Text = "Ajouter Budget";
            this.btnAjouterBudget.UseVisualStyleBackColor = false;
            this.btnAjouterBudget.Click += new System.EventHandler(this.btnAjouterBudget_Click);
            // 
            // lblTotalBudget
            // 
            this.lblTotalBudget.AutoSize = true;
            this.lblTotalBudget.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBudget.Location = new System.Drawing.Point(162, 25);
            this.lblTotalBudget.Name = "lblTotalBudget";
            this.lblTotalBudget.Size = new System.Drawing.Size(64, 25);
            this.lblTotalBudget.TabIndex = 13;
            this.lblTotalBudget.Text = "label2";
            this.lblTotalBudget.Click += new System.EventHandler(this.lblTotalBudget_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "Budget Total :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblBudgetRestant);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblTotalBudget);
            this.panel1.Location = new System.Drawing.Point(18, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 126);
            this.panel1.TabIndex = 15;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.dataGridViewBudgets);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.panelChart);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnAjouterBudget);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(686, 399);
            this.panel2.TabIndex = 16;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(336, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Chart Budget restant ";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(547, 18);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 27);
            this.button2.TabIndex = 19;
            this.button2.Text = "Recharger Budget";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(165, 176);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 29);
            this.button1.TabIndex = 17;
            this.button1.Text = "Ajouter Depense";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelChart
            // 
            this.panelChart.Location = new System.Drawing.Point(333, 55);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(319, 150);
            this.panelChart.TabIndex = 16;
            this.panelChart.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChart_Paint);
            // 
            // dataGridViewBudgets
            // 
            this.dataGridViewBudgets.AllowUserToAddRows = false;
            this.dataGridViewBudgets.AllowUserToDeleteRows = false;
            this.dataGridViewBudgets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBudgets.Location = new System.Drawing.Point(18, 225);
            this.dataGridViewBudgets.Name = "dataGridViewBudgets";
            this.dataGridViewBudgets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBudgets.Size = new System.Drawing.Size(634, 150);
            this.dataGridViewBudgets.TabIndex = 20;
            this.dataGridViewBudgets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBudgets_CellContentClick_1);
            // 
            // lblBudgetRestant
            // 
            this.lblBudgetRestant.AutoSize = true;
            this.lblBudgetRestant.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBudgetRestant.Location = new System.Drawing.Point(137, 91);
            this.lblBudgetRestant.Name = "lblBudgetRestant";
            this.lblBudgetRestant.Size = new System.Drawing.Size(66, 25);
            this.lblBudgetRestant.TabIndex = 15;
            this.lblBudgetRestant.Text = "labelR";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 59);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Budget Total Restant :";
            // 
            // UC_Budget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "UC_Budget";
            this.Size = new System.Drawing.Size(689, 378);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBudgets)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAjouterBudget;
        private System.Windows.Forms.Label lblTotalBudget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridViewBudgets;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBudgetRestant;
    }
}
