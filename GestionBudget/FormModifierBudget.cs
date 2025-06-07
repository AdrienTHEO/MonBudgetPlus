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
        private ClassUtilisateur utilisateur;



        public FormModifierBudget(string cat, ClassUtilisateur utilisateur)
        {
            InitializeComponent();
            this.utilisateur = utilisateur;
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

                    // --- 1. Mise à jour du budget ---
                    string updateBudgetQuery = "UPDATE budget SET budget_defini = ? WHERE categorie = ? AND utilisateur_id = ?";

                    using (OdbcCommand cmdBudget = new OdbcCommand(updateBudgetQuery, connection))
                    {
                        cmdBudget.Parameters.AddWithValue("", Convert.ToDecimal(txtBudgetDefini.Text));
                        cmdBudget.Parameters.AddWithValue("", categorie);
                        cmdBudget.Parameters.AddWithValue("", utilisateur.Id); // ✅ utilisateur connecté

                        cmdBudget.ExecuteNonQuery();
                    }

                    // --- 2. Mise à jour de la dernière dépense associée ---
                    string updateDepenseQuery = @"
                UPDATE depense 
                SET montant = ? 
                WHERE id = (
                    SELECT d.id 
                    FROM depense d
                    INNER JOIN budget b ON b.id = d.budget_id
                    WHERE b.categorie = ? AND b.utilisateur_id = ?
                    ORDER BY d.date_depense DESC
                    LIMIT 1
                )";

                    using (OdbcCommand cmdDepense = new OdbcCommand(updateDepenseQuery, connection))
                    {
                        cmdDepense.Parameters.AddWithValue("", Convert.ToDecimal(txtDepenseActuelle.Text));
                        cmdDepense.Parameters.AddWithValue("", categorie);
                        cmdDepense.Parameters.AddWithValue("", utilisateur.Id);

                        int rowsAffected = cmdDepense.ExecuteNonQuery();

                        // Optionnel : si aucune ligne n’a été modifiée, insérer une nouvelle dépense
                        if (rowsAffected == 0)
                        {
                            // Rechercher l’ID du budget pour cette catégorie
                            string getBudgetIdQuery = "SELECT id FROM budget WHERE categorie = ? AND utilisateur_id = ?";
                            using (OdbcCommand cmdGetId = new OdbcCommand(getBudgetIdQuery, connection))
                            {
                                cmdGetId.Parameters.AddWithValue("", categorie);
                                cmdGetId.Parameters.AddWithValue("", utilisateur.Id);
                                object result = cmdGetId.ExecuteScalar();

                                if (result != null)
                                {
                                    int budgetId = Convert.ToInt32(result);

                                    string insertDepenseQuery = "INSERT INTO depense (budget_id, montant, date_depense) VALUES (?, ?, CURRENT_DATE)";
                                    using (OdbcCommand cmdInsert = new OdbcCommand(insertDepenseQuery, connection))
                                    {
                                        cmdInsert.Parameters.AddWithValue("", budgetId);
                                        cmdInsert.Parameters.AddWithValue("", Convert.ToDecimal(txtDepenseActuelle.Text));
                                        cmdInsert.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }

                    MessageBox.Show("Budget et dépense mis à jour !");
                    this.Close();
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
