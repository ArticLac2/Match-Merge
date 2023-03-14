using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models { 
    public class PaysModel
    {
        [Key]
        public string idPays { get; set; }
        public string LibellePays { get; set; }
    }
}
