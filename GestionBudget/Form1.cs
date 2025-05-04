using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBudget
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GBEtudes_Click(object sender, EventArgs e)
        {

        }

        private void panelCercle_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen Pen = new Pen(Color.Blue, 2);
            int size = Math.Min(panelCercle.Width, panelCercle.Height);
            g.DrawEllipse(Pen, 0, 0, size - 1, size - 1);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormConnexion formConnexion = new FormConnexion();
            formConnexion.Show();   // Ouvre le formulaire d’inscription
            this.Hide();              // Optionnel : cache le formulaire actuel

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormInscription formInscription = new FormInscription();
            formInscription.Show();   // Ouvre le formulaire d’inscription
            this.Hide();              // Optionnel : cache le formulaire actuel
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormConctactUs formConctactUs = new FormConctactUs();
            formConctactUs.Show();   // Ouvre la page de conctat
            this.Hide();              // Optionnel : cache la page actuel
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();   // Ouvre la page de conctat
            this.Hide();              // Optionnel : cache la page actuel
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormConnexion form3 = new FormConnexion();
            form3.Show();   // Ouvre la page de conctat
            this.Hide();   // Optionnel : cache la page actuel

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();   // Ouvre le formulaire d’inscription
            this.Hide();              // Optionnel : cache le formulaire actuel
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();   // Ouvre le formulaire d’inscription
            this.Hide();              // Optionnel : cache le formulaire actuel
        }
    }
}
