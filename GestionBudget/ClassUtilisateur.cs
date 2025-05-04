using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBudget
{
    public class ClassUtilisateur
    {
        public int Id { get; set; }
        public string NomComplet { get; set; }
        public string MotDePasse { get; set; } // Juste si tu en as besoin (ex : pour le modifier)
        public DateTime DateNaissance { get; set; }
        public string Pays { get; set; }
        public byte[] Photo { get; set; }
    }
}



