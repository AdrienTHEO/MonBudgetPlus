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
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;

namespace GestionBudget
{
    public partial class UC_Budget : UserControl
    {
        private ClassUtilisateur utilisateur;
        public UC_Budget(ClassUtilisateur user)
        {
            InitializeComponent();
            this.utilisateur = user;
            ChargerBudgets();

            // Ajouter la colonne Modifier
            DataGridViewButtonColumn btnModifier = new DataGridViewButtonColumn();
            btnModifier.HeaderText = "Modifier";
            btnModifier.Text = "🖊️ Modifier";
            btnModifier.UseColumnTextForButtonValue = true;
            dataGridViewBudgets.Columns.Add(btnModifier);

            // Ajouter la colonne Supprimer
            DataGridViewButtonColumn btnSupprimer = new DataGridViewButtonColumn();
            btnSupprimer.HeaderText = "Supprimer";
            btnSupprimer.Text = "🗑️ Supprimer";
            btnSupprimer.UseColumnTextForButtonValue = true;
            dataGridViewBudgets.Columns.Add(btnSupprimer);




            CalculerTotalBudget();
        }

        private void ChargerBudgets()
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    b.categorie,
                    b.budget_defini,
                    COALESCE(SUM(d.montant), 0) AS montant_depense,
                    (b.budget_defini - COALESCE(SUM(d.montant), 0)) AS solde_restant,
                    CASE 
                        WHEN b.budget_defini > 0 
                        THEN ROUND((COALESCE(SUM(d.montant), 0) / b.budget_defini) * 100, 2)
                        ELSE 0 
                    END AS pourcentage_utilise
                FROM budget b
                LEFT JOIN depense d ON b.id = d.budget_id
                WHERE b.utilisateur_id = 1
                GROUP BY b.id, b.categorie, b.budget_defini";

                    using (OdbcCommand command = new OdbcCommand(query, connection)) // 🔄 AJOUTÉ
                    {
                        command.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id); // 🔄 AJOUTÉ

                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command)) // 🔄 AJOUTÉ
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            dataGridViewBudgets.AutoGenerateColumns = true;
                            dataGridViewBudgets.DataSource = table;
                            AfficherGraphiqueBudget();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du chargement : " + ex.Message);

                }
            }
        }
        private void SupprimerBudget(string categorie)
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM budget WHERE categorie = ? AND utilisateur_id = 1"; // Adapte selon l'utilisateur

                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@categorie", categorie); // 🔄 MODIFIÉ : nom explicite
                        command.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id); // 🔄 AJOUTÉ
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
                }
            }
        }

        private void CalculerTotalBudget()
        {
            decimal total = 0;

            if (!dataGridViewBudgets.Columns.Contains("budget_defini"))
            {
                MessageBox.Show("La colonne 'budget_defini' n'existe pas dans le DataGridView."); // ✅ pour débogage
                return;
            }

            foreach (DataGridViewRow row in dataGridViewBudgets.Rows)
            {
                if (row.Cells["budget_defini"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["budget_defini"].Value.ToString(), out decimal valeur))
                    {
                        total += valeur;
                    }
                }
            }

            lblTotalBudget.Text = "Budget total : " + total.ToString("N2") + " €";
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAjouterBudget_Click(object sender, EventArgs e)
        {
            FormAjouterBudget formB = new FormAjouterBudget(utilisateur);
            formB.Show();   // Ouvre la page d'acceuil 1
            this.Hide();              // Optionnel : cache la page
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAjouterDepense formB = new FormAjouterDepense(utilisateur);
            formB.Show();   // Ouvre la page d'acceuil 1
            this.Hide();              // Optionnel : cache la page
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewBudgets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ignorer les clics sur l'en-tête
            {
                // Identifier le nom de la colonne
                string colonneCliquee = dataGridViewBudgets.Columns[e.ColumnIndex].HeaderText;

                // Récupérer la catégorie (clé logique)
                string categorie = dataGridViewBudgets.Rows[e.RowIndex].Cells["categorie"].Value.ToString();

                if (colonneCliquee == "Modifier")
                {
                    // Ouvrir une petite fenêtre pour modifier
                    FormModifierBudget modifForm = new FormModifierBudget(categorie);
                    modifForm.ShowDialog();

                    // Rafraîchir après modification
                    ChargerBudgets();
                }
                else if (colonneCliquee == "Supprimer")
                {
                    DialogResult result = MessageBox.Show("Voulez-vous supprimer ce budget ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        SupprimerBudget(categorie); // 🔄 Supprime avec user lié
                        ChargerBudgets();           // 🔄 Recharge après suppression
                        CalculerTotalBudget();
                        AfficherGraphiqueBudget();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChargerBudgets();
        }

        private void panelChart_Paint(object sender, PaintEventArgs e)
        {

        }
        private void AfficherGraphiqueBudget()
        {
            // Nettoie le panel si un graphe est déjà affiché
            panelChart.Controls.Clear();

            LiveCharts.WinForms.PieChart pieChart = new LiveCharts.WinForms.PieChart
            {
                Dock = DockStyle.Fill,
                InnerRadius = 20,
                LegendLocation = LegendLocation.Right,
                BackColor = System.Drawing.Color.Transparent
            };

            SeriesCollection series = new SeriesCollection();

            foreach (DataGridViewRow row in dataGridViewBudgets.Rows)
            {
                if (row.Cells["categorie"].Value != null && row.Cells["budget_defini"].Value != null && row.Cells["montant_depense"].Value != null)
                {
                    string categorie = row.Cells["categorie"].Value.ToString();
                    if (decimal.TryParse(row.Cells["budget_defini"].Value.ToString(), out decimal budgetDefini) &&
                        decimal.TryParse(row.Cells["montant_depense"].Value.ToString(), out decimal montantDepense))
                    {
                        decimal soldeRestant = budgetDefini - montantDepense;

                        series.Add(new PieSeries
                        {
                            Title = categorie,
                            Values = new ChartValues<decimal> { soldeRestant },
                            DataLabels = true,
                            LabelPoint = chartPoint => $"{chartPoint.Y}€ ({chartPoint.Participation:P})"
                        });
                    }
                }
            }

            pieChart.Series = series;
            panelChart.Controls.Add(pieChart);
        }

    }
}
