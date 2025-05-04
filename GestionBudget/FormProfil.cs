using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBudget
{
    public partial class FormProfil : Form
    {
        public FormProfil()
        {
            InitializeComponent();
            RechargerUtilisateurDepuisBD();
           


        }
        private ClassUtilisateur utilisateur;

        public FormProfil(ClassUtilisateur user)
        {
            InitializeComponent();   // Appel nécessaire pour initialiser les contrôles du formulaire
            this.utilisateur = user; // On stocke l’utilisateur dans le champ 
            // Initialisation des informations utilisateur
            utilisateur = user;

            // Remplir les contrôles avec les données de l'utilisateur
            lblNom.Text = utilisateur.NomComplet; // Exemple pour afficher le nom complet
            lblPays.Text = utilisateur.Pays; // Exemple pour afficher le pays
            lblDate.Text = utilisateur.DateNaissance.ToShortDateString(); // Exemple pour afficher la date de naissance

            // Afficher la photo si elle existe
            if (utilisateur.Photo != null)
            {
                using (MemoryStream ms = new MemoryStream(utilisateur.Photo))
                {
                    picPhoto.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picPhoto.Image = null; // Ou une image par défaut
            }

        }


        private void FormProfil_Load(object sender, EventArgs e)
        {
            lblNom.Text = utilisateur.NomComplet;
            lblDate.Text = utilisateur.DateNaissance.ToShortDateString();
            lblPays.Text = utilisateur.Pays;

            if (utilisateur.Photo != null)
            {
                using (MemoryStream ms = new MemoryStream(utilisateur.Photo))
                {
                    picPhoto.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picPhoto.Image = null; // ou une image par défaut
            }
        }





        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNomCompletProfile_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            FormModifierProfil formModif = new FormModifierProfil(utilisateur);
            formModif.FormClosed += (s, args) =>
            {
                // Recharge les données après modification
                RechargerUtilisateurDepuisBD();
            };
            formModif.ShowDialog();
        }

        private void RechargerUtilisateurDepuisBD()
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT nom_complet, date_naissance, pays, photo FROM utilisateur WHERE id = ?";
                using (OdbcCommand cmd = new OdbcCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("id", utilisateur.Id);
                    using (OdbcDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            utilisateur.NomComplet = reader.GetString(0);
                            utilisateur.DateNaissance = reader.GetDateTime(1);
                            utilisateur.Pays = reader.GetString(2);
                            utilisateur.Photo = reader.IsDBNull(3) ? null : (byte[])reader[3];
                        }
                    }
                }
            }

            // Actualiser l'affichage
            FormProfil_Load(null, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 form2 = new Form3(utilisateur);
            form2.Show();   // Ouvre le formulaire d’inscription
            this.Hide();              // Optionnel : cache le formulaire actuel
        }

        private void FormProfil_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
