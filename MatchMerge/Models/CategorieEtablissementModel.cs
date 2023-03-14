using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class CategorieEtablissementModel
    {
        [Key]
        public string IdCategorieEtablissement { get; set; }
        public string LibelleCategorieEtablissement { get; set; }
    }
}
