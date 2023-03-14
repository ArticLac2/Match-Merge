using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class UniverModel
    {
        [Key]
        public string IdUnivers { get; set; }
        public string LibelleUnivers { get; set; }
    }
}
