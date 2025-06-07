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
    public partial class FormAjouterDepense : Form
    {
        private ClassUtilisateur utilisateur;
        public FormAjouterDepense(ClassUtilisateur user)
        {
            InitializeComponent();
            string connectionString = "DSN=PostgreLocal;";
            utilisateur = user;

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, categorie FROM budget WHERE utilisateur_id = ?";

                using (OdbcCommand cmd = new OdbcCommand(query, connection))
                {
                    // Passage du paramètre utilisateur.Id ici, avant ExecuteReader()
                    cmd.Parameters.AddWithValue("", utilisateur.Id);

                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<string, int> categories = new Dictionary<string, int>();

                        while (reader.Read())
                        {
                            string categorie = reader["categorie"].ToString();
                            int id = Convert.ToInt32(reader["id"]);
                            comboCategorie.Items.Add(categorie);
                            categories[categorie] = id;
                        }

                        comboCategorie.Tag = categories; // Affecte le dictionnaire après la boucle
                    }
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormAjouterDepense_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnAjouterDepense_Click(object sender, EventArgs e)
        {
            if (comboCategorie.Text == "" || txtMontant.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            decimal montant;
            if (!decimal.TryParse(txtMontant.Text, out montant))
            {
                MessageBox.Show("Montant invalide.");
                return;
            }

            int budgetId = ((Dictionary<string, int>)comboCategorie.Tag)[comboCategorie.Text];
            string connectionString = "DSN=PostgreLocal;";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO depense (budget_id, montant, date_depense) VALUES (?, ?, ?)";

                using (OdbcCommand cmd = new OdbcCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("bid", budgetId);
                    cmd.Parameters.AddWithValue("montant", montant);
                    cmd.Parameters.AddWithValue("date", datePickerDepense.Value.Date);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        MessageBox.Show("Dépense ajoutée !");
                    else
                        MessageBox.Show("Erreur lors de l’ajout.");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UC_Budget formB = new UC_Budget(utilisateur);
            formB.Show();   // Ouvre la page d'acceuil 1
            this.Hide();    // Optionnel : cache la page
        }
    }

}

