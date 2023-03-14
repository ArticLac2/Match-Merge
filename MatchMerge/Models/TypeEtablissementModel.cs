using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class TypeEtablissementModel
    {
        [Key]
        public string IdTypeEtablissement { get; set; }
        public string LibelleTypeEtablissement { get; set; }
    }
}
