using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Foglalas
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Szolgaltatas")]
        public int SzolgaltatasId { get; set; }
        public virtual Szolgaltatas Szolgaltatas { get; set; }

        [ForeignKey("Vasarlo")]
        public int VasarloId { get; set; }
        public virtual Vasarlo Vasarlo { get; set; }

        [ForeignKey("Naptar")]
        public int NaptarId { get; set; }
        public virtual Naptar Naptar { get; set; }
    }
}