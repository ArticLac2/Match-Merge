using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatchMerge.Models
{
    public class UtilisateurModel
    {
        [Key]
        public string IdUtilisateur { get; set; }
        public string NomUtilisateur { get; set; }
        public string EMail { get; set; }
        public string MotDePasse { get; set; }
        [Key]
        public string IdRole { get; set; }
        [Key]
        public string IdVisiteur { get; set; }
        [Key]
        public string IdManager { get; set; }
        public bool Suspendu { get; set; }

        public string NomRole { get; set; }
        public string NomVisiteur { get; set; }
        public string NomManager { get; set; }
        public string NomDossierDocuments { get; set; }
        public string IdEquipeVisiteur { get; set; }
        public string IdEquipeManager { get; set; }
        public string IdEquipeManagerOperationTrade { get; set; }
        public string IdEquipeVisiteurOperationTrade { get; set; }
        public string LibelleEquipe { get; set; }
        public string IdEquipe { get; set; }
    }
}
