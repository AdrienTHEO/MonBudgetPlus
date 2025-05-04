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
using PieChart = LiveCharts.WinForms.PieChart;

namespace GestionBudget
{
    public partial class UC_Budget : UserControl
    {
        private ClassUtilisateur utilisateur;

        public UC_Budget(ClassUtilisateur user)
        {
            InitializeComponent();
            this.utilisateur = user;
            ChargerBudgets(); // Charger les données
            InitialiserColonnesActions(); // Ajouter les boutons Modifier / Supprimer
        }

        private void InitialiserColonnesActions()
        {
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

            dataGridViewBudgets.CellClick += dataGridViewBudgets_CellContentClick;
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
                        WHERE b.utilisateur_id = ?
                        GROUP BY b.id, b.categorie, b.budget_defini";

                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("utilisateur_id", utilisateur.Id);

                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridViewBudgets.AutoGenerateColumns = true;
                            dataGridViewBudgets.DataSource = table;
                        }
                    }

                    CalculerTotalBudget(); // Appeler APRÈS remplissage du DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du chargement : " + ex.Message);
                }
            }
        }

        private void CalculerTotalBudget()
        {
            decimal total = 0;

            if (!dataGridViewBudgets.Columns.Contains("budget_defini"))
            {
                MessageBox.Show("La colonne 'budget_defini' n'existe pas dans le DataGridView.");
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

        private void dataGridViewBudgets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignorer clics sur en-tête

            string colonneCliquee = dataGridViewBudgets.Columns[e.ColumnIndex].HeaderText;
            string categorie = dataGridViewBudgets.Rows[e.RowIndex].Cells["categorie"].Value.ToString();

            if (colonneCliquee == "Modifier")
            {
                FormModifierBudget modifForm = new FormModifierBudget(categorie);
                modifForm.ShowDialog();
                ChargerBudgets();
            }
            else if (colonneCliquee == "Supprimer")
            {
                DialogResult result = MessageBox.Show("Voulez-vous supprimer ce budget ?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    SupprimerBudget(categorie);
                    ChargerBudgets();
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
                    string query = "DELETE FROM budget WHERE categorie = ? AND utilisateur_id = ?";

                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("categorie", categorie);
                        command.Parameters.AddWithValue("utilisateur_id", utilisateur.Id);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
                }
            }
        }

        // Autres handlers d'événements si nécessaire...
    }
}
