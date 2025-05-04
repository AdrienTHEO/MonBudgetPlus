namespace GestionBudget
{
    partial class FormModifierBudget
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBudgetDefini = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnregistrer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCategorie = new System.Windows.Forms.Label();
            this.txtDepenseActuelle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtDepenseActuelle);
            this.panel1.Controls.Add(this.lblCategorie);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnEnregistrer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBudgetDefini);
            this.panel1.Location = new System.Drawing.Point(137, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(431, 411);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtBudgetDefini
            // 
            this.txtBudgetDefini.Location = new System.Drawing.Point(221, 151);
            this.txtBudgetDefini.Name = "txtBudgetDefini";
            this.txtBudgetDefini.Size = new System.Drawing.Size(100, 26);
            this.txtBudgetDefini.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "budget_defini :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnEnregistrer
            // 
            this.btnEnregistrer.Location = new System.Drawing.Point(146, 318);
            this.btnEnregistrer.Name = "btnEnregistrer";
            this.btnEnregistrer.Size = new System.Drawing.Size(100, 32);
            this.btnEnregistrer.TabIndex = 2;
            this.btnEnregistrer.Text = "Enregistrer";
            this.btnEnregistrer.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnEnregistrer.UseVisualStyleBackColor = true;
            this.btnEnregistrer.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 34);
            this.label2.TabIndex = 3;
            this.label2.Text = "Modifie Budget";
            // 
            // lblCategorie
            // 
            this.lblCategorie.AutoSize = true;
            this.lblCategorie.Location = new System.Drawing.Point(158, 270);
            this.lblCategorie.Name = "lblCategorie";
            this.lblCategorie.Size = new System.Drawing.Size(51, 20);
            this.lblCategorie.TabIndex = 4;
            this.lblCategorie.Text = "label3";
            this.lblCategorie.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblCategorie.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtDepenseActuelle
            // 
            this.txtDepenseActuelle.Location = new System.Drawing.Point(221, 221);
            this.txtDepenseActuelle.Name = "txtDepenseActuelle";
            this.txtDepenseActuelle.Size = new System.Drawing.Size(100, 26);
            this.txtDepenseActuelle.TabIndex = 5;
            this.txtDepenseActuelle.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Montant dépensé :\r\n";
            // 
            // FormModifierBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "FormModifierBudget";
            this.Text = "FormModifierBudget";
            this.Load += new System.EventHandler(this.FormModifierBudget_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBudgetDefini;
        private System.Windows.Forms.Button btnEnregistrer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCategorie;
        private System.Windows.Forms.TextBox txtDepenseActuelle;
        private System.Windows.Forms.Label label3;
    }
}