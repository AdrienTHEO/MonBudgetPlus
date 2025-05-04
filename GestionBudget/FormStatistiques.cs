using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.WinForms;  // Pour PieChart
using LiveCharts.Wpf;       // Pour PieSeries
using System.Data.Odbc;


namespace GestionBudget
{
    public partial class FormStatistique : Form
    {
        public FormStatistique()
        {
            InitializeComponent();
            ChargerCamembert();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormStatistiques_Load(object sender, EventArgs e)
        {

        }

        private void ChargerCamembert()
        {
            pieChartBudget.Series.Clear();
            SeriesCollection series = new SeriesCollection();

            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT categorie, budget_defini, montant_depense FROM budget WHERE utilisateur_id = 1";
                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categorie = reader.GetString(0);
                            decimal budget = reader.GetDecimal(1);
                            decimal depense = reader.GetDecimal(2);
                            decimal solde = budget - depense;

                            if (solde > 0)
                            {
                                series.Add(new PieSeries
                                {
                                    Title = categorie,
                                    Values = new ChartValues<decimal> { solde },
                                    DataLabels = true
                                });
                            }
                        }
                    }

                    pieChartBudget.Series = series;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur graphique : " + ex.Message);
                }
            }
        }

        private void pieChartBudget_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
