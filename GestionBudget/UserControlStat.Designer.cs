using System;

namespace GestionBudget
{
    partial class UserControlStat
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
            this.lblSoldeActuel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblDepensesMois = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTotalRevenus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblMoyennePourcent = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.BtnStatReset = new System.Windows.Forms.Button();
            this.btnStatFiltrer = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpDebut = new System.Windows.Forms.DateTimePicker();
            this.panelBudget = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panelCategorie = new System.Windows.Forms.Panel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.panelGraphique = new System.Windows.Forms.Panel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSoldeActuel
            // 
            this.lblSoldeActuel.AutoSize = true;
            this.lblSoldeActuel.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoldeActuel.Location = new System.Drawing.Point(7, 30);
            this.lblSoldeActuel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSoldeActuel.Name = "lblSoldeActuel";
            this.lblSoldeActuel.Size = new System.Drawing.Size(0, 20);
            this.lblSoldeActuel.TabIndex = 1;
            this.lblSoldeActuel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "💰  Solde actuel";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.SkyBlue;
            this.panel8.Controls.Add(this.lblSoldeActuel);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Location = new System.Drawing.Point(5, 553);
            this.panel8.Margin = new System.Windows.Forms.Padding(2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(134, 71);
            this.panel8.TabIndex = 4;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // lblDepensesMois
            // 
            this.lblDepensesMois.AutoSize = true;
            this.lblDepensesMois.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepensesMois.Location = new System.Drawing.Point(21, 30);
            this.lblDepensesMois.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDepensesMois.Name = "lblDepensesMois";
            this.lblDepensesMois.Size = new System.Drawing.Size(0, 20);
            this.lblDepensesMois.TabIndex = 1;
            this.lblDepensesMois.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-3, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "💸 dépenses du mois\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel5.Controls.Add(this.lblDepensesMois);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Location = new System.Drawing.Point(157, 553);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.panel5.Size = new System.Drawing.Size(141, 71);
            this.panel5.TabIndex = 3;
            // 
            // lblTotalRevenus
            // 
            this.lblTotalRevenus.AutoSize = true;
            this.lblTotalRevenus.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenus.Location = new System.Drawing.Point(25, 30);
            this.lblTotalRevenus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalRevenus.Name = "lblTotalRevenus";
            this.lblTotalRevenus.Size = new System.Drawing.Size(0, 20);
            this.lblTotalRevenus.TabIndex = 1;
            this.lblTotalRevenus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "💼Total revenus";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightBlue;
            this.panel6.Controls.Add(this.lblTotalRevenus);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(321, 553);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(139, 71);
            this.panel6.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SkyBlue;
            this.panel7.Controls.Add(this.lblMoyennePourcent);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Location = new System.Drawing.Point(519, 553);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(163, 71);
            this.panel7.TabIndex = 6;
            // 
            // lblMoyennePourcent
            // 
            this.lblMoyennePourcent.AutoSize = true;
            this.lblMoyennePourcent.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoyennePourcent.Location = new System.Drawing.Point(21, 30);
            this.lblMoyennePourcent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMoyennePourcent.Name = "lblMoyennePourcent";
            this.lblMoyennePourcent.Size = new System.Drawing.Size(0, 20);
            this.lblMoyennePourcent.TabIndex = 1;
            this.lblMoyennePourcent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 6);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "📊 % Moyens";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 31);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mes Statistiques\r\n";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panelBudget);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Location = new System.Drawing.Point(2, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 501);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_1);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.BtnStatReset);
            this.panel4.Controls.Add(this.btnStatFiltrer);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.dtpFin);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.dtpDebut);
            this.panel4.Cursor = System.Windows.Forms.Cursors.No;
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(23, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(660, 111);
            this.panel4.TabIndex = 11;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // BtnStatReset
            // 
            this.BtnStatReset.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnStatReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStatReset.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStatReset.Location = new System.Drawing.Point(530, 79);
            this.BtnStatReset.Margin = new System.Windows.Forms.Padding(2);
            this.BtnStatReset.Name = "BtnStatReset";
            this.BtnStatReset.Size = new System.Drawing.Size(115, 28);
            this.BtnStatReset.TabIndex = 9;
            this.BtnStatReset.Text = "Annuler Filtre";
            this.BtnStatReset.UseVisualStyleBackColor = false;
            this.BtnStatReset.Click += new System.EventHandler(this.BtnStatReset_Click);
            // 
            // btnStatFiltrer
            // 
            this.btnStatFiltrer.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnStatFiltrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatFiltrer.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatFiltrer.Location = new System.Drawing.Point(211, 72);
            this.btnStatFiltrer.Margin = new System.Windows.Forms.Padding(2);
            this.btnStatFiltrer.Name = "btnStatFiltrer";
            this.btnStatFiltrer.Size = new System.Drawing.Size(152, 28);
            this.btnStatFiltrer.TabIndex = 8;
            this.btnStatFiltrer.Text = "Filtrer Transaction";
            this.btnStatFiltrer.UseVisualStyleBackColor = false;
            this.btnStatFiltrer.Click += new System.EventHandler(this.btnStatFiltrer_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(289, 27);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 17);
            this.label11.TabIndex = 5;
            this.label11.Text = "Date fin :";
            // 
            // dtpFin
            // 
            this.dtpFin.Location = new System.Drawing.Point(292, 48);
            this.dtpFin.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(183, 20);
            this.dtpFin.TabIndex = 4;
            this.dtpFin.ValueChanged += new System.EventHandler(this.dtpFin_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(2, 27);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 17);
            this.label12.TabIndex = 3;
            this.label12.Text = "Date debut :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(14, 7);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(219, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Filtrer les transaction par date  :";
            // 
            // dtpDebut
            // 
            this.dtpDebut.Location = new System.Drawing.Point(92, 46);
            this.dtpDebut.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDebut.Name = "dtpDebut";
            this.dtpDebut.Size = new System.Drawing.Size(183, 20);
            this.dtpDebut.TabIndex = 0;
            // 
            // panelBudget
            // 
            this.panelBudget.BackColor = System.Drawing.Color.Transparent;
            this.panelBudget.Location = new System.Drawing.Point(283, 289);
            this.panelBudget.Margin = new System.Windows.Forms.Padding(2);
            this.panelBudget.Name = "panelBudget";
            this.panelBudget.Size = new System.Drawing.Size(354, 200);
            this.panelBudget.TabIndex = 9;
            this.panelBudget.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBudget_Paint);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(14, 289);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(245, 17);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Solde mensuel (revenus - dépenses)";
            // 
            // panelCategorie
            // 
            this.panelCategorie.Location = new System.Drawing.Point(5, 178);
            this.panelCategorie.Margin = new System.Windows.Forms.Padding(2);
            this.panelCategorie.Name = "panelCategorie";
            this.panelCategorie.Size = new System.Drawing.Size(293, 140);
            this.panelCategorie.TabIndex = 5;
            this.panelCategorie.Paint += new System.Windows.Forms.PaintEventHandler(this.chartRevenuDepense_Paint);
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.LinkColor = System.Drawing.Color.Black;
            this.linkLabel2.Location = new System.Drawing.Point(22, 159);
            this.linkLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(156, 17);
            this.linkLabel2.TabIndex = 6;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Depense par categorie";
            // 
            // panelGraphique
            // 
            this.panelGraphique.Location = new System.Drawing.Point(321, 178);
            this.panelGraphique.Margin = new System.Windows.Forms.Padding(2);
            this.panelGraphique.Name = "panelGraphique";
            this.panelGraphique.Size = new System.Drawing.Size(321, 140);
            this.panelGraphique.TabIndex = 6;
            // 
            // linkLabel4
            // 
            this.linkLabel4.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.LinkColor = System.Drawing.Color.Black;
            this.linkLabel4.Location = new System.Drawing.Point(320, 159);
            this.linkLabel4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(296, 17);
            this.linkLabel4.TabIndex = 8;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Évolution mensuelle des revenus & dépenses";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // UserControlStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel4);
            this.Controls.Add(this.panelCategorie);
            this.Controls.Add(this.panelGraphique);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel7);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserControlStat";
            this.Size = new System.Drawing.Size(689, 660);
            this.Load += new System.EventHandler(this.UserControlStat_Load);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        


        






       

        #endregion

        private System.Windows.Forms.Label lblSoldeActuel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblDepensesMois;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTotalRevenus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblMoyennePourcent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelCategorie;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Panel panelGraphique;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panelBudget;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button BtnStatReset;
        private System.Windows.Forms.Button btnStatFiltrer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpDebut;
    }
}
