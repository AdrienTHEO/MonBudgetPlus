using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace GestionBudget
{
    public partial class UserControlNotif : UserControl
    {
        private ClassUtilisateur utilisateur;
        public static UserControlNotif Instance;
        public UserControlNotif( ClassUtilisateur utilisateur)
        {
            InitializeComponent();
            this.utilisateur = utilisateur;
            Instance = this;
            ChargerNotifications(utilisateur.Id); // Appel au chargement
            

        }

        private void UserControlNotif_Load(object sender, EventArgs e)
        {
            ChargerNotifications(utilisateur.Id); // Appel au chargement

        }


        private string GetRelativeTime(DateTime date)
        {
            var diff = DateTime.Now - date;
            if (diff.TotalDays >= 1)
                return $"il y a {Math.Floor(diff.TotalDays)} jour(s)";
            if (diff.TotalHours >= 1)
                return $"il y a {Math.Floor(diff.TotalHours)}h";
            if (diff.TotalMinutes >= 1)
                return $"il y a {Math.Floor(diff.TotalMinutes)} min";
            return "À l’instant";
        }

        private void ChargerNotifications(int utilisateurId)
        {
            flowLayoutNotifications.Controls.Clear(); // Nettoyer avant d’ajouter

            using (var conn = new OdbcConnection("DSN=PostgreLocal;"))
            {
                conn.Open();
                string query = @"
            SELECT message, type, date_notification
            FROM notification
            WHERE utilisateur_id = ?
            ORDER BY date_notification DESC";

                using (var cmd = new OdbcCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", utilisateurId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string message = !reader.IsDBNull(0) ? reader.GetString(0) : "Message vide";
                            string type = !reader.IsDBNull(1) ? reader.GetString(1) : "info"; // valeur par défaut
                            DateTime date = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now;

                            // Crée un contrôle personnalisé pour chaque notification
                            var panel = new Panel
                            {
                                Height = 60,
                                Width = flowLayoutNotifications.Width - 30,
                                Padding = new Padding(10),
                                Margin = new Padding(5),
                                BackColor = type == "succès" ? Color.FromArgb(200, 230, 200) : Color.FromArgb(240, 200, 200),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            var icon = new PictureBox
                            {
                                Width = 32,
                                Height = 32,
                                Image = type == "succès" ? Properties.Resources.ic_check_green : Properties.Resources.ic_alert_red,
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Location = new Point(5, 10)
                            };

                            var labelMessage = new Label
                            {
                                AutoSize = false,
                                Text = message,
                                Location = new Point(45, 10),
                                Size = new Size(panel.Width - 150, 20),
                                ForeColor = Color.Black
                            };

                            var labelTime = new Label
                            {
                                AutoSize = true,
                                Text = GetRelativeTime(date), // Fonction pour formater "il y a X heures"
                                Anchor = AnchorStyles.Right,
                                ForeColor = Color.Gray,
                                Location = new Point(panel.Width - 100, 10)
                            };

                            panel.Controls.Add(icon);
                            panel.Controls.Add(labelMessage);
                            panel.Controls.Add(labelTime);

                            flowLayoutNotifications.Controls.Add(panel);
                        }
                    }
                }
            }
        }






        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutNotifications_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void btnRafraichirNotifs_Click(object sender, EventArgs e)
        {
            ChargerNotifications(utilisateur.Id);
        }
    }
}
