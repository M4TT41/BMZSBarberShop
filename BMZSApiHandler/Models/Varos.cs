using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Varos
    {
        [Key]
        public int Id { get; set; }
        public int IranyitoSzam { get; set; }
        public string TelepulesNev { get; set; }
    }
}