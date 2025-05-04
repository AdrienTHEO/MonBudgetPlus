using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
namespace GestionBudget
{
    public partial class FormModifierBudget : Form
    {
        private string categorie;

        public FormModifierBudget(string cat)
        {
            InitializeComponent();

            this.categorie = cat;
            lblCategorie.Text = "Catégorie : " + cat; // Un label pour afficher

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE budget SET budget_defini = ?, montant_depense = ? WHERE categorie = ? AND utilisateur_id = 1";

                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("budget", Convert.ToDecimal(txtBudgetDefini.Text));
                        command.Parameters.AddWithValue("depense", Convert.ToDecimal(txtDepenseActuelle.Text));
                        command.Parameters.AddWithValue("cat", categorie);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Budget mis à jour !");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormModifierBudget_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
