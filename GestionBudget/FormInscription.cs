using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBudget
{
    public partial class FormInscription : Form
    {
        public FormInscription()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void FormInscription_Load(object sender, EventArgs e)
        {

        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red; // Change la couleur en bleu
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black; // Remet la couleur d’origine
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   // Ouvre la page d'acceuil 1
            this.Hide();              // Optionnel : cache la page
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   // Ouvre la page d'acceuil 1
            this.Hide();              // Optionnel : cache la page
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Vérification des champs
            if (txtNomComplet.Text == "" || txtMotDePasse.Text == "" || txtConfirmationMotDePasse.Text == "" ||
                comboPays.Text == "" || !checkCGU.Checked)
            {
                MessageBox.Show("Veuillez remplir tous les champs et accepter les CGU.");
                return;
            }

            if (txtMotDePasse.Text != txtConfirmationMotDePasse.Text)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.");
                return;
            }

            // Hachage personnalisé du mot de passe
            string hashedPassword = CustomHashPassword(txtMotDePasse.Text);

            // Connexion à PostgreSQL via ODBC (DSN)
            string connectionString = "DSN=PostgreLocal;";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO utilisateur (nom_complet, mot_de_passe, date_naissance, pays) " +
                                   "VALUES (?, ?, ?, ?)";

                    using (OdbcCommand command = new OdbcCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("nom", txtNomComplet.Text);
                        command.Parameters.AddWithValue("mdp", hashedPassword); // Utilisation du mot de passe haché
                        command.Parameters.AddWithValue("date", dateNaissance.Value.Date);
                        command.Parameters.AddWithValue("pays", comboPays.Text);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                            MessageBox.Show("Inscription réussie !");
                        else
                            MessageBox.Show("Erreur lors de l'inscription.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
        }

        // Méthode pour le hachage personnalisé (lettres de l'alphabet inversé)
        private string CustomHashPassword(string password)
        {
            StringBuilder hashedPassword = new StringBuilder();

            foreach (char c in password.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                {
                    // Calculer la valeur inverse (Z = 1, Y = 2, ..., A = 26)
                    int numericValue = 'Z' - c + 1;
                    hashedPassword.Append(numericValue.ToString());
                }
                else
                {
                    // Si c'est un caractère non alphabétique, l'ajouter tel quel
                    hashedPassword.Append(c);
                }
            }

            return hashedPassword.ToString();
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormConnexion cns = new FormConnexion();
            cns.Show();
            this.Hide();
        }
    }
}
