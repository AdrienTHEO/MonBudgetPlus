using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;







namespace GestionBudget
{
    public partial class Form3 : Form
    {
        private ClassUtilisateur utilisateur;

        
        public Form3(ClassUtilisateur user)
        {
            InitializeComponent();
            utilisateur = user;
            ChargerPhotoProfil();
        }

        private void ChargerPhotoProfil()
        {
            if (utilisateur.Photo != null)
            {
                using (MemoryStream ms = new MemoryStream(utilisateur.Photo))
                {
                    pictureBoxProfil.Image = System.Drawing.Image.FromStream(ms);
                    pictureBoxProfil.SizeMode = PictureBoxSizeMode.Zoom; // Optionnel : ajuste l’image
                }
            }
            else
            {
                pictureBoxProfil.Image = Properties.Resources.PhotoProfil; // ou null ou une autre image par défaut
            }

            // Tu peux aussi afficher d'autres infos dans des labels
            lblNom.Text = utilisateur.NomComplet;
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            string message = $"🎉 Bienvenue {utilisateur.NomComplet } dans la plateforme de gestion de budget étudiante !";
            var popup = new FormPopup(message);
            popup.Show();

            using (var conn = new OdbcConnection("DSN=PostgreLocal;"))
            {
                conn.Open();
                string insertQuery = "INSERT INTO notification (utilisateur_id, message) VALUES (?, ?)";
                using (var cmd = new OdbcCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                    cmd.Parameters.AddWithValue("@message", message);
                    cmd.ExecuteNonQuery();
                }
            }
            ChargerStatistiques();
            MettreAJourBadgeNotification(utilisateur.Id);
            ChargerObjectifs();

        }
        private void ChargerObjectifs()
        {
            flowLayoutObjectifs.Controls.Clear();

            using var conn = new OdbcConnection("DSN=PostgreLocal;");
            conn.Open();

            string query = "SELECT id, titre, description, montant_objectif, montant_actuel FROM objectif WHERE utilisateur_id = ?";
            using var cmd = new OdbcCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", utilisateur.Id);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string titre = reader.GetString(1);
                string desc = reader.GetString(2);
                decimal objectif = reader.GetDecimal(3);
                decimal actuel = reader.GetDecimal(4);

                // Crée une "carte"
                var panel = new Panel
                {
                    Width = 400,
                    Height = 130,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                var labelTitre = new Label { Text = titre, Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(10, 10) };
                var labelProgress = new Label { Text = $"{actuel} / {objectif} FCFA", Location = new Point(10, 40) };

                var progressBar = new ProgressBar
                {
                    Minimum = 0,
                    Maximum = (int)objectif,
                    Value = (int)Math.Min(actuel, objectif),
                    Width = 250,
                    Location = new Point(10, 65)
                };

                var btnContribuer = new Button
                {
                    Text = "Contribuer",
                    Location = new Point(270, 60),
                    Tag = id
                };
                btnContribuer.Click += button_Click;

                var btnSupprimer = new Button
                {
                    Text = "🗑 Supprimer",
                    BackColor = Color.IndianRed,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(270, 20),
                    Width = 100,
                    Tag = id
                };
                btnSupprimer.Click += btnSupprimer_Click;

                panel.Controls.Add(labelTitre);
                panel.Controls.Add(labelProgress);
                panel.Controls.Add(progressBar);
                panel.Controls.Add(btnContribuer);
                panel.Controls.Add(btnSupprimer);

                flowLayoutObjectifs.Controls.Add(panel);
            }
        }


        private void button_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int idObjectif = (int)btn.Tag;

            using var form = new FormContribuerObjectif(idObjectif , utilisateur);
            if (form.ShowDialog() == DialogResult.OK)
            {
                ChargerObjectifs(); // pour mettre à jour la progression
            }
        }

        private void ChargerStatistiques()
        {
            string connectionString = "DSN=PostgreLocal;";
            using var conn = new OdbcConnection(connectionString);
            conn.Open();

            decimal totalDepenses = 0;
            decimal totalRevenus = 0;
            decimal depensesMois = 0;
            double moyennePourcentage = 0;

            // 1. Total Dépenses
            using (var cmd = new OdbcCommand("SELECT SUM(montant) FROM transaction WHERE type = 'depense' AND utilisateur_id = ?", conn))
            {
                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value) totalDepenses = Convert.ToDecimal(result);
            }

            // 2. Total Revenus (Budget défini)
            using (var cmd = new OdbcCommand("SELECT SUM(budget_defini) FROM budget WHERE utilisateur_id = ?", conn))
            {
                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value) totalRevenus = Convert.ToDecimal(result);
            }

            // 3. Dépenses du mois courant
            using (var cmd = new OdbcCommand(@"
        SELECT SUM(montant)
        FROM transaction
        WHERE type = 'depense'
          AND EXTRACT(MONTH FROM date_transaction) = ?
          AND EXTRACT(YEAR FROM date_transaction) = ?
          AND utilisateur_id = ?", conn))
            {
                cmd.Parameters.AddWithValue("@mois", DateTime.Now.Month);
                cmd.Parameters.AddWithValue("@annee", DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                var result = cmd.ExecuteScalar();
                if (result != DBNull.Value) depensesMois = Convert.ToDecimal(result);
            }

            // 4. Pourcentage moyen d’utilisation des budgets
            using (var cmd = new OdbcCommand(@"
        SELECT b.budget_defini, COALESCE(SUM(t.montant), 0) as total_depense
        FROM budget b
        LEFT JOIN transaction t
          ON b.utilisateur_id = t.utilisateur_id AND b.categorie = t.categorie AND t.type = 'depense'
        WHERE b.utilisateur_id = ?
        GROUP BY b.budget_defini;", conn))
            {
                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                using var reader = cmd.ExecuteReader();

                double totalPourcentage = 0;
                int nbCategories = 0;

                while (reader.Read())
                {
                    decimal budget = reader.GetDecimal(0);
                    decimal depense = reader.GetDecimal(1);

                    if (budget > 0)
                    {
                        totalPourcentage += (double)(depense / budget) * 100;
                        nbCategories++;
                    }
                }

                moyennePourcentage = nbCategories > 0 ? totalPourcentage / nbCategories : 0;
            }

            // 5. Affectation aux labels
            lblSoldeActuel.Text = $"{(totalRevenus - totalDepenses):C2}";
            lblTotalRevenus.Text = $"{totalRevenus:C2}";
            lblDepensesMois.Text = $"{depensesMois:C2}";
            lblMoyennePourcent.Text = $"{moyennePourcentage:F1}%";
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel10_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void MettreAJourBadgeNotification(int utilisateurId)
        {
            using (var conn = new OdbcConnection("DSN=PostgreLocal;"))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM notification WHERE utilisateur_id = ?";

                using var cmd = new OdbcCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", utilisateurId);

                int nbNotif = Convert.ToInt32(cmd.ExecuteScalar());

                // Trouve le label badge
                var badge = this.Controls.Find("lblNotifCount", true).FirstOrDefault() as Label;
                if (badge != null)
                {
                    if (nbNotif > 0)
                    {
                        badge.Text = nbNotif.ToString();
                        badge.Visible = true;
                    }
                    else
                    {
                        badge.Visible = false;
                    }
                }
            }
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UserControlNotif budgetPage = new UserControlNotif(utilisateur);
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);


            // Cache le badge après lecture
            var badge = this.Controls.Find("lblNotifCount", true).FirstOrDefault() as Label;
            if (badge != null)
                badge.Visible = false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UC_Budget budgetPage = new UC_Budget(utilisateur);
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   // Ouvre le formulaire d’inscription
            this.Hide();              // Optionnel : cache le formulaire actuel
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void chart1_Click_2(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UserControlDashboad budgetPage = new UserControlDashboad(utilisateur);
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UserControlTransactions budgetPage = new UserControlTransactions(utilisateur);
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            
             panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UserControlCategorie budgetPage = new UserControlCategorie();
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UserControlStat budgetPage = new UserControlStat(utilisateur);
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UserControlNotif budgetPage = new UserControlNotif(utilisateur);
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            panel3.Controls.Clear(); // Nettoyer l'ancien contenu

            UserControlSetting budgetPage = new UserControlSetting();
            budgetPage.Dock = DockStyle.Fill;

            panel3.Controls.Add(budgetPage);

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FormProfil formProfil = new FormProfil(utilisateur);
            formProfil.ShowDialog(); // ou .Show(); selon ce que tu préfères
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void deconnexionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnActualiserObjectif_Click(object sender, EventArgs e)
        {
            FormObjectifs formAjout = new FormObjectifs(utilisateur);
            var result = formAjout.ShowDialog(); // Affiche en modal

            if (result == DialogResult.OK)
            {
                // Recharger la liste des objectifs si besoin
                ChargerObjectifs(); // À adapter avec ta méthode réelle
            }
        }



        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int idObjectif = (int)btn.Tag;

            DialogResult confirm = MessageBox.Show(
                "Voulez-vous vraiment supprimer cet objectif ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                SupprimerObjectif(idObjectif);
                ChargerObjectifs(); // Recharge la liste après suppression
            }
        }


        private void SupprimerObjectif(int id)
        {
            using var conn = new OdbcConnection("DSN=PostgreLocal;");
            conn.Open();

            string delete = "DELETE FROM objectif WHERE id = ?";
            using var cmd = new OdbcCommand(delete, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private void flowLayoutObjectifs_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ChargerObjectifs();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExporterPDF_Click(object sender, EventArgs e)
        {
            var dtTransactions = GetTransactionsDuMois();

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF File|*.pdf",
                FileName = "Rapport_Mensuel.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using var doc = new iTextSharp.text.Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, fs);
                doc.Open();

                doc.Add(new iTextSharp.text.Paragraph("Rapport Transactions du Mois"));
                doc.Add(new iTextSharp.text.Paragraph(" "));

                PdfPTable table = new PdfPTable(dtTransactions.Columns.Count);
                foreach (DataColumn col in dtTransactions.Columns)
                {
                    table.AddCell(new PdfPCell(new Phrase(col.ColumnName)));
                }

                foreach (DataRow row in dtTransactions.Rows)
                {
                    foreach (var cell in row.ItemArray)
                        table.AddCell(cell.ToString());
                }

                doc.Add(table);
                doc.Close();

                MessageBox.Show("Export PDF terminé !");
            }
        }

        private DataTable GetTransactionsDuMois()
        {
            DataTable dt = new DataTable();

            using var conn = new OdbcConnection("DSN=PostgreLocal;");
            conn.Open();

            string query = @"
        SELECT date_transaction, montant, categorie, type
        FROM transaction
        WHERE utilisateur_id = ? AND EXTRACT(MONTH FROM date_transaction) = EXTRACT(MONTH FROM CURRENT_DATE)
        AND EXTRACT(YEAR FROM date_transaction) = EXTRACT(YEAR FROM CURRENT_DATE)";

            using var cmd = new OdbcCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", utilisateur.Id);

            using var adapter = new OdbcDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        private void btnExporterExcel_Click(object sender, EventArgs e)
        {
            var dtTransactions = GetTransactionsDuMois();

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = "Rapport_Mensuel.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var wb = new ClosedXML.Excel.XLWorkbook();
                wb.Worksheets.Add(dtTransactions, "Transactions du mois");

                // Tu peux ajouter d'autres tables ici
                // wb.Worksheets.Add(GetBudgets(), "Budgets");

                wb.SaveAs(sfd.FileName);
                MessageBox.Show("Export Excel terminé !");

            }
        }
    }


}
