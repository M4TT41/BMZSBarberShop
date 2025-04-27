using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Szemely
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Telefonszam { get; set; }
        public string Email { get; set; }
    }
}