using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class IndividuModel
    {
        [Key]
        public string IdIndividu { get; set; }
        public string IdUnivers { get; set; }
        public string IdPays { get; set; }
        public string IdDepartement { get; set; }
        public string CodeExterneIndividu { get; set; }
        public string Civilite { get; set; }
        public string Titre { get; set; }
        public string NomIndividu { get; set; }
        public string Adresse { get; set; }
        public string IdSpecialite { get; set; }
        public string IdSpecialiteSecondaire { get; set; }
        public string IdTypeIndividu { get; set; }
        public string CodePostal { get; set; }
        public string TelephoneFixe { get; set; }
        public string TelephoneMobile { get; set; }
        public string TelephoneFixe1 { get; set; }
        public string TelephoneMobile1 { get; set; }
        public string Email { get; set; }
        public string FaceBook { get; set; }
        public string SiteWeb { get; set; }
        public string LinkedIn { get; set; }
        public string Instagram { get; set; }
        public string Tweeter { get; set; }
        public string Longitude { get; set; }
        public string latitude { get; set; }
        public bool Active { get; set; }
        public string Ville { get; set; }
        public string UpdatedBy { get; set; }

        public string LibelleEtablissement { get; set; }
        public string LibelleSpecialite { get; set; }
    }
}
