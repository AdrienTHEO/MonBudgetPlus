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
    public partial class FormContribuerObjectif : Form
    {
        private object idObjectif;
        private ClassUtilisateur utilisateur;

        public FormContribuerObjectif(int objectifId, ClassUtilisateur utilisateur)
        {
            InitializeComponent();
             idObjectif = objectifId;
            this.utilisateur = utilisateur;
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            decimal montant = numMontantObjectif.Value;

            using var conn = new OdbcConnection("DSN=PostgreLocal;");
            conn.Open();

            // Mise à jour du montant
            string update = "UPDATE objectif SET montant_actuel = montant_actuel + ? WHERE id = ?";
            using var cmd = new OdbcCommand(update, conn);
            cmd.Parameters.AddWithValue("@montant", montant);
            cmd.Parameters.AddWithValue("@id", idObjectif);
            cmd.ExecuteNonQuery();

            // Récupération des infos à jour
            string select = "SELECT montant_objectif, montant_actuel, notif_50, notif_100 FROM objectif WHERE id = ?";
            using var cmdSelect = new OdbcCommand(select, conn);
            cmdSelect.Parameters.AddWithValue("@id", idObjectif);

            using var reader = cmdSelect.ExecuteReader();
            if (reader.Read())
            {
                decimal objectif = reader.GetDecimal(0);
                decimal actuel = reader.GetDecimal(1);

                // Conversion sécurisée de notif_50
                bool notif50 = false;
                if (!reader.IsDBNull(2))
                {
                    var val = reader.GetValue(2);
                    if (val is bool b) notif50 = b;
                    else if (val is int i) notif50 = i == 1;
                }

                // Conversion sécurisée de notif_100
                bool notif100 = false;
                if (!reader.IsDBNull(3))
                {
                    var val = reader.GetValue(3);
                    if (val is bool b) notif100 = b;
                    else if (val is int i) notif100 = i == 1;
                }

                double pourcentage = (double)(actuel / objectif) * 100;

                // Notification 50%
                if (pourcentage >= 50 && !notif50)
                {
                    string msg = "👏 Vous avez atteint 50% de votre objectif d’épargne ! Continuez comme ça 💪";
                    AjouterNotification(utilisateur.Id, msg, "info");

                    using var update50 = new OdbcCommand("UPDATE objectif SET notif_50 = TRUE WHERE id = ?", conn);
                    update50.Parameters.AddWithValue("@id", idObjectif);
                    update50.ExecuteNonQuery();
                }

                // Notification 100%
                if (pourcentage >= 100 && !notif100)
                {
                    string msg = "🎉 Félicitations ! Vous avez atteint votre objectif d’épargne 🏆";
                    AjouterNotification(utilisateur.Id, msg, "succès");

                    using var update100 = new OdbcCommand("UPDATE objectif SET notif_100 = TRUE WHERE id = ?", conn);
                    update100.Parameters.AddWithValue("@id", idObjectif);
                    update100.ExecuteNonQuery();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AjouterNotification(int utilisateurId, string message, string type)
        {
            using var conn = new OdbcConnection("DSN=PostgreLocal;");
            conn.Open();

            string insert = "INSERT INTO notification (utilisateur_id, message, type) VALUES (?, ?, ?)";
            using var cmd = new OdbcCommand(insert, conn);
            cmd.Parameters.AddWithValue("@utilisateur_id", utilisateurId);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.ExecuteNonQuery();
        }


        private void FormContribuerObjectif_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }


}
