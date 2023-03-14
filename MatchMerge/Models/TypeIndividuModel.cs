using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class TypeIndividuModel
    {
        [Key]
        public string idTypeIndividu { get; set; }
        public string LibelleTypeIndividu { get; set; }
    }
}
