using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class SpecialiteModel
    {
        [Key]
        public string IdSpecialite { get; set; }
        public string LibelleSpecialite { get; set; }
        public bool check { get; set; }
}
}
