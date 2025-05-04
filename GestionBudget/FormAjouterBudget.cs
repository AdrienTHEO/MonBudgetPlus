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
    public partial class FormAjouterBudget : Form
    {
        private ClassUtilisateur utilisateur;
        public string Categorie { get; private set; }
        public string Montant { get; private set; }
        public FormAjouterBudget(ClassUtilisateur user)
        {
            InitializeComponent();
            utilisateur = user;
        }

        private void FormAjouterBudget_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCategorie.Text == "" || txtMontant.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            decimal budgetDefini;
            if (!decimal.TryParse(txtMontant.Text, out budgetDefini))
            {
                MessageBox.Show("Montant invalide.");
                return;
            }

            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO budget (utilisateur_id, categorie, budget_defini) VALUES (?, ?, ?)";

                using (OdbcCommand cmd = new OdbcCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("uid", 1); // Remplace par l'ID réel de l'utilisateur
                    cmd.Parameters.AddWithValue("cat", txtCategorie.Text);
                    cmd.Parameters.AddWithValue("montant", budgetDefini);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                        MessageBox.Show("Budget ajouté !");
                    else
                        MessageBox.Show("Erreur lors de l’ajout.");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UC_Budget formB = new UC_Budget(utilisateur);
            formB.Show();   // Ouvre la page d'acceuil 1
            this.Hide();              // Optionnel : cache la page
        }
    }
}
