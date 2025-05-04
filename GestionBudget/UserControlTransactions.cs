using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.Odbc;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Linq;
using System.Globalization;

namespace GestionBudget
{
    public partial class UserControlTransactions : UserControl
    {
        private ClassUtilisateur utilisateur;
       
        public UserControlTransactions( ClassUtilisateur user)
        {
            InitializeComponent();
            this.utilisateur = user;
            ChargerTransactions();

            // Ajouter la colonne "Modifier"
            DataGridViewButtonColumn btnModifier = new DataGridViewButtonColumn();
            btnModifier.HeaderText = "Modifier";
            btnModifier.Text = "🖊️ Modifier";
            btnModifier.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(btnModifier);

            // Ajouter la colonne "Supprimer"
            DataGridViewButtonColumn btnSupprimer = new DataGridViewButtonColumn();
            btnSupprimer.HeaderText = "Supprimer";
            btnSupprimer.Text = "🗑️ Supprimer";
            btnSupprimer.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(btnSupprimer);

            InitialiserGraphe();  // Une fois au début (si pas encore fait)
            MettreAJourGraphe();  // Après chaque changement




        }

        private void UserControlTransactions_Load(object sender, EventArgs e)
        {
            ChargerCategories();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FormAjouterTransaction formAjouter = new FormAjouterTransaction(utilisateur);
            formAjouter.ShowDialog();  // Ouvre le formulaire d'ajout
            ChargerTransactions();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vérifie si l'utilisateur a cliqué sur une ligne (et pas sur l'en-tête)
            if (e.RowIndex >= 0)
            {
                // Récupère la colonne qui a été cliquée (pour vérifier si c'est la colonne "Supprimer" ou "Modifier")
                string colonneCliquee = dataGridView2.Columns[e.ColumnIndex].HeaderText;

                // Récupère l'ID de la transaction à partir de la cellule correspondante (par exemple, cellule "id")
                int transactionId = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["id"].Value);

                // Si la colonne "Supprimer" a été cliquée, procéder à la suppression
                 if (colonneCliquee == "Supprimer")
                {
                    DialogResult result = MessageBox.Show("Supprimer cette transaction ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        string typeTransaction = dataGridView2.Rows[e.RowIndex].Cells["type"].Value.ToString();
                        string categorie = dataGridView2.Rows[e.RowIndex].Cells["categorie"].Value.ToString();
                        decimal montant = Convert.ToDecimal(dataGridView2.Rows[e.RowIndex].Cells["montant"].Value);
                        DateTime date = Convert.ToDateTime(dataGridView2.Rows[e.RowIndex].Cells["date_transaction"].Value);

                        string connectionString = "DSN=PostgreLocal;";
                        using (OdbcConnection connection = new OdbcConnection(connectionString))
                        {
                            connection.Open();
                            using (OdbcTransaction trans = connection.BeginTransaction())
                            {
                                try
                                {
                                    // Supprimer transaction
                                    string deleteTransaction = "DELETE FROM transaction WHERE id = ? AND utilisateur_id = ?";
                                    using (OdbcCommand cmd = new OdbcCommand(deleteTransaction, connection, trans))
                                    {
                                        cmd.Parameters.AddWithValue("@id", transactionId);
                                        cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                                        cmd.ExecuteNonQuery();
                                    }

                                    // Supprimer Budget ou Dépense si nécessaire
                                    if (typeTransaction == "Revenu")
                                    {
                                        string deleteBudget = "DELETE FROM budget WHERE categorie = ? AND utilisateur_id = ?";
                                        using (OdbcCommand cmd = new OdbcCommand(deleteBudget, connection, trans))
                                        {
                                            cmd.Parameters.AddWithValue("@categorie", categorie);
                                            cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    else if (typeTransaction == "Dépense")
                                    {
                                        string deleteDepense = "DELETE FROM depense WHERE budget_id = (SELECT id FROM budget WHERE categorie = ? AND utilisateur_id = ?) AND montant = ? AND date_depense = ?";
                                        using (OdbcCommand cmd = new OdbcCommand(deleteDepense, connection, trans))
                                        {
                                            cmd.Parameters.AddWithValue("@categorie", categorie);
                                            cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                                            cmd.Parameters.AddWithValue("@montant", montant);
                                            cmd.Parameters.AddWithValue("@date_depense", date);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }

                                    trans.Commit();
                                    MessageBox.Show("Transaction supprimée."); 
                                    ChargerTransactions();
                                    InitialiserGraphe();  
                                    MettreAJourGraphe(); 

                                }
                                catch (Exception ex)
                                {
                                    trans.Rollback();
                                    MessageBox.Show("Erreur : " + ex.Message);
                                }
                            }
                        }
                    }
                }

                // Si la colonne "Modifier" a été cliquée, ouvrir le formulaire de modification
                else if (colonneCliquee == "Modifier")
                {
                    // Ouvrir un formulaire de modification pour cette transaction avec les paramètres transactionId et utilisateurId
                    FormModifierTransaction formModifier = new FormModifierTransaction(transactionId, utilisateur);
                    formModifier.ShowDialog(); // Afficher le formulaire de modification en mode dialogue

                    // Rafraîchir le DataGridView après modification (si nécessaire)
                    ChargerTransactions();
                }
            }
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFiltrer_Click(object sender, EventArgs e)
        {
           
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void ChargerTransactions(DateTime? startDate = null, DateTime? endDate = null, string type = null)
        {
            string query = "SELECT id, date_transaction, categorie, montant, type FROM transaction WHERE utilisateur_id = @utilisateur_id";

            // Appliquer les filtres sur les dates et le type si spécifiés
            if (startDate.HasValue)
                query += " AND date_transaction >= @startDate";
            if (endDate.HasValue)
                query += " AND date_transaction <= @endDate";
            if (!string.IsNullOrEmpty(type))
                query += " AND type = @type";

            // Remplir le DataGridView avec les transactions
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                        if (startDate.HasValue)
                            command.Parameters.AddWithValue("@startDate", startDate.Value);
                        if (endDate.HasValue)
                            command.Parameters.AddWithValue("@endDate", endDate.Value);
                        if (!string.IsNullOrEmpty(type))
                            command.Parameters.AddWithValue("@type", type);

                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView2.DataSource = table;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du chargement des transactions : " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FiltrerEtMettreAJourGraphique();
        }

        // Déclare la méthode
        private void InitialiserGraphe()
        {
            // Nettoie le panel s'il y avait déjà un graphe
            panelGraphique.Controls.Clear();

            LiveCharts.WinForms.CartesianChart chart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill,
                Name = "monGraphe"
            };

            panelGraphique.Controls.Add(chart);
        }

        private void MettreAJourGraphe()
        {
            var revenusParMois = new Dictionary<string, double>();
            var depensesParMois = new Dictionary<string, double>();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                string type = row.Cells["type"].Value.ToString().ToLower(); // "revenu" ou "depense"
                decimal montant = Convert.ToDecimal(row.Cells["montant"].Value);
                DateTime date = Convert.ToDateTime(row.Cells["date_transaction"].Value);

                string mois = date.ToString("MMMM yyyy", new CultureInfo("fr-FR")); // Exemple : "avril 2025"

                if (type == "revenu")
                {
                    if (!revenusParMois.ContainsKey(mois)) revenusParMois[mois] = 0;
                    revenusParMois[mois] += (double)montant;
                }
                else if (type == "depense")
                {
                    if (!depensesParMois.ContainsKey(mois)) depensesParMois[mois] = 0;
                    depensesParMois[mois] += (double)montant;
                }
            }

            var tousLesMois = revenusParMois.Keys
                                .Union(depensesParMois.Keys)
                                .Distinct()
                                .OrderBy(m => DateTime.ParseExact(m, "MMMM yyyy", new CultureInfo("fr-FR")))
                                .ToList();

            var valeursRevenus = new ChartValues<double>();
            var valeursDepenses = new ChartValues<double>();

            foreach (var mois in tousLesMois)
            {
                valeursRevenus.Add(revenusParMois.ContainsKey(mois) ? revenusParMois[mois] : 0);
                valeursDepenses.Add(depensesParMois.ContainsKey(mois) ? depensesParMois[mois] : 0);
            }

            var chart = panelGraphique.Controls["monGraphe"] as LiveCharts.WinForms.CartesianChart;

            chart.Series = new SeriesCollection
             {
                new LiveCharts.Wpf.LineSeries
                {
                    Title = "Revenus",
                    Values = valeursRevenus,
                    Stroke = System.Windows.Media.Brushes.Green,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometrySize = 10
                },
                new LiveCharts.Wpf.LineSeries
                {
                    Title = "Dépenses",
                    Values = valeursDepenses,
                    Stroke = System.Windows.Media.Brushes.Red,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometrySize = 10
                }
            };

            chart.AxisX.Clear();
            chart.AxisX.Add(new Axis
            {
                Title = "Mois",
                Labels = tousLesMois
            });

            chart.AxisY.Clear();
            chart.AxisY.Add(new Axis
            {
                Title = "Montant",
                LabelFormatter = value => value.ToString("C")
            });

            chart.LegendLocation = LegendLocation.Right;
        }
        private void FiltrerEtMettreAJourGraphique()
        {
            DateTime dateDebut = dtpDebut.Value.Date;
            DateTime dateFin = dtpFin.Value.Date;
            string categorieFiltre = cbxCategorieFiltre.SelectedItem?.ToString();

            var lignesFiltrees = dataGridView2.Rows
                .Cast<DataGridViewRow>()
                .Where(row =>
                {
                    if (row.IsNewRow) return false;

                    DateTime date = Convert.ToDateTime(row.Cells["date_transaction"].Value);
                    string categorie = row.Cells["categorie"].Value.ToString();

                    bool dateOk = date >= dateDebut && date <= dateFin;
                    bool categorieOk = (categorieFiltre == "Toutes" || string.IsNullOrEmpty(categorieFiltre)) || categorie == categorieFiltre;

                    return dateOk && categorieOk;
                })
                .ToList();

            MettreAJourGrapheDepuisLignes(lignesFiltrees);
        }


        private void MettreAJourGrapheDepuisLignes(List<DataGridViewRow> lignes)
        {
            var revenusParMois = new Dictionary<string, double>();
            var depensesParMois = new Dictionary<string, double>();

            foreach (DataGridViewRow row in lignes)
            {
                string type = row.Cells["type"].Value.ToString().ToLower();
                decimal montant = Convert.ToDecimal(row.Cells["montant"].Value);
                DateTime date = Convert.ToDateTime(row.Cells["date_transaction"].Value);

                string mois = date.ToString("MMMM yyyy", new CultureInfo("fr-FR"));

                if (type == "revenu")
                {
                    if (!revenusParMois.ContainsKey(mois)) revenusParMois[mois] = 0;
                    revenusParMois[mois] += (double)montant;
                }
                else if (type == "depense")
                {
                    if (!depensesParMois.ContainsKey(mois)) depensesParMois[mois] = 0;
                    depensesParMois[mois] += (double)montant;
                }
            }

            var tousLesMois = revenusParMois.Keys
                                .Union(depensesParMois.Keys)
                                .Distinct()
                                .OrderBy(m => DateTime.ParseExact(m, "MMMM yyyy", new CultureInfo("fr-FR")))
                                .ToList();

            var valeursRevenus = new ChartValues<double>();
            var valeursDepenses = new ChartValues<double>();

            foreach (var mois in tousLesMois)
            {
                valeursRevenus.Add(revenusParMois.ContainsKey(mois) ? revenusParMois[mois] : 0);
                valeursDepenses.Add(depensesParMois.ContainsKey(mois) ? depensesParMois[mois] : 0);
            }

            var chart = panelGraphique.Controls["monGraphe"] as LiveCharts.WinForms.CartesianChart;

            chart.Series = new SeriesCollection
    {
        new LiveCharts.Wpf.LineSeries
        {
            Title = "Revenus",
            Values = valeursRevenus,
            Stroke = System.Windows.Media.Brushes.Green,
            Fill = System.Windows.Media.Brushes.Transparent
        },
        new LiveCharts.Wpf.LineSeries
        {
            Title = "Dépenses",
            Values = valeursDepenses,
            Stroke = System.Windows.Media.Brushes.Red,
            Fill = System.Windows.Media.Brushes.Transparent
        }
    };

            chart.AxisX.Clear();
            chart.AxisX.Add(new Axis
            {
                Title = "Mois",
                Labels = tousLesMois
            });

            chart.AxisY.Clear();
            chart.AxisY.Add(new Axis
            {
                Title = "Montant",
                LabelFormatter = value => value.ToString("C")
            });

            chart.LegendLocation = LegendLocation.Right;
        }

        private void ChargerCategories()
        {
            var categories = new HashSet<string>();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;
                string cat = row.Cells["categorie"].Value.ToString();
                categories.Add(cat);
            }

            cbxCategorieFiltre.Items.Clear();
            cbxCategorieFiltre.Items.Add("Toutes");
            cbxCategorieFiltre.Items.AddRange(categories.ToArray());
            cbxCategorieFiltre.SelectedIndex = 0;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            // Réinitialise les filtres
            dtpDebut.Value = DateTime.Now.AddMonths(-1); // ou DateTime.Today selon ton choix
            dtpFin.Value = DateTime.Now;
            cbxCategorieFiltre.SelectedIndex = -1; // Déselectionne la catégorie

            ChargerTransactions();
            InitialiserGraphe();
            MettreAJourGraphe();  // Après chaque changement

        }
    }
}
