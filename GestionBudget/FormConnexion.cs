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
    public partial class FormConnexion : Form
    {
        public FormConnexion()
        {
            InitializeComponent();
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.DarkRed; // Change la couleur en bleu
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black; // Remet la couleur d’origine
        }

    

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   // Ouvre la page d'acceuil 1
            this.Hide();              // Optionnel : cache la page
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
                // Vérification des champs
                if (txtNomComplet.Text == "" || txtMotDePasse.Text == "")
                {
                    MessageBox.Show("Veuillez remplir tous les champs.");
                    return;
                }

                // Hachage personnalisé du mot de passe saisi
                string hashedPassword = CustomHashPassword(txtMotDePasse.Text);

                // Connexion à PostgreSQL via ODBC (DSN)
                string connectionString = "DSN=PostgreLocal;";

                using (OdbcConnection connection = new OdbcConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Requête SQL pour récupérer le mot de passe de l'utilisateur correspondant au nom d'utilisateur
                        string query = "SELECT mot_de_passe FROM utilisateur WHERE nom_complet = ?";

                        using (OdbcCommand command = new OdbcCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("nom", txtNomComplet.Text);

                            // Exécution de la requête pour obtenir le mot de passe stocké
                            var result = command.ExecuteScalar();

                            if (result != null)
                            {
                                // Comparaison entre le mot de passe saisi (haché) et celui stocké dans la base de données
                                if (hashedPassword == result.ToString())
                                {
                                // Nouvel appel SQL pour charger les infos complètes
                                string infoQuery = "SELECT id, nom_complet, mot_de_passe, date_naissance, pays, photo FROM utilisateur WHERE nom_complet = ?";
                                using (OdbcCommand infoCommand = new OdbcCommand(infoQuery, connection))
                                {
                                    infoCommand.Parameters.AddWithValue("nom", txtNomComplet.Text);
                                    using (OdbcDataReader reader = infoCommand.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            ClassUtilisateur user = new ClassUtilisateur
                                            {
                                                Id = reader.GetInt32(0),
                                                NomComplet = reader.GetString(1),
                                                MotDePasse = reader.GetString(2),
                                                DateNaissance = reader.GetDateTime(3),
                                                Pays = reader.GetString(4),
                                                Photo = reader.IsDBNull(5) ? null : (byte[])reader[5]
                                            };

                                            // Ajouter une notification de bienvenue
                                            string msg = $"🎉 Bienvenue {user.NomComplet} dans la plateforme de gestion de budget étudiante !";
                                            string type = "succès";

                                            string insertNotifQuery = "INSERT INTO notification (utilisateur_id, message, type) VALUES (?, ?, ?)";
                                            using (OdbcCommand notifCmd = new OdbcCommand(insertNotifQuery, connection))
                                            {
                                                notifCmd.Parameters.AddWithValue("@utilisateur_id", user.Id);
                                                notifCmd.Parameters.AddWithValue("@message", msg);
                                                notifCmd.Parameters.AddWithValue("@type", type);
                                                notifCmd.ExecuteNonQuery();
                                            }

                                            FormProfil profil = new FormProfil(user); // on passe l'utilisateur
                                            profil.Show();
                                            this.Hide();
                                        }
                                    }
                                }
                            }
                            else
                                {
                                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nom d'utilisateur non trouvé.");
                            }
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



        private void FormConnexion_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormInscription ins = new FormInscription();
            ins.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
