using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Szolgaltatas
    {
        [Key]
        public int Id { get; set; }  
        public string SzolgaltatasNev { get; set; }
        public int Idotartalom { get; set; }
        public int Ar { get; set; }
        public ICollection<Foglalas> Foglalasok { get; set; }
    }
}