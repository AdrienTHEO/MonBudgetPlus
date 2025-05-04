using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBudget
{
    class Utilisateur
    {
        public int Id { get; set; }
        public string NomComplet { get; set; }
        public string MotDePasse { get; set; }
        public string ConfirmationMotDePasse { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Pays { get; set; }
         
    }
}
