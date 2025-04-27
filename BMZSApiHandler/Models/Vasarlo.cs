using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Vasarlo
    {
        [Key]
        public int Id { get; set; }
        public string FelhasznaloNev { get; set; }
        public string Nev { get; set; }
        public string TelefonSzam { get; set; }
        public string EMail { get; set; }
        public string ProfilePic { get; set; }
        public byte[] JelszoHash { get; set; }
        public byte[] JelszoSalt { get; set; }
        public ICollection<Szavazott> Szavazottak { get; set; }
        public ICollection<Foglalas> Foglalasok { get; set; }
    }
}