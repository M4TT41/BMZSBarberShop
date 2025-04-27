using BMZSApi.Models;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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


namespace BMZSApiHandler
{
    /// <summary>
    /// Interaction logic for NewItemWindow.xaml
    /// </summary>
    public partial class NewItemWindow : MetroWindow
    {
        TextBox nev, telefonSzam, leiras, pfpName, utca, hazszam, fodraszatok, eleresiut, kepnev, szolgaltatasNev, idotartalom, ar, iranyitoszam, telepulesNev, fodraszatAzon;
        ComboBox varosok, szolgaltatasok, vasarlok, naptar, fodraszok, napNeve;
        MahApps.Metro.Controls.DateTimePicker datetime;
        DatePicker datum;
        TimePicker kezdes, vege;
        Button filePickerButton;
        StackPanel stackPanel;
        public class FodraszatPostModel
        {
            public int VarosId { get; set; }
            public string Utca { get; set; }
            public int HazSzam { get; set; }
            public string FodraszatAzon { get; set; }
        }
        private void OpenFilePicker(TextBox eleresiut)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a file",
                Filter = "Images (*.jpg;*.png;)|*.jpg;*.png"  
            };

            if (openFileDialog.ShowDialog() == true)
            {
                eleresiut.Text = openFileDialog.FileName;
            }
        }
        public NewItemWindow()
        {
            InitializeComponent();

            this.Topmost = true;
            if ((Application.Current as App).ComboBoxSelectedItem == "Beosztás")
            {
                newdata.Content = "ÚJ FODRÁSZ ADATAI";
                var FodraszNevList = new List<string>();

                foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                {
                    FodraszNevList.Add(fodrasz.Nev);
                }

                List<string> NapokList = new List<string> { "Hétfo", "Kedd", "Szerda", "Csütörtök", "Péntek", "Szombat" };

                fodraszok = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = FodraszNevList,
                    Width = 180,
                    SelectedIndex = 0
                };

                Grid.SetRow(fodraszok, 1);
                Grid.SetColumn(fodraszok, 1);

                datum = new DatePicker
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    SelectedDate = DateTime.Today,
                };

                Grid.SetRow(datum, 2);
                Grid.SetColumn(datum, 1);

                napNeve = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = NapokList,
                    Width = 180,
                    SelectedIndex = 0
                };

                Grid.SetRow(napNeve, 3);
                Grid.SetColumn(napNeve, 1);


                kezdes = new TimePicker
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    IsClockVisible = true,
                };

                Grid.SetRow(kezdes, 4);
                Grid.SetColumn(kezdes, 1);


                vege = new TimePicker
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    IsClockVisible = true,
                };

                Grid.SetRow(vege, 5);
                Grid.SetColumn(vege, 1);



                mainGrid.Children.Add(fodraszok);
                mainGrid.Children.Add(datum);
                mainGrid.Children.Add(napNeve);
                mainGrid.Children.Add(kezdes);
                mainGrid.Children.Add(vege);


                // Labels

                firstLabel.Content = "Fodrász:";
                secondLabel.Content = "Dátum:";
                thirdLabel.Content = "Nap:";
                fourthLabel.Content = "Kezdés:";
                fifthLabel.Content = "Vége:";

            }
            if ((Application.Current as App).ComboBoxSelectedItem == "Fodrászok")
            {
                newdata.Content = "ÚJ FODRÁSZ ADATAI";
                nev = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180
                };

                Grid.SetRow(nev, 1);
                Grid.SetColumn(nev, 1);

                telefonSzam = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180
                };

                Grid.SetRow(telefonSzam, 2);
                Grid.SetColumn(telefonSzam, 1);

                leiras = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180
                };

                Grid.SetRow(leiras, 3);
                Grid.SetColumn(leiras, 1);

                pfpName = new TextBox
                {
                    FontSize = 24,
                    Margin = new Thickness(10, 0, 30, 0),
                    FontFamily = new System.Windows.Media.FontFamily("Arial"),
                    Width = 130,
                    IsEnabled = false
                };
                filePickerButton = new Button
                {
                    Content = "📂",
                    FontSize = 24,
                    Margin = new Thickness(0),
                    Width = 40,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                filePickerButton.Click += (sender, e) => OpenFilePicker(pfpName);

                stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                stackPanel.Children.Add(filePickerButton);
                stackPanel.Children.Add(pfpName);

                Grid.SetRow(stackPanel, 4);
                Grid.SetColumn(stackPanel, 1);

                var FodraszatIdList = new List<int>();

                foreach (var fodraszat in (Application.Current as App).GetFodraszat())
                {
                    FodraszatIdList.Add(fodraszat.Id);
                }

                fodraszatok = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    Text = (Application.Current as App).SelectedFodraszat.ToString()
                };

                Grid.SetRow(fodraszatok, 5);
                Grid.SetColumn(fodraszatok, 1);

                fodraszatok.IsEnabled = false;

                mainGrid.Children.Add(nev);
                mainGrid.Children.Add(telefonSzam);
                mainGrid.Children.Add(leiras);
                mainGrid.Children.Add(stackPanel);
                mainGrid.Children.Add(fodraszatok);


                // Labels

                firstLabel.Content = "Név:";
                secondLabel.Content = "Telefonszám:";
                thirdLabel.Content = "Leírás:";
                fourthLabel.Content = "Profilkép neve:";
                fifthLabel.Content = "Fodrászat:";

            }
            if ((Application.Current as App).ComboBoxSelectedItem == "Fodrászatok")
            {
                newdata.Content = "ÚJ FODRÁSZAT ADATAI";

                var TelepulesNevList = new List<string>();

                foreach (var fodraszat in (Application.Current as App).GetVaros())
                {
                    TelepulesNevList.Add(fodraszat.TelepulesNev);
                }

                varosok = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = TelepulesNevList,
                    Width = 180,
                    SelectedIndex = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(varosok, 1);
                Grid.SetColumn(varosok, 1);

                utca = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(utca, 2);
                Grid.SetColumn(utca, 1);

                hazszam = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(hazszam, 3);
                Grid.SetColumn(hazszam, 1);

                fodraszatAzon = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(fodraszatAzon, 4);
                Grid.SetColumn(fodraszatAzon, 1);

                mainGrid.Children.Add(varosok);
                mainGrid.Children.Add(utca);
                mainGrid.Children.Add(hazszam);
                mainGrid.Children.Add(fodraszatAzon);


                // Labels

                firstLabel.Content = "Város:";
                secondLabel.Content = "Utca:";
                thirdLabel.Content = "Házszám:";
                fourthLabel.Content = "Azonosító:";

            }
            if ((Application.Current as App).ComboBoxSelectedItem == "Foglalások")
            {
                newdata.Content = "ÚJ FOGLALÁS ADATAI";

                var SzolgaltatasNevList = new List<string>();

                foreach (var szolgaltatas in (Application.Current as App).GetSzolgaltatas())
                {
                    SzolgaltatasNevList.Add(szolgaltatas.SzolgaltatasNev);
                }

                szolgaltatasok = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = SzolgaltatasNevList,
                    Width = 180,
                    SelectedIndex = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(szolgaltatasok, 1);
                Grid.SetColumn(szolgaltatasok, 1);


                var VasarloNevList = new List<string>();

                foreach (var vasarlo in (Application.Current as App).GetVasarlo())
                {
                    VasarloNevList.Add(vasarlo.Nev);
                }

                vasarlok = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = VasarloNevList,
                    Width = 180,
                    SelectedIndex = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(vasarlok, 2);
                Grid.SetColumn(vasarlok, 1);


                var NaptarDatumList = new List<DateTime>();

                foreach (var naptar in (Application.Current as App).GetNaptar())
                {
                    NaptarDatumList.Add(naptar.Datum);
                }

                naptar = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = NaptarDatumList,
                    Width = 180,
                    SelectedIndex = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(naptar, 3);
                Grid.SetColumn(naptar, 1);


                mainGrid.Children.Add(szolgaltatasok);
                mainGrid.Children.Add(vasarlok);
                mainGrid.Children.Add(naptar);


                // Labels

                firstLabel.Content = "Szolgáltatások:";
                secondLabel.Content = "Vásárlók:";
                thirdLabel.Content = "Dátumok:";

            }
            if ((Application.Current as App).ComboBoxSelectedItem == "Képek")
            {
                newdata.Content = "ÚJ KÉP ADATAI";

                var FodraszNevList = new List<string>();

                foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                {
                    FodraszNevList.Add(fodrasz.Nev);
                }

                fodraszok = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = FodraszNevList,
                    Width = 180,
                    SelectedIndex = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(fodraszok, 1);
                Grid.SetColumn(fodraszok, 1);

                eleresiut = new TextBox
                {
                    FontSize = 24,
                    Margin = new Thickness(10, 0, 30, 0),
                    Width = 130,
                    FontFamily = new System.Windows.Media.FontFamily("Arial"),
                    IsEnabled = false
                };
                filePickerButton = new Button
                {
                    Content = "📂",
                    FontSize = 24,
                    Margin = new Thickness(0),
                    Width = 40,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                filePickerButton.Click += (sender, e) => OpenFilePicker(eleresiut);

                stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                stackPanel.Children.Add(filePickerButton);
                stackPanel.Children.Add(eleresiut);

                Grid.SetRow(stackPanel, 2);
                Grid.SetColumn(stackPanel, 1);

                kepnev = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(kepnev, 3);
                Grid.SetColumn(kepnev, 1);

                leiras = new TextBox
                {
                    Name = "pfpName",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(leiras, 4);
                Grid.SetColumn(leiras, 1);




                mainGrid.Children.Add(fodraszok);
                mainGrid.Children.Add(stackPanel);
                mainGrid.Children.Add(kepnev);
                mainGrid.Children.Add(leiras);


                // Labels

                firstLabel.Content = "Fodrászok:";
                secondLabel.Content = "Ellérési út:";
                thirdLabel.Content = "Kép neve:";
                fourthLabel.Content = "Leírás:";

            }
            if ((Application.Current as App).ComboBoxSelectedItem == "Naptár")
            {
                newdata.Content = "ÚJ NAPTÁR ADATAI";



                datetime = new MahApps.Metro.Controls.DateTimePicker()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    SelectedDateTime = DateTime.Now,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(datetime, 1);
                Grid.SetColumn(datetime, 1);

                var FodraszNevList = new List<string>();

                foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                {
                    FodraszNevList.Add(fodrasz.Nev);
                }

                fodraszok = new ComboBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    ItemsSource = FodraszNevList,
                    Width = 180,
                    SelectedIndex = 0,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(fodraszok, 2);
                Grid.SetColumn(fodraszok, 1);

                mainGrid.Children.Add(datetime);
                mainGrid.Children.Add(fodraszok);


                // Labels

                firstLabel.Content = "Dátum:";
                secondLabel.Content = "Fodrászok:";

            }
            if ((Application.Current as App).ComboBoxSelectedItem == "Szolgáltatások")
            {
                newdata.Content = "ÚJ SZOLGÁTATÁS ADATAI";
                szolgaltatasNev = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(szolgaltatasNev, 1);
                Grid.SetColumn(szolgaltatasNev, 1);

                idotartalom = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(idotartalom, 2);
                Grid.SetColumn(idotartalom, 1);

                ar = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(ar, 3);
                Grid.SetColumn(ar, 1);

                mainGrid.Children.Add(szolgaltatasNev);
                mainGrid.Children.Add(idotartalom);
                mainGrid.Children.Add(ar);


                // Labels

                firstLabel.Content = "Szolgáltatás neve:";
                secondLabel.Content = "Időtartam:";
                thirdLabel.Content = "Ár (Ft):";

            }
            if ((Application.Current as App).ComboBoxSelectedItem == "Város")
            {
                newdata.Content = "ÚJ VÁROS ADATAI";
                iranyitoszam = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(iranyitoszam, 1);
                Grid.SetColumn(iranyitoszam, 1);

                telepulesNev = new TextBox
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 24,
                    Margin = new Thickness(0, 0, 30, 0),
                    Width = 180,
                    FontFamily = new System.Windows.Media.FontFamily("Arial")
                };

                Grid.SetRow(telepulesNev, 2);
                Grid.SetColumn(telepulesNev, 1);

                mainGrid.Children.Add(iranyitoszam);
                mainGrid.Children.Add(telepulesNev);


                // Labels

                firstLabel.Content = "Irányítószám:";
                secondLabel.Content = "Település neve:";

            }

        }
        private async void AddData<T>(T newData)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:44364");

                    if(newData is FodraszatPostModel fodraszat)
                    {
                        if (string.IsNullOrEmpty(fodraszat.FodraszatAzon))
                        {
                            MessageBox.Show(this, "Fodrászat azonosító nem lehet üres!");
                            return;
                        }
                    }

                    string json = JsonConvert.SerializeObject(newData);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = null;
                    switch (newData)
                    {
                        case Beosztas _: response = await client.PostAsync($"api/Beosztas", content); break;
                        case Fodrasz _: response = await client.PostAsync($"api/Fodrasz/1", content); break;
                        case FodraszatPostModel _: response = await client.PostAsync($"api/Fodraszat/0", content); break;
                        case Foglalas _: response = await client.PostAsync($"api/Foglalas", content); break;
                        case Kepek _: response = await client.PostAsync($"api/Kepek", content); break;
                        case Naptar _: response = await client.PostAsync($"api/Naptar", content); break;
                        case Szolgaltatas _: response = await client.PostAsync($"api/Szolgaltatas", content); break;
                        case Varos _: response = await client.PostAsync($"api/Varos", content); break;
                        case Vasarlo _: response = await client.PostAsync($"api/Vasarlo", content); break;
                    }

                    if (response.IsSuccessStatusCode) MessageBox.Show(this,"Sikeres új adat létrehozása!");
                    else MessageBox.Show(this, "Az új adat létrehozása sikertelen.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in mainGrid.Children)
            {
                switch (item)
                {
                    case TextBox textBox: textBox.Text = ""; break;
                    case ComboBox comboBox: comboBox.SelectedIndex = 0; break;
                    case DatePicker datePicker: datePicker.SelectedDate = DateTime.Now; break;
                    case TimePicker timePicker: timePicker.SelectedDateTime = DateTime.Now; break;
                }
            }
            this.Close();
        }

        private void finalAdd_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                switch ((Application.Current as App).ComboBoxSelectedItem)
                {
                    case "Beosztás": AddData(new Beosztas
                    {
                        Fodrasz = (Application.Current as App).GetFodrasz().Where(x => x.Nev == fodraszok.SelectedItem.ToString()).FirstOrDefault(),
                        FodraszId = (Application.Current as App).GetFodrasz().Where(x => x.Nev == fodraszok.SelectedItem.ToString()).Select(x=>x.Id).FirstOrDefault(),
                        Datum = datum.SelectedDate ?? DateTime.Today,
                        NapNeve = napNeve.SelectedItem.ToString(),
                        Kezdes = kezdes.SelectedDateTime?.TimeOfDay ?? TimeSpan.Zero,
                        Vege = vege.SelectedDateTime?.TimeOfDay ?? TimeSpan.Zero,

                    });break;
                    case "Fodrászok":
                        AddData(new Fodrasz
                        {
                            Nev = nev.Text,
                            TelefonSzam = telefonSzam.Text,
                            Leiras = leiras.Text,
                            PfpName = pfpName.Text,
                            FodraszatId = Convert.ToInt32(fodraszatok.Text)
                        }); break;

                    case "Fodrászatok":
                        PasswordHasher.CreatePasswordHash(fodraszatAzon.Text, out byte[] passwordHash, out byte[] passwordSalt);
                        AddData(new FodraszatPostModel
                        {
                            VarosId = (Application.Current as App).GetVaros().Where(x => x.TelepulesNev == varosok.SelectedItem.ToString()).Select(x => x.Id).FirstOrDefault(),
                            Utca = utca.Text,
                            HazSzam = Convert.ToInt32(hazszam.Text),
                            FodraszatAzon = fodraszatAzon.Text

                            
                        }); break;

                    case "Foglalások":
                        AddData(new Foglalas
                        {
                            Szolgaltatas = (Application.Current as App).GetSzolgaltatas().Where(x => x.SzolgaltatasNev == szolgaltatasok.SelectedItem.ToString()).FirstOrDefault(),
                            SzolgaltatasId = (Application.Current as App).GetSzolgaltatas().Where(x => x.SzolgaltatasNev == szolgaltatasok.SelectedItem.ToString()).Select(x => x.Id).FirstOrDefault(),
                            Vasarlo = (Application.Current as App).GetVasarlo().Where(x => x.Nev == vasarlok.SelectedItem.ToString()).FirstOrDefault(),
                            VasarloId = (Application.Current as App).GetVasarlo().Where(x => x.Nev == vasarlok.SelectedItem.ToString()).Select(x => x.Id).FirstOrDefault(),
                            Naptar = (Application.Current as App).GetNaptar().Where(x => x.Datum == DateTime.Parse(naptar.SelectedItem.ToString())).FirstOrDefault(),
                            NaptarId = (Application.Current as App).GetNaptar().Where(x => x.Datum == DateTime.Parse(naptar.SelectedItem.ToString())).Select(x => x.Id).FirstOrDefault(),
                        }); break;
                    case "Képek":
                        AddData(new Kepek
                        {
                            Fodrasz = (Application.Current as App).GetFodrasz().Where(x => x.Nev == fodraszok.SelectedItem.ToString()).FirstOrDefault(),
                            FodraszId = (Application.Current as App).GetFodrasz().Where(x => x.Nev == fodraszok.SelectedItem.ToString()).Select(x => x.Id).FirstOrDefault(),
                            EleresiUt = eleresiut.Text,
                            KepNev = kepnev.Text,
                            Leiras = leiras.Text,
                        }); break;
                    case "Naptár":
                        AddData(new Naptar
                        {
                            Datum = datetime.SelectedDateTime ?? DateTime.Today,
                            Fodrasz = (Application.Current as App).GetFodrasz().Where(x => x.Nev == fodraszok.SelectedItem.ToString()).FirstOrDefault(),
                            FodraszId = (Application.Current as App).GetFodrasz().Where(x => x.Nev == fodraszok.SelectedItem.ToString()).Select(x => x.Id).FirstOrDefault(),
                        }); break;
                    case "Szolgáltatások":
                        AddData(new Szolgaltatas
                        {
                            SzolgaltatasNev = szolgaltatasNev.Text,
                            Idotartalom = Convert.ToInt32(idotartalom.Text),
                            Ar = Convert.ToInt32(ar.Text)
                        }); break;
                    case "Város":
                        AddData(new Varos
                        {
                            IranyitoSzam = Convert.ToInt32(iranyitoszam.Text),
                            TelepulesNev = telepulesNev.Text
                        }); break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
