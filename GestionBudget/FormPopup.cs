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
    public partial class FormPopup : Form
    {
        public FormPopup(string v)
        {
            InitializeComponent();
        }

        private void FormPopup_Load(object sender, EventArgs e)
        {
            var fadeIn = new Timer { Interval = 20 };
            fadeIn.Tick += (s, ev) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.05;
                else
                    fadeIn.Stop();
            };
            fadeIn.Start();

            var stayTimer = new Timer { Interval = 3000 }; // Visible 3 secondes
            stayTimer.Tick += (s, ev) => { this.Close(); };
            stayTimer.Start();
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FormPopup_Load(this, EventArgs.Empty);
        }
    }
}
