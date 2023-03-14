using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatchMerge.Models
{
    public class EtablissementTerritoireModel
    {
        [Key]
        public string IdEtablissement { get; set; }
        [Key]
        public string IdDecoupage { get; set; }
        [Key]
        public string IdTerritoire { get; set; }

        public string LibelleTerritoire { get; set; }

        public string LibelleDecoupage { get; set; }

        public string DescriptionTerritoire { get; set; }
    }
}
