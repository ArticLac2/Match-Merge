using System.ComponentModel.DataAnnotations;

namespace MatchMerge.Models
{
    public class DepartementModel
    {
        [Key]
        public string IdDepartement { get; set; }
        public string IdPays { get; set; }
        public string LibelleDepartement { get; set; }
        public string LibellePays { get; set; }
    }
}
