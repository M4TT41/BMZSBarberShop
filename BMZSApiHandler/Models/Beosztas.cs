using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public enum HetNapja
    {
        Hétfo,
        Kedd,
        Szerda,
        Csütörtök,
        Péntek,
        Szombat,
    }
    public class Beosztas
    {
        public int Id { get; set; }
        public int FodraszId { get; set; }
        public virtual Fodrasz Fodrasz { get; set; }
        private DateTime _datum;
        public DateTime Datum
        {
            get => _datum;
            set => _datum = value.Date;
        }
        public string NapNeve { get; set; }
        public TimeSpan Kezdes { get; set; }
        public TimeSpan Vege { get; set; }
    }
}