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
            CalculerTotalBudget();
            CalculerBudgetRestantTotal();
            AfficherGraphiqueBudget();
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
                                b.id,  
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
                            WHERE b.utilisateur_id = ?
                            GROUP BY b.id, b.categorie, b.budget_defini";




                    using (OdbcCommand command = new OdbcCommand(query, connection)) 
                    {

                        command.Parameters.AddWithValue("", utilisateur.Id); 




                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command)) 
                        {
                            DataTable table = new DataTable();
                            dataGridViewBudgets.Columns.Clear(); // Réinitialise complètement les colonnes
                            adapter.Fill(table);

                            dataGridViewBudgets.AutoGenerateColumns = true;
                            dataGridViewBudgets.DataSource = table;

                            bool boutonModifierExiste = dataGridViewBudgets.Columns
                                .OfType<DataGridViewButtonColumn>()
                                .Any(col => col.Name == "Modifier");

                            if (!boutonModifierExiste)
                             {
                                DataGridViewButtonColumn btnModifier = new DataGridViewButtonColumn();
                                btnModifier.HeaderText = "Modifier";
                                btnModifier.Text = "🖊️ Modifier";
                                btnModifier.UseColumnTextForButtonValue = true;
                                btnModifier.Name = "Modifier";
                                dataGridViewBudgets.Columns.Add(btnModifier);   // Ajouter colonne Modifier
                            }


                            if (!dataGridViewBudgets.Columns.Contains("Supprimer"))
                            {
                                DataGridViewButtonColumn btnSupprimer = new DataGridViewButtonColumn();
                                btnSupprimer.HeaderText = "Supprimer";
                                btnSupprimer.Text = "🗑️ Supprimer";
                                btnSupprimer.UseColumnTextForButtonValue = true;
                                btnSupprimer.Name = "Supprimer";
                                dataGridViewBudgets.Columns.Add(btnSupprimer);
                            }
                            CalculerTotalBudget();
                            CalculerBudgetRestantTotal();
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
        private void SupprimerBudget(int budgetId)
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM budget WHERE id = ? AND utilisateur_id = ?";

                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("", budgetId);
                        command.Parameters.AddWithValue("", utilisateur.Id);
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

            lblTotalBudget.Text =  total.ToString("N2") + " fcfa";
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAjouterBudget_Click(object sender, EventArgs e)
        {
            FormAjouterBudget formB = new FormAjouterBudget(utilisateur);
            formB.ShowDialog(); // Attend que le formulaire soit fermé
            ChargerBudgets();   // Recharge les données
            CalculerTotalBudget();
            AfficherGraphiqueBudget();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAjouterDepense formD = new FormAjouterDepense(utilisateur);
            formD.ShowDialog(); // Attend la fermeture
            ChargerBudgets();   // Recharge la grille
            CalculerTotalBudget();
            AfficherGraphiqueBudget();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalBudget_Click(object sender, EventArgs e)
        {

        }

        private void CalculerBudgetRestantTotal()
        {
            decimal totalBudget = 0;
            decimal totalDepense = 0;

            foreach (DataGridViewRow row in dataGridViewBudgets.Rows)
            {
                if (row.Cells["budget_defini"].Value != null &&
                    decimal.TryParse(row.Cells["budget_defini"].Value.ToString(), out decimal budgetDefini))
                {
                    totalBudget += budgetDefini;
                }

                if (row.Cells["montant_depense"].Value != null &&
                    decimal.TryParse(row.Cells["montant_depense"].Value.ToString(), out decimal depense))
                {
                    totalDepense += depense;
                }
            }

            decimal budgetRestant = totalBudget - totalDepense;

            // Assure-toi d'avoir un label lblBudgetRestant créé sur ton UserControl
            lblBudgetRestant.Text = $"{budgetRestant:N2} fcfa";
        }


        private void dataGridViewBudgets_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ignorer les clics sur l'en-tête
            {
                // Identifier le nom de la colonne
                string colonneCliquee = dataGridViewBudgets.Columns[e.ColumnIndex].HeaderText;

                // Récupérer l'ID du budget
                if (dataGridViewBudgets.Rows[e.RowIndex].Cells["id"].Value == null)
                {
                    MessageBox.Show("ID du budget introuvable.");
                    return;
                }

                int budgetId = Convert.ToInt32(dataGridViewBudgets.Rows[e.RowIndex].Cells["id"].Value);
                string categorie = dataGridViewBudgets.Rows[e.RowIndex].Cells["categorie"].Value.ToString();

                if (colonneCliquee == "Modifier")
                {
                    FormModifierBudget modifForm = new FormModifierBudget(categorie, utilisateur);
                    modifForm.ShowDialog();

                    ChargerBudgets(); // Rafraîchir
                }
                else if (colonneCliquee == "Supprimer")
                {
                    DialogResult result = MessageBox.Show($"Voulez-vous supprimer le budget '{categorie}' ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        SupprimerBudget(budgetId); // Suppression via ID
                        ChargerBudgets();          // Rafraîchir
                        CalculerTotalBudget();
                        AfficherGraphiqueBudget();
                    }
                }
            }
        }

    }
}
