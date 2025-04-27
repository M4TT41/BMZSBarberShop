using BMZSApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace BMZSApiHandler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string ComboBoxSelectedItem { get; set; }
        public int SelectedFodraszat { get; set; }
        public string azonForEverythingButFodraszat { get; set; }

        public HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:44364") };
        public List<Beosztas> GetBeosztas()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Beosztas").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Beosztas>>(result);
                var selectedDataList = new List<Beosztas>();
                foreach (var beosztas in obj)
                {
                    foreach (var fodrasz in GetFodrasz())
                    {
                        if (beosztas.FodraszId == fodrasz.Id)
                        {
                            beosztas.Fodrasz = fodrasz;
                        }
                    }
                    if (beosztas?.Fodrasz?.FodraszatId == SelectedFodraszat)
                    {
                        selectedDataList.Add(beosztas);
                    }
                }

                return selectedDataList;
            }
            return null;
        }
        public List<Fodrasz> GetFodrasz()
        {

            HttpResponseMessage response1 = client.GetAsync($"/api/Fodrasz/1").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Fodrasz>>(result);
                var selectedDataList = new List<Fodrasz>();
                foreach (var fodrasz in obj)
                {
                    if (fodrasz.FodraszatId == SelectedFodraszat)
                    {
                        selectedDataList.Add(fodrasz);
                    }
                }
                return selectedDataList;
            }
            return null;
        }
        public List<Fodraszat> GetFodraszat()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Fodraszat/0").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Fodraszat>>(result);
                return obj;
            }
            return null;
        }
        public List<Foglalas> GetFoglalas()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Foglalas").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Foglalas>>(result);
                var selectedDataList = new List<Foglalas>();
                foreach (var foglalas in obj)
                {
                    foreach (var szolgaltatas in GetSzolgaltatas())
                    {
                        if (szolgaltatas.Id == foglalas.SzolgaltatasId)
                        {
                            foglalas.Szolgaltatas = szolgaltatas;
                        }
                    }

                    foreach (var vasarlo in GetVasarlo())
                    {
                        if (vasarlo.Id == foglalas.VasarloId)
                        {
                            foglalas.Vasarlo = vasarlo;
                        }
                    }
                    foreach (var naptar in GetNaptar())
                    {
                        if (foglalas.NaptarId == naptar.Id)
                        {
                            foglalas.Naptar = naptar;
                        }
                    }
                    foreach (var fodrasz in GetFodrasz())
                    {
                        if (foglalas?.Naptar?.FodraszId == fodrasz.Id)
                        {
                            foglalas.Naptar.Fodrasz = fodrasz;
                        }
                    }
                    if (foglalas?.Naptar?.Fodrasz?.FodraszatId == SelectedFodraszat)
                    {
                        selectedDataList.Add(foglalas);
                    }
                }


                return selectedDataList;
            }
            return null;
        }
        public List<Kepek> GetKepek()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Kepek").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Kepek>>(result);
                var selectedDataList = new List<Kepek>();
                foreach (var kepek in obj)
                {
                    foreach (var fodrasz in GetFodrasz())
                    {
                        if (kepek.FodraszId == fodrasz.Id)
                        {
                            kepek.Fodrasz = fodrasz;
                        }
                    }
                    if (kepek?.Fodrasz?.FodraszatId == SelectedFodraszat)
                    {
                        selectedDataList.Add(kepek);
                    }
                }

                return selectedDataList;
            }
            return null;
        }
        public List<Naptar> GetNaptar()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Naptar").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Naptar>>(result);
                var selectedDataList = new List<Naptar>();
                foreach (var naptar in obj)
                {
                    foreach (var fodrasz in GetFodrasz())
                    {
                        if (naptar.FodraszId == fodrasz.Id)
                        {
                            naptar.Fodrasz = fodrasz;
                        }
                    }
                    if (naptar?.Fodrasz?.FodraszatId == SelectedFodraszat)
                    {
                        selectedDataList.Add(naptar);
                    }
                }

                return selectedDataList;
            }
            return null;
        }
        public List<Szolgaltatas> GetSzolgaltatas()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Szolgaltatas").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Szolgaltatas>>(result);
                return obj;
            }
            return null;
        }
        public List<Varos> GetVaros()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Varos").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Varos>>(result);
                return obj;
            }
            return null;
        }
        public List<Vasarlo> GetVasarlo()
        {
            HttpResponseMessage response1 = client.GetAsync($"/api/Vasarlo").Result;
            if (response1.IsSuccessStatusCode)
            {
                string result = response1.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<Vasarlo>>(result);
                return obj;
            }
            return null;
        }
        

    }
}
