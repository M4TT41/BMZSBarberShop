using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Fodraszat
    {
        [Key]
        public int Id { get; set; }
        public int VarosId { get; set; }
        public virtual Varos Varos { get; set; }
        public string Utca { get; set; }
        public int HazSzam { get; set; }
        public byte[] FodraszatAzonHash { get; set; }
        public byte[] FodraszatAzonSalt { get; set; }
    }
}