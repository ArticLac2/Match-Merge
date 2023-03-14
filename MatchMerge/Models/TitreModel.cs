using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class TitreModel
    {
        [Key]
        public string IdTitre { get; set; }
        public string LibelleTitre { get; set; }
    }
}
