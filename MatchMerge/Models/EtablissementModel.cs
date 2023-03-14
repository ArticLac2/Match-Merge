using System;
using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class EtablissementModel
    {
        [Key]
        public string IdEtablissement { get; set; }
        public string NomUtilisateur { get; set; }
        public string IdUnivers { get; set; }
        public string IdPays { get; set; }
        public string CodeExterneEtablissement { get; set; }
        public string LibelleEtablissement { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string IdCategorieEtablissement { get; set; }
        public string LibelleCategorieEtablissement { get; set; }
        public string IdTypeEtablissement { get; set; }
        public string LibelleTypeEtablissement { get; set; }
        public string IdSpecialite { get; set; }
        public string IdEtablissmentParent { get; set; }
        public string TelephoneFixe { get; set; }
        public string TelephoneMobile { get; set; }
        public string TelephoneFixe1 { get; set; }
        public string TelephoneMobile1 { get; set; }
        public string AdresseComplete { get; set; }
        public string ListeDesTelephones { get; set; }
        public string Email { get; set; }
        public string FaceBook { get; set; }
        public string SiteWeb { get; set; }
        public string LinkedIn { get; set; }
        public string Instagram { get; set; }
        public string Tweeter { get; set; }
        public string Longitude { get; set; }
        public string latitude { get; set; }
        public bool Active { get; set; }
        public string IdDepartement { get; set; }
        public string Ville { get; set; }

        public string IdDecoupage { get; set; }
        public string IdTerritoire { get; set; }
        public string LibelleSpecialite { get; set; }
        public string LibelleTerritoire { get; set; }
        public string LibelleDecoupage { get; set; }    
        public string LibelleDepartement { get; set; }

        public string Zone { get; set; }
        public string Departement { get; set; }
        public DateTime? DateMiseAJour { get; set; }  
        public bool IsSelectedPlace { get; set; }

    }
}