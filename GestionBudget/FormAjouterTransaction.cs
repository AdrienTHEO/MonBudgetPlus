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
    public partial class FormAjouterTransaction : Form
    {
        private ClassUtilisateur utilisateur;

       
            public FormAjouterTransaction(ClassUtilisateur  user)
        {
            InitializeComponent();
            this.utilisateur = user;
            
            cbxType.Items.AddRange(new string[] { "revenu", "depense" });

            cbxType.SelectedIndex = 0; // Pour déclencher l'affichage correct des champs

            txtCategorie.Visible = true;
            cbxCategorieExistante.Visible = false;





        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string type = cbxType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Veuillez sélectionner un type de transaction.");
                return;
            }

            string categorie = (type == "revenu") ? txtCategorie.Text.Trim() : cbxCategorieExistante.SelectedItem?.ToString();
            decimal montant;
            DateTime date = dtpDate.Value;

            // Validation
            if (string.IsNullOrEmpty(categorie))
            {
                MessageBox.Show("Veuillez saisir une catégorie valide.");
                return;
            }

            if (!decimal.TryParse(txtMontant.Text.Trim(), out montant) || montant <= 0)
            {
                MessageBox.Show("Veuillez saisir un montant valide supérieur à zéro.");
                return;
            }

            if (type == "depense" && cbxCategorieExistante.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une catégorie existante pour la dépense.");
                return;
            }

            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();
                OdbcTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Ajouter dans la table transaction
                    string insertTransaction = @"
                INSERT INTO transaction (utilisateur_id, montant, categorie, type, date_transaction)
                VALUES (?, ?, ?, ?, ?)";

                    using (OdbcCommand cmd = new OdbcCommand(insertTransaction, connection, transaction))
                    {
                        cmd.Parameters.Add("@utilisateur_id", OdbcType.Int).Value = utilisateur.Id;
                        cmd.Parameters.Add("@montant", OdbcType.Decimal).Value = montant;
                        cmd.Parameters.Add("@categorie", OdbcType.VarChar, 255).Value = categorie;
                        cmd.Parameters.Add("@type", OdbcType.VarChar, 50).Value = type;
                        cmd.Parameters.Add("@date_transaction", OdbcType.DateTime).Value = date;
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Si revenu : créer un budget
                    if (type == "revenu")
                    {
                        string insertBudget = "INSERT INTO budget (utilisateur_id, categorie, budget_defini) VALUES (?, ?, ?)";
                        using (OdbcCommand cmd = new OdbcCommand(insertBudget, connection, transaction))
                        {
                            cmd.Parameters.Add("@utilisateur_id", OdbcType.Int).Value = utilisateur.Id;
                            cmd.Parameters.Add("@categorie", OdbcType.VarChar, 255).Value = categorie;
                            cmd.Parameters.Add("@budget_defini", OdbcType.Decimal).Value = montant;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    // 3. Si dépense : insérer dans dépense
                    else if (type == "depense")
                    {
                        string queryBudgetId = "SELECT id FROM budget WHERE utilisateur_id = ? AND categorie = ?";
                        int budgetId = -1;

                        using (OdbcCommand cmd = new OdbcCommand(queryBudgetId, connection, transaction))
                        {
                            cmd.Parameters.Add("@utilisateur_id", OdbcType.Int).Value = utilisateur.Id;
                            cmd.Parameters.Add("@categorie", OdbcType.VarChar, 255).Value = categorie;
                            var result = cmd.ExecuteScalar();
                            if (result != null)
                                budgetId = Convert.ToInt32(result);
                        }

                        if (budgetId > 0)
                        {
                            string insertDepense = "INSERT INTO depense (budget_id, montant, date_depense) VALUES (?, ?, ?)";
                            using (OdbcCommand cmd = new OdbcCommand(insertDepense, connection, transaction))
                            {
                                cmd.Parameters.Add("@budget_id", OdbcType.Int).Value = budgetId;
                                cmd.Parameters.Add("@montant", OdbcType.Decimal).Value = montant;
                                cmd.Parameters.Add("@date_depense", OdbcType.DateTime).Value = date;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            throw new Exception("Aucun budget trouvé pour cette catégorie.");
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Transaction ajoutée avec succès.");

                    // Optionnel : Réinitialiser les champs pour nouvelle saisie
                    txtMontant.Clear();
                    if (type == "revenu")
                        txtCategorie.Clear();
                    else
                        cbxCategorieExistante.SelectedIndex = -1;

                    dtpDate.Value = DateTime.Now;

                    // Fermer si tu préfères
                    // this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Erreur lors de l'ajout de la transaction : " + ex.Message);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboTypeTransaction_SelectedIndexChanged(object sender, EventArgs e) 
        {
            string type = cbxType.SelectedItem.ToString();

            if (type == "revenu")
            {
                txtCategorie.Visible = true;
                cbxCategorieExistante.Visible = false;
            }
            else if (type == "depense")
            {
                txtCategorie.Visible = false;
                cbxCategorieExistante.Visible = true;
                ChargerCategoriesExistantes(); // Méthode qui remplit le combo à partir de la table budget
            }
        }
        private void ChargerCategoriesExistantes()
        {
            string connectionString = "DSN=PostgreLocal;";
            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT categorie FROM budget WHERE utilisateur_id = ?";
                    using (OdbcCommand cmd = new OdbcCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@utilisateur_id", utilisateur.Id); // Assure-toi que `utilisateur` est bien défini globalement
                        using (OdbcDataReader reader = cmd.ExecuteReader())
                        {
                            cbxCategorieExistante.Items.Clear();
                            while (reader.Read())
                            {
                                string categorie = reader.GetString(0);
                                cbxCategorieExistante.Items.Add(categorie);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du chargement des catégories : " + ex.Message);
                }
            }
        }

    }
}
