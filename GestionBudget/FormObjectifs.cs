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
    public partial class FormObjectifs : Form
    {
        private ClassUtilisateur utilisateur;
        public FormObjectifs( ClassUtilisateur utilisateur)
        {
            InitializeComponent();
            this.utilisateur = utilisateur;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            using var conn = new OdbcConnection("DSN=PostgreLocal;");
            conn.Open();
            string insert = @"
    INSERT INTO objectif (utilisateur_id, titre, description, montant_objectif)
    VALUES (?, ?, ?, ?)";
            using var cmd = new OdbcCommand(insert, conn);
            cmd.Parameters.AddWithValue("@userId", utilisateur.Id);
            cmd.Parameters.AddWithValue("@titre", txtTitre.Text);
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
            cmd.Parameters.AddWithValue("@montant", numMontantObjectif.Value);
            cmd.ExecuteNonQuery();

        }
    }
}
