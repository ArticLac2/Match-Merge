using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class DecoupageDetailModel
    {
        [Key]
        public string IdDecoupage { get; set; }
        public string Zone { get; set; }
        public string Departement { get; set; }
        public string Secteur { get; set; }

        [Key]
        public string IdTerritoire { get; set; }
        public string DescriptionTerritoire { get; set; }

        public string Details { get; set; }
        public string LibelleDecoupage { get; set; }
        public string LibelleTerritoire { get; set; }
    }
}
