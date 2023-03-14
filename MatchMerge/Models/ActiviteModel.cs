using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class ActiviteModel
    {
        [Key]
        public string IdActivite { get; set; }
        public string IdIndividu { get; set; }
        public string IdEtablissement { get; set; }
        public bool ActivitePrincipale { get; set; }

        public DateTime DateDebutActivite { get; set; }
        public DateTime? DateFinActivite { get; set; }
        public bool Active { get; set; }

        public string NomIndividu { get; set; }
        public string LibelleEtablissement { get; set; }

        public string IdSpecialite { get; set; }
        public string IdTypeIndividu { get; set; }
        public string IdTypeEtablissement { get; set; }
        public string IdDepartement { get; set; }

        public string IdSegment { get; set; }
        public string LibelleSegment { get; set; }
        public string ProduitPresente { get; set; }
        public string LibelleCanal { get; set; }
        public string LibelleEquipe { get; set; }
        public string LibelleDeparement { get; set; }

        public string IdDecoupage { get; set; }
        public string IdTerritoire { get; set; }

        public string LibelleSpecialite { get; set; }
        public string LibelleTerritoire { get; set; }
        public string LibelleDecoupage { get; set; }

        public string Zone { get; set; }
        public string Departement { get; set; }

        public string IdCritereIndividu { get; set; }
        public string ValeurCritereIndividu { get; set; }
        public string IdCritereEtablissement { get; set; }
        public string ValeurCritereEtablissement { get; set; }
        public string IdCritereActivite { get; set; }
        public string ValeurCritereActivite { get; set; }
        public string AdresseComplete { get; set; }
        public string ListeDesTelephones { get; set; }
        public string DateMiseAJour { get; set; }
        public DateTime DateSegment { get; set; }
        public IEnumerable<string> IdTypeEtablissementMultiple { get; set; }

        public string LibellePanel { get; set; }

        public string IdCanal { get; set; }
        public bool IsChecked { get; set; }

    }
}
