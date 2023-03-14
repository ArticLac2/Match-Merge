using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class DecoupageModel
    {
        [Key]
        public string IdDecoupage { get; set; }
        public string LibelleDecoupage { get; set; }
        public string idPays { get; set; }
        public string LibellePays { get; set; }
    }
}
