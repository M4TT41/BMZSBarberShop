using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BMZSApi.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace BMZSApiHandler
{
    /// <summary>
    /// Interaction logic for OpenPage.xaml
    /// </summary>
    public partial class OpenPage : MetroWindow
    {
        public class FodraszatValidationModel
        {
            public int Id { get; set; }
            public string FodraszatAzon { get; set; }
        }
        public OpenPage()
        {
            InitializeComponent();
            
            comboBox.ItemsSource = (Application.Current as App).GetFodraszat()?.Select(fodraszat => $"{fodraszat.Id} - {(Application.Current as App).GetVaros().FirstOrDefault(varos=>varos.Id == fodraszat.VarosId)?.TelepulesNev}");
            comboBox.SelectedIndex = 0;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (Application.Current as App).SelectedFodraszat = Convert.ToInt32(comboBox.SelectedItem.ToString().Split('-')[0].Trim(' '));
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:44364") };
            

            string json = JsonConvert.SerializeObject(new FodraszatValidationModel {
                Id = Convert.ToInt32(comboBox.SelectedItem.ToString().Split('-')[0].Trim(' ')),
                FodraszatAzon = azon.Password
            });
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response1 = client.PostAsync($"/api/Fodraszat/authenticate", content).Result;
             if (response1.IsSuccessStatusCode) 
             {
                (Application.Current as App).azonForEverythingButFodraszat = azon.Password;
                ApiHandler AH = new ApiHandler();
                AH.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hibás azonosító!");
                azon.Password = "";
            }
            
            
        }
    }
}
