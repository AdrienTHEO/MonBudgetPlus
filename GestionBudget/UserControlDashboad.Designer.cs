namespace GestionBudget
{
    partial class UserControlDashboad
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
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnActualiserObjectif = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutObjectifs = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblSoldeActuel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblDepensesMois = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTotalRevenus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblMoyennePourcent = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel11.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel11
            // 
            this.panel11.AutoScroll = true;
            this.panel11.Controls.Add(this.linkLabel4);
            this.panel11.Controls.Add(this.panel10);
            this.panel11.Location = new System.Drawing.Point(6, 542);
            this.panel11.Margin = new System.Windows.Forms.Padding(2);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(661, 184);
            this.panel11.TabIndex = 5;
            // 
            // panel10
            // 
            this.panel10.Location = new System.Drawing.Point(29, 24);
            this.panel10.Margin = new System.Windows.Forms.Padding(2);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(583, 153);
            this.panel10.TabIndex = 4;
            // 
            // linkLabel4
            // 
            this.linkLabel4.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.LinkColor = System.Drawing.Color.Black;
            this.linkLabel4.Location = new System.Drawing.Point(26, 5);
            this.linkLabel4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(126, 17);
            this.linkLabel4.TabIndex = 8;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Bar d\'avancement";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel11);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Location = new System.Drawing.Point(5, 5);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(684, 728);
            this.panel3.TabIndex = 3;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnActualiserObjectif);
            this.panel2.Location = new System.Drawing.Point(13, 177);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(653, 162);
            this.panel2.TabIndex = 22;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnActualiserObjectif
            // 
            this.btnActualiserObjectif.Location = new System.Drawing.Point(0, 3);
            this.btnActualiserObjectif.Name = "btnActualiserObjectif";
            this.btnActualiserObjectif.Size = new System.Drawing.Size(221, 46);
            this.btnActualiserObjectif.TabIndex = 0;
            this.btnActualiserObjectif.Text = "Ajouter votre Objectif maintenant ";
            this.btnActualiserObjectif.UseVisualStyleBackColor = true;
            this.btnActualiserObjectif.Click += new System.EventHandler(this.btnAjouterObjectif_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutObjectifs);
            this.panel1.Location = new System.Drawing.Point(6, 345);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 180);
            this.panel1.TabIndex = 21;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // flowLayoutObjectifs
            // 
            this.flowLayoutObjectifs.AutoScroll = true;
            this.flowLayoutObjectifs.Location = new System.Drawing.Point(12, 3);
            this.flowLayoutObjectifs.Name = "flowLayoutObjectifs";
            this.flowLayoutObjectifs.Size = new System.Drawing.Size(631, 143);
            this.flowLayoutObjectifs.TabIndex = 0;
            this.flowLayoutObjectifs.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutObjectifs_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(476, 34);
            this.label2.TabIndex = 20;
            this.label2.Text = "Creer Vos objectifs d\'epargnes personnalisé et suivew son avancement \r\nen fonctio" +
    "ns de vos contributions manuelles \r\n";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "Objectif D\'Epargne ";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.SkyBlue;
            this.panel8.Controls.Add(this.lblSoldeActuel);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Location = new System.Drawing.Point(6, 2);
            this.panel8.Margin = new System.Windows.Forms.Padding(2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(134, 71);
            this.panel8.TabIndex = 15;
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
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel5.Controls.Add(this.lblDepensesMois);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Location = new System.Drawing.Point(158, 2);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.panel5.Size = new System.Drawing.Size(141, 71);
            this.panel5.TabIndex = 14;
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
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightBlue;
            this.panel6.Controls.Add(this.lblTotalRevenus);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(322, 2);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(139, 71);
            this.panel6.TabIndex = 16;
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
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SkyBlue;
            this.panel7.Controls.Add(this.lblMoyennePourcent);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Location = new System.Drawing.Point(520, 2);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(159, 71);
            this.panel7.TabIndex = 17;
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
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "📊 % Moyens";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "Actualiserles objectifs\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GestionBudget.Properties.Resources._6152022_general2_13;
            this.pictureBox1.Location = new System.Drawing.Point(372, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 159);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // UserControlDashboad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserControlDashboad";
            this.Size = new System.Drawing.Size(689, 751);
            this.Load += new System.EventHandler(this.UserControlDashboad_Load);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblSoldeActuel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblDepensesMois;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblTotalRevenus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblMoyennePourcent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnActualiserObjectif;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutObjectifs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
