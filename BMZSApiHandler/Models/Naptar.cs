using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Naptar
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int FodraszId { get; set; }
        public virtual Fodrasz Fodrasz { get; set; }
        public ICollection<Foglalas> Foglalasok { get; set; }
    }
}