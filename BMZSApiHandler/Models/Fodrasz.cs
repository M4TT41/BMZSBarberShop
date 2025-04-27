using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Fodrasz : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string TelefonSzam { get; set; }
        public string Leiras { get; set; }
        private string _pfpName { get; set; }
        public string PfpName
        {
            get => _pfpName; set
            {
                if (_pfpName != value)
                {
                    _pfpName = value;
                    OnPropertyChanged(nameof(PfpName));
                }
            }
        }
        public int FodraszatId { get; set; }
        public virtual Fodraszat Fodraszat { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}