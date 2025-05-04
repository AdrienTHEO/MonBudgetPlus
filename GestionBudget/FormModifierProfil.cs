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
    public partial class FormModifierProfil : Form
    {
        public FormModifierProfil()
        {
            InitializeComponent();
        }
        private ClassUtilisateur utilisateur;

        public FormModifierProfil(ClassUtilisateur user)
        {
            InitializeComponent();
            utilisateur = user;

            // Préremplir les champs
            txtNomComplet.Text = utilisateur.NomComplet;
            dateNaissance.Value = utilisateur.DateNaissance;
            comboPays.Text = utilisateur.Pays;

            if (utilisateur.Photo != null)
            {
                using (MemoryStream ms = new MemoryStream(utilisateur.Photo))
                {
                    pictureBoxPhoto.Image = Image.FromStream(ms);
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnChoisirPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPhoto.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            byte[] photoBytes = null;

            if (pictureBoxPhoto.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBoxPhoto.Image.Save(ms, pictureBoxPhoto.Image.RawFormat);
                    photoBytes = ms.ToArray();
                }
            }

            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE utilisateur SET nom_complet = ?, date_naissance = ?, pays = ?, photo = ? WHERE id = ?";

                    using (OdbcCommand command = new OdbcCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("nom", txtNomComplet.Text);
                        command.Parameters.AddWithValue("date", dateNaissance.Value.Date);
                        command.Parameters.AddWithValue("pays", comboPays.Text);
                        command.Parameters.AddWithValue("photo", (object)photoBytes ?? DBNull.Value);
                        command.Parameters.AddWithValue("id", utilisateur.Id);

                        int rows = command.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Profil mis à jour !");
                            // Fermer et recharger la page de profil principale si besoin
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Aucune modification effectuée.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la mise à jour : " + ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormProfil prof = new FormProfil();
            prof.Show();
            this.Hide();
        }

        private void comboPays_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
