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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelGraphique = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalBudget = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridViewBudgets = new System.Windows.Forms.DataGridView();
            this.btnAfficherBudgets = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.deleteCol = new System.Windows.Forms.PictureBox();
            this.editCol = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.budget = new GestionBudget.budget();
            this.budgetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBudgets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.budget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1156, 100);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DodgerBlue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(322, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(286, 43);
            this.button2.TabIndex = 2;
            this.button2.Text = "Ajouter une depense\r\n";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(728, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(286, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ajouter un budget";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mon Budget";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.panelGraphique);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(0, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1033, 215);
            this.panel2.TabIndex = 1;
            // 
            // panelGraphique
            // 
            this.panelGraphique.BackColor = System.Drawing.SystemColors.Window;
            this.panelGraphique.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGraphique.Location = new System.Drawing.Point(511, 9);
            this.panelGraphique.Name = "panelGraphique";
            this.panelGraphique.Size = new System.Drawing.Size(486, 188);
            this.panelGraphique.TabIndex = 2;
            this.panelGraphique.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblTotalBudget);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Cursor = System.Windows.Forms.Cursors.No;
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(26, 9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(437, 188);
            this.panel4.TabIndex = 1;
            // 
            // lblTotalBudget
            // 
            this.lblTotalBudget.AutoSize = true;
            this.lblTotalBudget.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBudget.Location = new System.Drawing.Point(67, 90);
            this.lblTotalBudget.Name = "lblTotalBudget";
            this.lblTotalBudget.Size = new System.Drawing.Size(73, 32);
            this.lblTotalBudget.TabIndex = 2;
            this.lblTotalBudget.Text = "0fcfa";
            this.lblTotalBudget.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = " Budget Actuelle";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.dataGridViewBudgets);
            this.panel3.Controls.Add(this.btnAfficherBudgets);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.deleteCol);
            this.panel3.Controls.Add(this.editCol);
            this.panel3.Location = new System.Drawing.Point(3, 318);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1140, 242);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // dataGridViewBudgets
            // 
            this.dataGridViewBudgets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBudgets.Location = new System.Drawing.Point(23, 73);
            this.dataGridViewBudgets.Name = "dataGridViewBudgets";
            this.dataGridViewBudgets.RowHeadersWidth = 62;
            this.dataGridViewBudgets.RowTemplate.Height = 28;
            this.dataGridViewBudgets.Size = new System.Drawing.Size(962, 150);
            this.dataGridViewBudgets.TabIndex = 3;
            this.dataGridViewBudgets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBudgets_CellContentClick);
            // 
            // btnAfficherBudgets
            // 
            this.btnAfficherBudgets.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAfficherBudgets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAfficherBudgets.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAfficherBudgets.Location = new System.Drawing.Point(675, 24);
            this.btnAfficherBudgets.Name = "btnAfficherBudgets";
            this.btnAfficherBudgets.Size = new System.Drawing.Size(286, 43);
            this.btnAfficherBudgets.TabIndex = 5;
            this.btnAfficherBudgets.Text = "Affiché les Budget";
            this.btnAfficherBudgets.UseVisualStyleBackColor = false;
            this.btnAfficherBudgets.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(479, 34);
            this.label4.TabIndex = 4;
            this.label4.Text = "Afficher tous les budgets par categories\r\n";
            // 
            // deleteCol
            // 
            this.deleteCol.Image = global::GestionBudget.Properties.Resources.delete;
            this.deleteCol.Location = new System.Drawing.Point(1034, 180);
            this.deleteCol.Name = "deleteCol";
            this.deleteCol.Size = new System.Drawing.Size(54, 34);
            this.deleteCol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.deleteCol.TabIndex = 2;
            this.deleteCol.TabStop = false;
            this.deleteCol.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // editCol
            // 
            this.editCol.Image = global::GestionBudget.Properties.Resources.compose__1_;
            this.editCol.Location = new System.Drawing.Point(1034, 77);
            this.editCol.Name = "editCol";
            this.editCol.Size = new System.Drawing.Size(54, 34);
            this.editCol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.editCol.TabIndex = 1;
            this.editCol.TabStop = false;
            this.editCol.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // budget
            // 
            this.budget.DataSetName = "budget";
            this.budget.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // budgetBindingSource
            // 
            this.budgetBindingSource.DataSource = this.budget;
            this.budgetBindingSource.Position = 0;
            // 
            // UC_Budget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UC_Budget";
            this.Size = new System.Drawing.Size(1033, 633);
            this.Load += new System.EventHandler(this.UC_Budget_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBudgets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.budget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelGraphique;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalBudget;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.BindingSource budgetBindingSource;
        private budget budget;
        private System.Windows.Forms.PictureBox editCol;
        private System.Windows.Forms.PictureBox deleteCol;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAfficherBudgets;
        private System.Windows.Forms.DataGridView dataGridViewBudgets;
    }
}
