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
    public partial class FormModifierTransaction : Form
    {
        private int transactionId;
        private ClassUtilisateur utilisateur;
        private string ancienType;
        private string ancienneCategorie;
        private decimal ancienMontant;
        private DateTime ancienneDate;

        public FormModifierTransaction(int transactionId, ClassUtilisateur user)
        {
            InitializeComponent();
            this.transactionId = transactionId;
            this.utilisateur = user;

            cboType.Items.Add("Revenu");
            cboType.Items.Add("Depense");

            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged;

            ChargerTransactionExistante();
        }

        private void ChargerTransactionExistante()
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM transaction WHERE id = ? AND utilisateur_id = ?";
                using (OdbcCommand command = new OdbcCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", transactionId);
                    command.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);

                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string type = reader["type"].ToString();
                            ancienType = type;
                            ancienneCategorie = reader["categorie"].ToString();
                            ancienMontant = Convert.ToDecimal(reader["montant"]);
                            ancienneDate = Convert.ToDateTime(reader["date_transaction"]);

                            cboType.SelectedItem = type;
                            txtMontant.Text = ancienMontant.ToString();
                            dateTimePicker1.Value = ancienneDate;

                            if (type == "Revenu")
                            {
                                txtCategorie.Visible = true;
                                cbxCategorie.Visible = false;
                                txtCategorie.Text = ancienneCategorie;
                            }
                            else if (type == "Depense")
                            {
                                txtCategorie.Visible = false;
                                cbxCategorie.Visible = true;
                                ChargerCategoriesExistantes();
                                cbxCategorie.SelectedItem = ancienneCategorie;
                            }
                        }
                    }
                }
            }
        }

        private void ChargerCategoriesExistantes()
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT categorie FROM budget WHERE utilisateur_id = ?";
                using (OdbcCommand command = new OdbcCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        cbxCategorie.Items.Clear();
                        while (reader.Read())
                        {
                            cbxCategorie.Items.Add(reader["categorie"].ToString());
                        }
                    }
                }
            }
        }

        private void btnModifierTransaction_Click(object sender, EventArgs e)
        {
            // Récupère les informations du formulaire
            string type = cboType.SelectedItem.ToString();  // "Revenu" ou "Dépense"
            string categorie = (type == "revenu") ? txtCategorie.Text : cbxCategorie.SelectedItem?.ToString();
            decimal montant;
            DateTime date = dateTimePicker1.Value;

            // Validation des données
            if (string.IsNullOrEmpty(categorie) || !decimal.TryParse(txtMontant.Text, out montant))
            {
                MessageBox.Show("Veuillez saisir toutes les informations valides.");
                return;
            }

            string connectionString = "DSN=PostgreLocal;";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                OdbcTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Mettre à jour la transaction dans la table transaction
                    string updateTransaction = @"
                UPDATE transaction 
                SET montant = ?, categorie = ?, type = ?, date_transaction = ?
                WHERE id = ? AND utilisateur_id = ?";

                    using (OdbcCommand cmd = new OdbcCommand(updateTransaction, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@montant", montant);
                        cmd.Parameters.AddWithValue("@categorie", categorie);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@date_transaction", date);
                        cmd.Parameters.AddWithValue("@id", transactionId);  // transactionId venant du formulaire
                        cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Si c'est un revenu, mettre à jour ou insérer dans la table budget
                    if (type == "revenu")
                    {
                        // Vérifie si un budget pour la catégorie existe déjà
                        string checkBudgetQuery = "SELECT COUNT(*) FROM budget WHERE utilisateur_id = ? AND categorie = ?";
                        int budgetCount = 0;
                        using (OdbcCommand cmd = new OdbcCommand(checkBudgetQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                            cmd.Parameters.AddWithValue("@categorie", categorie);
                            budgetCount = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Si le budget existe déjà, on met à jour le budget existant
                        if (budgetCount > 0)
                        {
                            string updateBudget = "UPDATE budget SET budget_defini = ? WHERE utilisateur_id = ? AND categorie = ?";
                            using (OdbcCommand cmd = new OdbcCommand(updateBudget, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@budget_defini", montant);
                                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                                cmd.Parameters.AddWithValue("@categorie", categorie);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        // Si le budget n'existe pas, on en crée un nouveau
                        else
                        {
                            string insertBudget = "INSERT INTO budget (utilisateur_id, categorie, budget_defini) VALUES (?, ?, ?)";
                            using (OdbcCommand cmd = new OdbcCommand(insertBudget, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                                cmd.Parameters.AddWithValue("@categorie", categorie);
                                cmd.Parameters.AddWithValue("@budget_defini", montant);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // 3. Si c'est une dépense, mettre à jour ou insérer dans la table depense
                    else if (type == "depense")
                    {
                        // Trouver l'id du budget associé à la catégorie
                        string queryBudgetId = "SELECT id FROM budget WHERE utilisateur_id = ? AND categorie = ?";
                        int budgetId = -1;

                        using (OdbcCommand cmd = new OdbcCommand(queryBudgetId, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                            cmd.Parameters.AddWithValue("@categorie", categorie);
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                                budgetId = Convert.ToInt32(result);
                        }

                        if (budgetId > 0)
                        {
                            string updateDepense = "UPDATE depense SET montant = ?, date_depense = ? WHERE budget_id = ? AND montant = ? AND date_depense = ?";
                            using (OdbcCommand cmd = new OdbcCommand(updateDepense, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@montant", montant);
                                cmd.Parameters.AddWithValue("@date_depense", date);
                                cmd.Parameters.AddWithValue("@budget_id", budgetId);
                                cmd.Parameters.AddWithValue("@ancienMontant", montant);  // Ancien montant, si nécessaire
                                cmd.Parameters.AddWithValue("@ancienneDate", date);      // Ancienne date, si nécessaire
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            throw new Exception("Aucun budget trouvé pour cette catégorie.");
                        }
                    }

                    // Commit des transactions
                    transaction.Commit();
                    MessageBox.Show("Transaction modifiée avec succès.");
                    this.Close();  // Ferme le formulaire
                }
                catch (Exception ex)
                {
                    // Rollback en cas d'erreur
                    transaction.Rollback();
                    MessageBox.Show("Erreur lors de la modification de la transaction : " + ex.Message);
                }
            }
        }


        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedItem?.ToString() == "Revenu")
            {
                txtCategorie.Visible = true;
                cbxCategorie.Visible = false;
            }
            else if (cboType.SelectedItem?.ToString() == "Depense")
            {
                txtCategorie.Visible = false;
                cbxCategorie.Visible = true;
                ChargerCategoriesExistantes();
            }
        }
        private void dateTimePickerTransaction_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormModifierTransaction_Load(object sender, EventArgs e)
        {

        }
    }


}




