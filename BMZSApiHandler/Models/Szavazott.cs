using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Szavazott
    {
        [Key, Column(Order = 1), ForeignKey("Kepek")]
        public int KepekId { get; set; }
        public virtual Kepek Kepek { get; set; }

        [Key, Column(Order = 2), ForeignKey("Vasarlo")]
        public int VasarloId { get; set; }
        public virtual Vasarlo Vasarlo { get; set; }

        public int Csillag { get; set; }
    }
}