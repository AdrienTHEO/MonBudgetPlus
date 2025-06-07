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
                            txtMontant.Text = ancienMontant.ToString("0.##");
                            dateTimePicker1.Value = ancienneDate;

                            if (type.Equals("Revenu", StringComparison.OrdinalIgnoreCase))
                            {
                                txtCategorie.Visible = true;
                                cbxCategorie.Visible = false;
                                txtCategorie.Text = ancienneCategorie;
                            }
                            else if (type.Equals("Depense", StringComparison.OrdinalIgnoreCase))
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
            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un type de transaction.");
                return;
            }

            string type = cboType.SelectedItem.ToString();
            string categorie;

            if (type.Equals("Revenu", StringComparison.OrdinalIgnoreCase))
                categorie = txtCategorie.Text.Trim();
            else if (type.Equals("Depense", StringComparison.OrdinalIgnoreCase))
                categorie = cbxCategorie.SelectedItem?.ToString();
            else
            {
                MessageBox.Show("Type de transaction invalide.");
                return;
            }

            if (string.IsNullOrEmpty(categorie))
            {
                MessageBox.Show("Veuillez saisir une catégorie valide.");
                return;
            }

            if (!decimal.TryParse(txtMontant.Text, out decimal montant))
            {
                MessageBox.Show("Veuillez saisir un montant valide.");
                return;
            }

            DateTime date = dateTimePicker1.Value;

            string connectionString = "DSN=PostgreLocal;";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                using (OdbcTransaction transaction = connection.BeginTransaction())
                {
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
                            cmd.Parameters.AddWithValue("@id", transactionId);
                            cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Si c'est un revenu, mettre à jour ou insérer dans la table budget
                        if (type.Equals("Revenu", StringComparison.OrdinalIgnoreCase))
                        {
                            string checkBudgetQuery = "SELECT COUNT(*) FROM budget WHERE utilisateur_id = ? AND categorie = ?";
                            int budgetCount = 0;
                            using (OdbcCommand cmd = new OdbcCommand(checkBudgetQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                                cmd.Parameters.AddWithValue("@categorie", categorie);
                                budgetCount = Convert.ToInt32(cmd.ExecuteScalar());
                            }

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
                        else if (type.Equals("Depense", StringComparison.OrdinalIgnoreCase))
                        {
                            string queryBudgetId = "SELECT id FROM budget WHERE utilisateur_id = ? AND categorie = ?";
                            int budgetId = -1;

                            using (OdbcCommand cmd = new OdbcCommand(queryBudgetId, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id);
                                cmd.Parameters.AddWithValue("@categorie", categorie);
                                var result = cmd.ExecuteScalar();
                                if (result != null && int.TryParse(result.ToString(), out int parsedId))
                                    budgetId = parsedId;
                            }

                            if (budgetId > 0)
                            {
                                // Important : On utilise les anciennes valeurs pour WHERE pour cibler la bonne dépense
                                string updateDepense = @"
                            UPDATE depense 
                            SET montant = ?, date_depense = ? 
                            WHERE budget_id = ? AND montant = ? AND date_depense = ?";

                                using (OdbcCommand cmd = new OdbcCommand(updateDepense, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@montant", montant);
                                    cmd.Parameters.AddWithValue("@date_depense", date);
                                    cmd.Parameters.AddWithValue("@budget_id", budgetId);
                                    cmd.Parameters.AddWithValue("@ancienMontant", ancienMontant);
                                    cmd.Parameters.AddWithValue("@ancienneDate", ancienneDate);
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected == 0)
                                    {
                                        // Si aucune dépense mise à jour, on peut insérer une nouvelle dépense
                                        string insertDepense = "INSERT INTO depense (budget_id, montant, date_depense) VALUES (?, ?, ?)";
                                        using (OdbcCommand insertCmd = new OdbcCommand(insertDepense, connection, transaction))
                                        {
                                            insertCmd.Parameters.AddWithValue("@budget_id", budgetId);
                                            insertCmd.Parameters.AddWithValue("@montant", montant);
                                            insertCmd.Parameters.AddWithValue("@date_depense", date);
                                            insertCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Aucun budget trouvé pour cette catégorie.");
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Transaction modifiée avec succès.");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erreur lors de la modification de la transaction : " + ex.Message);
                    }
                }
            }
        }


        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedItem == null) return;

            string selectedType = cboType.SelectedItem.ToString();

            if (selectedType.Equals("Revenu", StringComparison.OrdinalIgnoreCase))
            {
                txtCategorie.Visible = true;
                cbxCategorie.Visible = false;
            }
            else if (selectedType.Equals("Depense", StringComparison.OrdinalIgnoreCase))
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




