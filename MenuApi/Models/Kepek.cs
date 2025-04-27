using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BMZSApi.Models
{
    public class Kepek : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int FodraszId { get; set; }
        public virtual Fodrasz Fodrasz { get; set; }

        private string _eleresiUt;
        public string EleresiUt
        {
            get => _eleresiUt;
            set
            {
                if (_eleresiUt != value)
                {
                    _eleresiUt = value;
                    OnPropertyChanged(nameof(EleresiUt));
                }
            }
        }
        public string KepNev { get; set; }
        public string Leiras { get; set; }
        public ICollection<Szavazott> Szavazottak { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}