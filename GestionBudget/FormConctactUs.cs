using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBudget
{
    public partial class FormConctactUs : Form
    {
        public FormConctactUs()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   // Ouvre la page de conctat
            this.Hide();              // Optionnel : cache la page actuel
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormConctactUs formConctactUs = new FormConctactUs();
            formConctactUs.Show();   // Ouvre la page de conctat
            this.Hide();              // Optionnel : cache la page actuel
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nom = txtNom.Text.Trim();
            string email = txtEmail.Text.Trim();
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = new OdbcConnection("DSN=PostgreLocal;");
                conn.Open();

                string query = "INSERT INTO contact_utilisateur (nom, email, message, date_contact) VALUES (?, ?, ?, CURRENT_TIMESTAMP)";
                using var cmd = new OdbcCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.ExecuteNonQuery();

                // Envoi par email
                EnvoyerEmail(nom, email, message);

                MessageBox.Show("Merci ! Votre message a été envoyé et enregistré.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void EnvoyerEmail(string nom, string email, string message)
        {
            try
            {
                // Configuration SMTP (exemple avec Gmail)
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("adrientamba790@gmail.com", "kyeh inlu xilc qtpb"),
                    EnableSsl = true,
                };

                // Ton adresse de réception
                string destinataire = "adrientamba96@gmail.com";
                string sujet = "Nouveau message de contact depuis l'application";

                // Contenu de l'email
                string corps = $"Nom : {nom}\nEmail : {email}\n\nMessage :\n{message}";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("adrientamba790@gmail.com"),
                    Subject = sujet,
                    Body = corps,
                    IsBodyHtml = false,
                };

                // 🔁 TOI tu reçois le message
                mailMessage.To.Add(destinataire);

                // 🔁 Optionnel mais pratique : tu pourras répondre directement à l'utilisateur
                mailMessage.ReplyToList.Add(new MailAddress(email));

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l’envoi de l’email : " + ex.Message, "Erreur email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
