using System;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System.Data.Odbc;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace GestionBudget
{
    public partial class UserControlStat : UserControl
    {
        private ClassUtilisateur utilisateur;
        


        public UserControlStat(ClassUtilisateur utilisateur)
        {
            InitializeComponent();
            this.utilisateur = utilisateur;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UserControlStat_Load(object sender, EventArgs e)
        {
            ChargerGraphiqueRevenuDepense();
            ChargerGraphiqueCategorieDepense();
            ChargerGraphiqueSoldeMensuel();
            ChargerStatistiques();
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
            lblMoyennePourcent.Text = $"{moyennePourcentage:F1}% utilisé ";
        }


        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chartRevenuDepense_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ChargerGraphiqueRevenuDepense()
        {
            panelGraphique.Controls.Clear();

            var chart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };

            panelGraphique.Controls.Add(chart);

            Dictionary<string, double> revenusParMois = new Dictionary<string, double>();
            Dictionary<string, double> depensesParMois = new Dictionary<string, double>();

            string connectionString = "DSN=PostgreLocal;";
            using (var conn = new OdbcConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT type, EXTRACT(MONTH FROM date_transaction) AS mois, SUM(montant)
            FROM transaction
            WHERE utilisateur_id = ?
            GROUP BY type, mois
            ORDER BY mois;";

                using var command = new OdbcCommand(query, conn);
                command.Parameters.Add(new OdbcParameter("utilisateur_id", utilisateur.Id));

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string type = reader.GetString(0);
                    int mois = Convert.ToInt32(reader.GetDouble(1));
                    double total = reader.GetDouble(2);

                    string moisStr = new DateTime(2024, mois, 1).ToString("MMM");

                    if (type == "revenu")
                        revenusParMois[moisStr] = total;
                    else if (type == "depense")
                        depensesParMois[moisStr] = total;
                }
            }

            var labels = Enumerable.Range(1, 12)
                .Select(m => new DateTime(2024, m, 1).ToString("MMM"))
                .ToList();

            chart.Series = new SeriesCollection
    {
        new LineSeries
        {
            Title = "Revenus",
            Values = new ChartValues<double>(labels.Select(m => revenusParMois.ContainsKey(m) ? revenusParMois[m] : 0)),
            Stroke = System.Windows.Media.Brushes.Green,
            Fill = System.Windows.Media.Brushes.Transparent
        },
        new LineSeries
        {
            Title = "Dépenses",
            Values = new ChartValues<double>(labels.Select(m => depensesParMois.ContainsKey(m) ? depensesParMois[m] : 0)),
            Stroke = System.Windows.Media.Brushes.Red,
            Fill = System.Windows.Media.Brushes.Transparent
        }
    };

            chart.AxisX.Add(new Axis { Title = "Mois", Labels = labels });
            chart.AxisY.Add(new Axis { Title = "Montant (FCFA)" });
        }


        private void ChargerGraphiqueCategorieDepense()
        {
            panelCategorie.Controls.Clear();

            var pieChart = new LiveCharts.WinForms.PieChart
            {
                Dock = DockStyle.Fill,
                InnerRadius = 50,
                LegendLocation = LegendLocation.Right
            };

            panelCategorie.Controls.Add(pieChart);

            string connectionString = "DSN=PostgreLocal;";
            using var conn = new OdbcConnection(connectionString);
            conn.Open();

            string query = @"
        SELECT categorie, SUM(montant)
        FROM transaction
        WHERE type = 'depense' AND utilisateur_id = ?
        GROUP BY categorie;";

            using var cmd = new OdbcCommand(query, conn);
            cmd.Parameters.Add(new OdbcParameter("user_id", utilisateur.Id));

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string categorie = reader.GetString(0);
                double total = reader.GetDouble(1);

                pieChart.Series.Add(new PieSeries
                {
                    Title = categorie,
                    Values = new ChartValues<double> { total },
                    DataLabels = true
                });
            }
        }



        private void ChargerGraphiqueSoldeMensuel()
        {
            panelBudget.Controls.Clear();

            var chart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };

            panelBudget.Controls.Add(chart);

            Dictionary<string, double> revenusParMois = new Dictionary<string, double>();
            Dictionary<string, double> depensesParMois = new Dictionary<string, double>();

            string connectionString = "DSN=PostgreLocal;";
            using var conn = new OdbcConnection(connectionString);
            conn.Open();

            string query = @"
        SELECT type, EXTRACT(MONTH FROM date_transaction) AS mois, SUM(montant)
        FROM transaction
        WHERE utilisateur_id = ?
        GROUP BY type, mois
        ORDER BY mois;";

            using var cmd = new OdbcCommand(query, conn);
            cmd.Parameters.Add(new OdbcParameter("user_id", utilisateur.Id));

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string type = reader.GetString(0);
                int mois = Convert.ToInt32(reader.GetDouble(1));
                double total = reader.GetDouble(2);

                string moisStr = new DateTime(2024, mois, 1).ToString("MMM");

                if (type == "revenu")
                    revenusParMois[moisStr] = total;
                else if (type == "depense")
                    depensesParMois[moisStr] = total;
            }

            var labels = Enumerable.Range(1, 12)
                .Select(m => new DateTime(2024, m, 1).ToString("MMM"))
                .ToList();

            var soldes = labels.Select(m =>
                (revenusParMois.ContainsKey(m) ? revenusParMois[m] : 0) -
                (depensesParMois.ContainsKey(m) ? depensesParMois[m] : 0))
                .ToList();

            chart.Series = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Solde",
            Values = new ChartValues<double>(soldes),
            Fill = System.Windows.Media.Brushes.SteelBlue
        }
    };

            chart.AxisX.Add(new Axis { Title = "Mois", Labels = labels });
            chart.AxisY.Add(new Axis { Title = "Solde (FCFA)" });
        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void dtpFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }



        private void btnStatFiltrer_Click(object sender, EventArgs e)
        {
            DateTime dateDebut = dtpDebut.Value.Date;
            DateTime dateFin = dtpFin.Value.Date;

            ChargerGraphiqueRevenuDepense(dateDebut, dateFin);
            ChargerGraphiqueCategorieDepense(dateDebut, dateFin);
            ChargerGraphiqueSoldeMensuel(dateDebut, dateFin);
        }


        private void ChargerGraphiqueRevenuDepense(DateTime debut, DateTime fin)
        {
            var revenuParMois = new Dictionary<string, decimal>();
            var depenseParMois = new Dictionary<string, decimal>();

            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();

                // Récupération des revenus mensuels
                string queryRevenu = @"
            SELECT TO_CHAR(date_transaction, 'YYYY-MM') AS mois, SUM(montant) AS total
            FROM transaction
            WHERE type = 'revenu' AND utilisateur_id = ? AND date_transaction BETWEEN ? AND ?
            GROUP BY mois
            ORDER BY mois";

                using (OdbcCommand cmd = new OdbcCommand(queryRevenu, connection))
                {
                    cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                    cmd.Parameters.AddWithValue("@debut", debut);
                    cmd.Parameters.AddWithValue("@fin", fin);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mois = reader.GetString(0);
                            decimal total = reader.GetDecimal(1);
                            revenuParMois[mois] = total;
                        }
                    }
                }

                // Récupération des dépenses mensuelles
                string queryDepense = @"
            SELECT TO_CHAR(date_transaction, 'YYYY-MM') AS mois, SUM(montant) AS total
            FROM transaction
            WHERE type = 'depense' AND utilisateur_id = ? AND date_transaction BETWEEN ? AND ?
            GROUP BY mois
            ORDER BY mois";

                using (OdbcCommand cmd = new OdbcCommand(queryDepense, connection))
                {
                    cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                    cmd.Parameters.AddWithValue("@debut", debut);
                    cmd.Parameters.AddWithValue("@fin", fin);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mois = reader.GetString(0);
                            decimal total = reader.GetDecimal(1);
                            depenseParMois[mois] = total;
                        }
                    }
                }
            }

            var tousLesMois = revenuParMois.Keys
                .Union(depenseParMois.Keys)
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            var valeursRevenu = new ChartValues<decimal>();
            var valeursDepense = new ChartValues<decimal>();
            var labels = new List<string>();

            foreach (var mois in tousLesMois)
            {
                labels.Add(mois);
                valeursRevenu.Add(revenuParMois.ContainsKey(mois) ? revenuParMois[mois] : 0);
                valeursDepense.Add(depenseParMois.ContainsKey(mois) ? depenseParMois[mois] : 0);
            }

            var courbeRevenu = new LineSeries
            {
                Title = "Revenus",
                Values = valeursRevenu,
                PointGeometry = DefaultGeometries.Circle,
                Stroke = new SolidColorBrush(System.Windows.Media.Colors.Green),
                Fill = System.Windows.Media.Brushes.Transparent
            };

            var courbeDepense = new LineSeries
            {
                Title = "Dépenses",
                Values = valeursDepense,
                PointGeometry = DefaultGeometries.Square,
                Stroke = new SolidColorBrush(System.Windows.Media.Colors.Red),
                Fill = System.Windows.Media.Brushes.Transparent
            };

            var chart = new LiveCharts.WinForms.CartesianChart
            {
                Series = new SeriesCollection { courbeRevenu, courbeDepense },
                AxisX = new AxesCollection
        {
            new Axis
            {
                Title = "Mois",
                Labels = labels
            }
        },
                AxisY = new AxesCollection
        {
            new Axis
            {
                Title = "Montant (€)"
            }
        },
                Dock = DockStyle.Fill
            };

            panelGraphique.Controls.Clear();
            panelGraphique.Controls.Add(chart);
        }


        private void ChargerGraphiqueCategorieDepense(DateTime debut, DateTime fin)
        {
            panelCategorie.Controls.Clear();

            var pieChart = new LiveCharts.WinForms.PieChart
            {
                Dock = DockStyle.Fill,
                InnerRadius = 50,
                LegendLocation = LegendLocation.Right
            };

            panelCategorie.Controls.Add(pieChart);

            string connectionString = "DSN=PostgreLocal;";
            using var conn = new OdbcConnection(connectionString);
            conn.Open();

            string query = @"
        SELECT categorie, SUM(montant)
        FROM transaction
        WHERE type = 'depense'
          AND utilisateur_id = ?
          AND date_transaction BETWEEN ? AND ?
        GROUP BY categorie;
    ";

            using var cmd = new OdbcCommand(query, conn);
            cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
            cmd.Parameters.AddWithValue("@debut", debut);
            cmd.Parameters.AddWithValue("@fin", fin);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string categorie = reader.GetString(0);
                double total = reader.GetDouble(1);

                pieChart.Series.Add(new PieSeries
                {
                    Title = categorie,
                    Values = new ChartValues<double> { total },
                    DataLabels = true
                });
            }
        }


        private void ChargerGraphiqueSoldeMensuel(DateTime debut, DateTime fin)
        {            

         var revenuParMois = new Dictionary<string, decimal>();
            var depenseParMois = new Dictionary<string, decimal>();

            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();

                // Récupérer les revenus par mois
                string queryRevenus = @"
            SELECT TO_CHAR(date_transaction, 'YYYY-MM') AS mois, SUM(montant) AS total
            FROM transaction
            WHERE type = 'revenu' AND utilisateur_id = ? AND date_transaction BETWEEN ? AND ?
            GROUP BY mois
            ORDER BY mois";

                using (OdbcCommand cmd = new OdbcCommand(queryRevenus, connection))
                {
                    cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                    cmd.Parameters.AddWithValue("@debut", debut);
                    cmd.Parameters.AddWithValue("@fin", fin);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mois = reader.GetString(0);
                            decimal total = reader.GetDecimal(1);
                            revenuParMois[mois] = total;
                        }
                    }
                }

                // Récupérer les dépenses par mois
                string queryDepenses = @"
            SELECT TO_CHAR(date_transaction, 'YYYY-MM') AS mois, SUM(montant) AS total
            FROM transaction
            WHERE type = 'depense' AND utilisateur_id = ? AND date_transaction BETWEEN ? AND ?
            GROUP BY mois
            ORDER BY mois";

                using (OdbcCommand cmd = new OdbcCommand(queryDepenses, connection))
                {
                    cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                    cmd.Parameters.AddWithValue("@debut", debut);
                    cmd.Parameters.AddWithValue("@fin", fin);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mois = reader.GetString(0);
                            decimal total = reader.GetDecimal(1);
                            depenseParMois[mois] = total;
                        }
                    }
                }
            }

            // Construction du graphique
            var series = new ColumnSeries
            {
                Title = "Solde mensuel",
                Values = new ChartValues<decimal>(),
                Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(76, 175, 80)) // Vert
            };

            var labels = new List<string>();

            // Fusionner les deux dictionnaires
            var tousLesMois = revenuParMois.Keys
                .Union(depenseParMois.Keys)
                .Distinct()
                .OrderBy(m => m);

            foreach (var mois in tousLesMois)
            {
                decimal revenu = revenuParMois.ContainsKey(mois) ? revenuParMois[mois] : 0;
                decimal depense = depenseParMois.ContainsKey(mois) ? depenseParMois[mois] : 0;
                decimal solde = revenu - depense;

                series.Values.Add(solde);
                labels.Add(mois);
            }

            // Création et ajout du graphique au panel
            var chart = new LiveCharts.WinForms.CartesianChart
            {
                Series = new SeriesCollection { series },
                AxisX = new AxesCollection
        {
            new Axis
            {
                Title = "Mois",
                Labels = labels
            }
        },
                AxisY = new AxesCollection
        {
            new Axis
            {
                Title = "Solde (€)"
            }
        },
                Dock = DockStyle.Fill
            };

            // Nettoyer et ajouter au panel
            panelBudget.Controls.Clear();
            panelBudget.Controls.Add(chart);
        }





        private void BtnStatReset_Click(object sender, EventArgs e)
        {
            ChargerGraphiqueRevenuDepense();
            ChargerGraphiqueCategorieDepense();
            ChargerGraphiqueSoldeMensuel();
        }

        private void panelBudget_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
