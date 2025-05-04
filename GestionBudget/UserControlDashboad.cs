using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace GestionBudget
{
    public partial class UserControlDashboad : UserControl
    {
        private ClassUtilisateur utilisateur;
        public UserControlDashboad(ClassUtilisateur utilisateur)
        {
            InitializeComponent();
            this.utilisateur = utilisateur;

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void UserControlDashboad_Load(object sender, EventArgs e)
        {
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

                var labelTitre = new Label { Text = titre, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(10, 10) };
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
                btnContribuer.Click += button1_Click;

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


        private void button1_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int idObjectif = (int)btn.Tag;

            using var form = new FormContribuerObjectif(idObjectif , utilisateur);
            if (form.ShowDialog() == DialogResult.OK)
            {
                ChargerObjectifs(); // pour mettre à jour la progression
            }
        }

        private void btnAjouterObjectif_Click(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            ChargerObjectifs();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutObjectifs_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
