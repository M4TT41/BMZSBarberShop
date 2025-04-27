using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BMZSApi.Models;
using System.Runtime.InteropServices;
using System.Collections;
using System.Security.AccessControl;
using System.Xml.Linq;
using System.ComponentModel;
using Org.BouncyCastle.Utilities;
using MahApps.Metro.Controls;
using System.Web.UI.WebControls;
using Xceed.Wpf.Toolkit;
using Org.BouncyCastle.Utilities.Encoders;
using BMZSApi.Models;
using TextBox = System.Windows.Controls.TextBox;
using System.Globalization;
using static BMZSApiHandler.OpenPage;
using Renci.SshNet.Security;


namespace BMZSApiHandler
{
    /// <summary>
    /// Interaction logic for ApiHandler.xaml
    /// </summary>
    /// 

    public partial class ApiHandler : MetroWindow
    {
        HttpClient client = new HttpClient();
        private class FodraszatHelpModel
        {
            public int Id { get; set; }
            public int VarosId { get; set; }
            public Varos Varos { get; set; }
            public string Utca { get; set; }
            public int HazSzam { get; set; }
            public string FodraszatAzon { get; set; }
            public string NewFodraszatAzon { get; set; }
        }
        public ApiHandler()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:44364");

            comboBox.Items.Add("Beosztás");
            comboBox.Items.Add("Fodrászok");
            comboBox.Items.Add("Fodrászatok");
            comboBox.Items.Add("Foglalások");
            comboBox.Items.Add("Képek");
            comboBox.Items.Add("Naptár");
            comboBox.Items.Add("Szolgáltatások");
            comboBox.Items.Add("Város");
            comboBox.Items.Add("Vásárló");

            comboBox.SelectedIndex = 0;
            (Application.Current as App).ComboBoxSelectedItem = comboBox.SelectedItem.ToString();

            dataGrid.CanUserDeleteRows = false;
            dataGrid.AutoGenerateColumns = false;
            dataGrid.CanUserAddRows = false;
        }

        public void Get()
        {
            dataGrid.Columns.Clear();
            if (comboBox.SelectedItem.ToString() == "Beosztás")
            {
                var beosztasList = (Application.Current as App).GetBeosztas();
                HashSet<string> FodraszNevHash = new HashSet<string>();

                foreach (var beosztas in beosztasList)
                {
                    foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                    {
                        FodraszNevHash.Add(fodrasz.Nev);
                        if (fodrasz.Id == beosztas.FodraszId)
                        {
                            beosztas.Fodrasz = fodrasz;
                        }
                    }
                }

                /*---------------------------------------------------------------------------------------------------------------------*/

                List<string> Napok = new List<string> { "Hétfo", "Kedd", "Szerda", "Csütörtök", "Péntek", "Szombat" };

                /*---------------------------------------------------------------------------------------------------------------------*/
                var dateColumn = new DataGridTemplateColumn
                {
                    Header = "Kezdés dátuma",
                    SortMemberPath = "Datum",
                    CellTemplate = new DataTemplate(),
                    CellEditingTemplate = new DataTemplate()
                };

                FrameworkElementFactory datePickerFactory = new FrameworkElementFactory(typeof(DatePicker));
                datePickerFactory.SetBinding(DatePicker.SelectedDateProperty, new Binding("Datum")
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });
                datePickerFactory.SetValue(DatePicker.DisplayDateStartProperty, DateTime.Today); // restrict past dates
                datePickerFactory.SetValue(DatePicker.SelectedDateFormatProperty, DatePickerFormat.Short);

                var dateTemplate = new DataTemplate();
                dateTemplate.VisualTree = datePickerFactory;

                dateColumn.CellTemplate = dateTemplate;
                dateColumn.CellEditingTemplate = dateTemplate;

                /*---------------------------------------------------------------------------------------------------------------------*/

                var kezdesColumn = new DataGridTemplateColumn
                {
                    Header = "Kezdés",
                    CellTemplate = new DataTemplate(),
                    CellEditingTemplate = new DataTemplate()
                };
                var timeConverter = new TimeSpanToDateTimeConverter();

                var timePickerFactory = new FrameworkElementFactory(typeof(MahApps.Metro.Controls.TimePicker));
                timePickerFactory.SetBinding(MahApps.Metro.Controls.TimePicker.SelectedDateTimeProperty, new Binding("Kezdes")
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Converter = timeConverter
                });
                //timePickerFactory.SetValue(Xceed.Wpf.Toolkit.TimePicker.IsVisibleProperty, true); // keep UI clear

                // Apply the editing template
                var editingTemplate = new DataTemplate();
                editingTemplate.VisualTree = timePickerFactory;
                kezdesColumn.CellEditingTemplate = editingTemplate;

                // For display, just use a TextBlock that shows the time
                var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Kezdes")
                {
                    StringFormat = @"hh\:mm"
                });
                var displayTemplate = new DataTemplate();
                displayTemplate.VisualTree = textBlockFactory;
                kezdesColumn.CellTemplate = displayTemplate;

                /*---------------------------------------------------------------------------------------------------------------------*/

                var vegeColumn = new DataGridTemplateColumn
                {
                    Header = "Vége",
                    CellTemplate = new DataTemplate(),
                    CellEditingTemplate = new DataTemplate()
                };
                timeConverter = new TimeSpanToDateTimeConverter();

                timePickerFactory = new FrameworkElementFactory(typeof(MahApps.Metro.Controls.TimePicker));
                timePickerFactory.SetBinding(MahApps.Metro.Controls.TimePicker.SelectedDateTimeProperty, new Binding("Vege")
                {
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    Converter = timeConverter
                });
                //timePickerFactory.SetValue(Xceed.Wpf.Toolkit.TimePicker.IsVisibleProperty, true); // keep UI clear

                // Apply the editing template
                editingTemplate = new DataTemplate();
                editingTemplate.VisualTree = timePickerFactory;
                vegeColumn.CellEditingTemplate = editingTemplate;

                // For display, just use a TextBlock that shows the time
                textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Vege")
                {
                    StringFormat = @"hh\:mm"
                });
                displayTemplate = new DataTemplate();
                displayTemplate.VisualTree = textBlockFactory;
                vegeColumn.CellTemplate = displayTemplate;





                dataGrid.ItemsSource = beosztasList;

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Fodrász", SelectedItemBinding = new Binding("Fodrasz.Nev"), ItemsSource = FodraszNevHash });
                dataGrid.Columns.Add(dateColumn);
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Nap", SelectedItemBinding = new Binding("NapNeve"), ItemsSource = Napok });
                dataGrid.Columns.Add(kezdesColumn);
                dataGrid.Columns.Add(vegeColumn);




            }
            if (comboBox.SelectedItem.ToString() == "Fodrászok")
            {
                dataGrid.ItemsSource = (Application.Current as App).GetFodrasz();

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Név", Binding = new Binding("Nev") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Telefonszám", Binding = new Binding("TelefonSzam") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Leírás", Binding = new Binding("Leiras") });
                var eleresiUtColumn = new DataGridTemplateColumn
                {
                    Header = "Profilkép neve",
                };

                var cellTemplate = new DataTemplate();

                // Create the StackPanel to hold button and label
                FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
                stackPanelFactory.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);

                // Create the MahApps Metro styled Button
                FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(System.Windows.Controls.Button));
                buttonFactory.SetValue(System.Windows.Controls.Button.ContentProperty, "📂");
                buttonFactory.SetValue(System.Windows.Controls.Button.StyleProperty, (System.Windows.Style)Application.Current.Resources["MahApps.Styles.Button"]);

                // Add the click handler
                buttonFactory.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(FilePickerButton_Click));

                // Create the Label
                FrameworkElementFactory labelFactory = new FrameworkElementFactory(typeof(TextBlock));
                labelFactory.SetBinding(TextBlock.TextProperty, new Binding("PfpName"));
                labelFactory.SetValue(TextBlock.MarginProperty, new Thickness(10, 0, 0, 0));

                // Add button and label to the stackpanel
                stackPanelFactory.AppendChild(buttonFactory);
                stackPanelFactory.AppendChild(labelFactory);

                // Set the visual tree of the template
                cellTemplate.VisualTree = stackPanelFactory;

                eleresiUtColumn.CellTemplate = cellTemplate;

                // Add the column to your datagrid
                dataGrid.Columns.Add(eleresiUtColumn);

                List<int> FodraszatIdList = new List<int>();
                foreach (var item in (Application.Current as App).GetFodraszat())
                {
                    FodraszatIdList.Add(item.Id);
                }
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Fodrászat azonosító", SelectedItemBinding = new Binding("FodraszatId"), ItemsSource = FodraszatIdList });


            }
            if (comboBox.SelectedItem.ToString() == "Fodrászatok")
            {
                delete.IsEnabled = false;
                HashSet<string> TelepulesNevHash = new HashSet<string>();
                var selectedFodraszat = (Application.Current as App).GetFodraszat().Where(x => x.Id == (Application.Current as App).SelectedFodraszat).FirstOrDefault();
                var fodraszatHelp = new List<FodraszatHelpModel>();

                foreach (var varos in (Application.Current as App).GetVaros())
                {
                    TelepulesNevHash.Add(varos.TelepulesNev);
                    if (varos.Id == selectedFodraszat.VarosId)
                    {
                        selectedFodraszat.Varos = varos;
                    }
                }

                fodraszatHelp.Add(new FodraszatHelpModel()
                {
                    Id = selectedFodraszat.Id,
                    VarosId = selectedFodraszat.VarosId,
                    Varos = selectedFodraszat.Varos,
                    Utca = selectedFodraszat.Utca,
                    HazSzam = selectedFodraszat.HazSzam,
                    FodraszatAzon = "",
                    NewFodraszatAzon = "",
                });

                dataGrid.ItemsSource = fodraszatHelp;

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Város", SelectedItemBinding = new Binding("Varos.TelepulesNev"), ItemsSource = TelepulesNevHash });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Utca", Binding = new Binding("Utca") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Házszám", Binding = new Binding("HazSzam") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Azonosító", Binding = new Binding("FodraszatAzon") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Új Azonosító", Binding = new Binding("NewFodraszatAzon") });


            }
            if (comboBox.SelectedItem.ToString() == "Foglalások")
            {
                var foglalasokList = (Application.Current as App).GetFoglalas();

                HashSet<string> SzolgaltatasNevHash = new HashSet<string>();
                HashSet<string> VasarloNevHash = new HashSet<string>();
                HashSet<DateTime> NaptarDatumHash = new HashSet<DateTime>();

                foreach (var foglalas in foglalasokList)
                {
                    foreach (var szolgaltatas in (Application.Current as App).GetSzolgaltatas())
                    {
                        SzolgaltatasNevHash.Add(szolgaltatas.SzolgaltatasNev);
                        if (szolgaltatas.Id == foglalas.SzolgaltatasId)
                        {
                            foglalas.Szolgaltatas = szolgaltatas;
                        }
                    }
                    foreach (var vasarlo in (Application.Current as App).GetVasarlo())
                    {
                        VasarloNevHash.Add(vasarlo.Nev);
                        if (vasarlo.Id == foglalas.VasarloId)
                        {
                            foglalas.Vasarlo = vasarlo;
                        }
                    }
                    foreach (var naptar in (Application.Current as App).GetNaptar())
                    {
                        NaptarDatumHash.Add(naptar.Datum);
                        if (naptar.Id == foglalas.NaptarId)
                        {
                            foglalas.Naptar = naptar;
                        }
                    }
                }


                dataGrid.ItemsSource = foglalasokList;
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Szolgáltatás", SelectedItemBinding = new Binding("Szolgaltatas.SzolgaltatasNev"), ItemsSource = SzolgaltatasNevHash });
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Vásárló", SelectedItemBinding = new Binding("Vasarlo.Nev"), ItemsSource = VasarloNevHash });
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Naptár", SelectedItemBinding = new Binding("Naptar.Datum"), ItemsSource = NaptarDatumHash });


            }
            if (comboBox.SelectedItem.ToString() == "Képek")
            {
                var kepekList = (Application.Current as App).GetKepek();
                HashSet<string> FodraszNevHash = new HashSet<string>();

                foreach (var kep in kepekList)
                {
                    foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                    {
                        FodraszNevHash.Add(fodrasz.Nev);
                        if (fodrasz.Id == kep.FodraszId)
                        {
                            kep.Fodrasz = fodrasz;
                        }
                    }
                }

                dataGrid.ItemsSource = kepekList;

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Fodrász", SelectedItemBinding = new Binding("Fodrasz.Nev"), ItemsSource = FodraszNevHash });
                var eleresiUtColumn = new DataGridTemplateColumn
                {
                    Header = "Elérési út",
                };

                var cellTemplate = new DataTemplate();

                // Create the StackPanel to hold button and label
                FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));
                stackPanelFactory.SetValue(StackPanel.OrientationProperty, System.Windows.Controls.Orientation.Horizontal);

                // Create the MahApps Metro styled Button
                FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(System.Windows.Controls.Button));
                buttonFactory.SetValue(System.Windows.Controls.Button.ContentProperty, "📂");
                buttonFactory.SetValue(System.Windows.Controls.Button.StyleProperty, (System.Windows.Style)Application.Current.Resources["MahApps.Styles.Button"]);

                // Add the click handler
                buttonFactory.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(FilePickerButton_Click));

                // Create the Label
                FrameworkElementFactory labelFactory = new FrameworkElementFactory(typeof(TextBlock));
                labelFactory.SetBinding(TextBlock.TextProperty, new Binding("EleresiUt"));
                labelFactory.SetValue(TextBlock.MarginProperty, new Thickness(10, 0, 0, 0));

                // Add button and label to the stackpanel
                stackPanelFactory.AppendChild(buttonFactory);
                stackPanelFactory.AppendChild(labelFactory);

                // Set the visual tree of the template
                cellTemplate.VisualTree = stackPanelFactory;

                eleresiUtColumn.CellTemplate = cellTemplate;

                // Add the column to your datagrid
                dataGrid.Columns.Add(eleresiUtColumn);
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Kép neve", Binding = new Binding("KepNev") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Leírás", Binding = new Binding("Leiras") });


            }
            if (comboBox.SelectedItem.ToString() == "Naptár")
            {
                var naptarList = (Application.Current as App).GetNaptar();
                HashSet<string> FodraszNevHash = new HashSet<string>();

                foreach (var naptar in naptarList)
                {
                    foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                    {
                        FodraszNevHash.Add(fodrasz.Nev);
                        if (fodrasz.Id == naptar.FodraszId)
                        {
                            naptar.Fodrasz = fodrasz;
                        }
                    }
                }

                dataGrid.ItemsSource = naptarList;

                // Add DateTimePicker Column
                DataGridTemplateColumn dateTimeColumn = new DataGridTemplateColumn
                {
                    Header = "Dátum",
                    SortMemberPath = "Datum"
                };

                // Cell Template (Displays the selected date & time as text)
                DataTemplate cellTemplate = new DataTemplate();
                FrameworkElementFactory textBlock = new FrameworkElementFactory(typeof(TextBlock));
                textBlock.SetBinding(TextBlock.TextProperty, new Binding("Datum") { StringFormat = "yyyy-MM-dd HH:mm" });
                cellTemplate.VisualTree = textBlock;
                dateTimeColumn.CellTemplate = cellTemplate;

                // Cell Editing Template (Allows editing with MahApps.Metro DateTimePicker)
                DataTemplate editTemplate = new DataTemplate();
                FrameworkElementFactory dateTimePicker = new FrameworkElementFactory(typeof(MahApps.Metro.Controls.DateTimePicker));
                dateTimePicker.SetBinding(MahApps.Metro.Controls.DateTimePicker.SelectedDateTimeProperty, new Binding("Datum") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                editTemplate.VisualTree = dateTimePicker;
                dateTimeColumn.CellEditingTemplate = editTemplate;




                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(dateTimeColumn);
                dataGrid.Columns.Add(new DataGridComboBoxColumn { Header = "Fodrász", SelectedItemBinding = new Binding("Fodrasz.Nev"), ItemsSource = FodraszNevHash });

            }
            if (comboBox.SelectedItem.ToString() == "Szolgáltatások")
            {
                dataGrid.ItemsSource = (Application.Current as App).GetSzolgaltatas();

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Szolgálatás Neve", Binding = new Binding("SzolgaltatasNev") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Időtartam", Binding = new Binding("Idotartalom") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Ár (Ft)", Binding = new Binding("Ar") });

            }
            if (comboBox.SelectedItem.ToString() == "Város")
            {
                dataGrid.ItemsSource = (Application.Current as App).GetVaros();

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Irányítószám", Binding = new Binding("IranyitoSzam") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Településnév", Binding = new Binding("TelepulesNev") });

            }
            if (comboBox.SelectedItem.ToString() == "Vásárló")
            {
                add.IsEnabled = false;
                update.IsEnabled = false;

                dataGrid.ItemsSource = (Application.Current as App).GetVasarlo();

                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Id", Binding = new Binding("Id") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Felhasználónév", Binding = new Binding("FelhasznaloNev") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Név", Binding = new Binding("Nev") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "Telefonszám", Binding = new Binding("TelefonSzam") });
                dataGrid.Columns.Add(new DataGridTextColumn { Header = "E-mail", Binding = new Binding("EMail") });

                dataGrid.Columns[1].IsReadOnly = true;
                dataGrid.Columns[2].IsReadOnly = true;
                dataGrid.Columns[3].IsReadOnly = true;
                dataGrid.Columns[4].IsReadOnly = true;

            }
            dataGrid.Columns[0].IsReadOnly = true;

            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                dataGrid.Columns[i].Width = ((Convert.ToDouble(this.Width) * 0.8) / dataGrid.Columns.Count) - 1;
            }

        }
        private void FilePickerButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button == null) return;


            if (comboBox.SelectedItem.ToString() == "Képek")
            {
                var dataContext = button.DataContext as Kepek;
                if (dataContext == null) return;

                var openFileDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Title = "Select a file",
                    Filter = "Images (*.jpg;*.png;)|*.jpg;*.png"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    dataContext.EleresiUt = openFileDialog.FileName;
                    dataContext.OnPropertyChanged(nameof(dataContext.EleresiUt));
                }
            }
            if (comboBox.SelectedItem.ToString() == "Fodrászok")
            {
                var dataContext = button.DataContext as Fodrasz;
                if (dataContext == null) return;

                var openFileDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Title = "Select a file",
                    Filter = "Images (*.jpg;*.png;)|*.jpg;*.png"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    dataContext.PfpName = openFileDialog.FileName;
                    dataContext.OnPropertyChanged(nameof(dataContext.PfpName));
                }
            }




        }

        public bool _isSuccesFulOrNot = true;
        private async Task UpdateData<T>(T updatedItem, int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364");

                // --- Beosztás tábla fodrászainak manuális beállítása ---

                if (updatedItem is Beosztas beosztas)
                {
                    foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                    {
                        if (beosztas.Fodrasz.Nev == fodrasz.Nev)
                        {
                            beosztas.FodraszId = fodrasz.Id;
                            beosztas.Fodrasz = fodrasz;
                        }
                    }
                }

                // --- Fodrászat tábla városainak manuális beállítása ---

                if (updatedItem is FodraszatHelpModel fodraszat)
                {
                    foreach (var varos in (Application.Current as App).GetVaros())
                    {
                        if (fodraszat.Varos.TelepulesNev == varos.TelepulesNev)
                        {
                            fodraszat.VarosId = varos.Id;
                            fodraszat.Varos = varos;
                        }
                    }

                    // --- Fodrászat azonosító ellenőrzése ---

                    if (string.IsNullOrEmpty(fodraszat.FodraszatAzon))
                    {
                        System.Windows.MessageBox.Show("A fodrászat módosításához meg kell adnod az azonosítót!");
                        return;
                    }

                    var validationModel = new FodraszatValidationModel { Id = id, FodraszatAzon = fodraszat.FodraszatAzon };
                    var validationContent = new StringContent(JsonConvert.SerializeObject(validationModel), Encoding.UTF8, "application/json");

                    var validationResponse = await client.PostAsync("/api/Fodraszat/authenticate", validationContent);

                    if (!validationResponse.IsSuccessStatusCode)
                    {
                        System.Windows.MessageBox.Show("Hibás azonosító!");
                        return;
                    }
                    else if (!string.IsNullOrEmpty(fodraszat.NewFodraszatAzon))
                    {
                        fodraszat.FodraszatAzon = fodraszat.NewFodraszatAzon;
                    }
                }

                // --- Foglálás tábla szolgáltatásainak, vásárlóinak és naptárainak manuális beállítása ---

                if (updatedItem is Foglalas foglalas)
                {
                    foreach (var szolgaltatas in (Application.Current as App).GetSzolgaltatas())
                    {
                        if (foglalas.Szolgaltatas.SzolgaltatasNev == szolgaltatas.SzolgaltatasNev)
                        {
                            foglalas.SzolgaltatasId = szolgaltatas.Id;
                            foglalas.Szolgaltatas = szolgaltatas;
                        }
                    }
                    foreach (var vasarlo in (Application.Current as App).GetVasarlo())
                    {
                        if (foglalas.Vasarlo.Nev == vasarlo.Nev)
                        {
                            foglalas.VasarloId = vasarlo.Id;
                            foglalas.Vasarlo = vasarlo;
                        }
                    }
                    foreach (var naptar in (Application.Current as App).GetNaptar())
                    {
                        if (naptar.Datum == naptar.Datum)
                        {
                            foglalas.NaptarId = naptar.Id;
                            foglalas.Naptar = naptar;
                        }
                    }
                }

                // --- Kép tábla fodrászainak manuális beállítása ---

                if (updatedItem is Kepek kepek)
                {
                    foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                    {
                        if (kepek.Fodrasz.Nev == fodrasz.Nev)
                        {
                            kepek.FodraszId = fodrasz.Id;
                            kepek.Fodrasz = fodrasz;
                        }
                    }
                }

                // --- Naptár tábla fodrászainak manuális beállítása ---

                if (updatedItem is Naptar naptarok)
                {
                    foreach (var fodrasz in (Application.Current as App).GetFodrasz())
                    {
                        if (naptarok.Fodrasz.Nev == fodrasz.Nev)
                        {
                            naptarok.FodraszId = fodrasz.Id;
                            naptarok.Fodrasz = fodrasz;
                        }
                    }
                }

                // --- Update-elem az adatot ---

                var updateContent = new StringContent(JsonConvert.SerializeObject(updatedItem), Encoding.UTF8, "application/json");
                HttpResponseMessage updateResponse = null;


                switch (updatedItem)
                {
                    case Beosztas _: updateResponse = await client.PutAsync($"api/Beosztas{id}/", updateContent); break;
                    case Fodrasz _: updateResponse = await client.PutAsync($"api/Fodrasz{id}/1", updateContent); break;
                    case FodraszatHelpModel _:
                        updateResponse = await client.PutAsync($"api/Fodraszat{id}/0", updateContent);
                        System.Windows.MessageBox.Show("Fodrászat adatainak módosítása után újból be kell jelentkezned!");
                        exit_Click(new object { }, new RoutedEventArgs { });
                        break;
                    case Foglalas _: updateResponse = await client.PutAsync($"api/Foglalas{id}", updateContent); break;
                    case Kepek _: updateResponse = await client.PutAsync($"api/Kepek{id}", updateContent); break;
                    case Naptar _: updateResponse = await client.PutAsync($"api/Naptar{id}", updateContent); break;
                    case Szolgaltatas _: updateResponse = await client.PutAsync($"api/Szolgaltatas{id}", updateContent); break;
                    case Varos _: updateResponse = await client.PutAsync($"api/Varos{id}", updateContent); break;
                    case Vasarlo _: updateResponse = await client.PutAsync($"api/Vasarlo{id}", updateContent); break;
                }

                // --- Válaszüzenet ---
                if (updateResponse != null && updateResponse.IsSuccessStatusCode) _isSuccesFulOrNot = true;
                else _isSuccesFulOrNot = false;
            }
        }

        private async Task DeleteData(int deletedId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364");
                HttpResponseMessage response = null;

                switch (comboBox.SelectedItem.ToString())
                {
                    case "Beosztás": response = await client.DeleteAsync($"api/Beosztas{deletedId}/"); break;
                    case "Fodrászok": response = await client.DeleteAsync($"api/Fodrasz{deletedId}/1"); break;
                    case "Fodrászatok": response = await client.DeleteAsync($"api/Fodraszat{deletedId}/0"); break;
                    case "Foglalások": response = await client.DeleteAsync($"api/Foglalas{deletedId}"); break;
                    case "Képek": response = await client.DeleteAsync($"api/Kepek{deletedId}"); break;
                    case "Naptár": response = await client.DeleteAsync($"api/Naptar{deletedId}"); break;
                    case "Szolgáltatások": response = await client.DeleteAsync($"api/Szolgaltatas{deletedId}"); break;
                    case "Város": response = await client.DeleteAsync($"api/Varos{deletedId}"); break;
                    case "Vásárló": response = await client.DeleteAsync($"api/Vasarlo{deletedId}"); break;
                }
                if (response.IsSuccessStatusCode) System.Windows.MessageBox.Show("Sikeres törlés!");
                else System.Windows.MessageBox.Show("A törlés nem sikerült.");
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update.IsEnabled = true;
            delete.IsEnabled = true;
            add.IsEnabled = true;
            Get();
            (Application.Current as App).ComboBoxSelectedItem = comboBox.SelectedItem.ToString();
        }
        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Get();
        }

        private async void update_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            foreach (var item in dataGrid.Items)
            {
                if (!_isSuccesFulOrNot) break;
                switch (item)
                {
                    case Beosztas updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                    case Fodrasz updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                    case FodraszatHelpModel updatedItem:
                        await UpdateData(updatedItem, updatedItem.Id);
                        break;
                    case Foglalas updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                    case Kepek updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                    case Naptar updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                    case Szolgaltatas updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                    case Varos updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                    case Vasarlo updatedItem: await UpdateData(updatedItem, updatedItem.Id); break;
                }
            }
            if (_isSuccesFulOrNot) System.Windows.MessageBox.Show("Sikeresen módosítva!");
            else System.Windows.MessageBox.Show("A módosítás sikertelen.");

            Get();
        }
        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var result = System.Windows.MessageBox.Show("Biztosan törölni szeretné a kiválasztott elemet?", "Confirm Delete", System.Windows.MessageBoxButton.YesNo);
                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    switch (dataGrid.SelectedItem)
                    {
                        case Beosztas deletedItem: await DeleteData(deletedItem.Id); break;
                        case Fodrasz deletedItem: await DeleteData(deletedItem.Id); break;
                        case Fodraszat deletedItem: await DeleteData(deletedItem.Id); break;
                        case Foglalas deletedItem: await DeleteData(deletedItem.Id); break;
                        case Kepek deletedItem: await DeleteData(deletedItem.Id); break;
                        case Naptar deletedItem: await DeleteData(deletedItem.Id); break;
                        case Szolgaltatas deletedItem: await DeleteData(deletedItem.Id); break;
                        case Varos deletedItem: await DeleteData(deletedItem.Id); break;
                        case Vasarlo deletedItem: await DeleteData(deletedItem.Id); break;
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Kérlek válassz ki egy elemet a törléshez!");
            }


            Get();

        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new NewItemWindow();
            newItem.Closed += (object _, EventArgs a) => this.IsEnabled = true;
            newItem.Show();
            this.IsEnabled = false;
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var binding = column.Binding as Binding;
                    if (binding != null)
                    {
                        var propertyName = binding.Path.Path;
                        var editedTextbox = e.EditingElement as System.Windows.Controls.TextBox;
                        if (editedTextbox != null)
                        {
                            string newValue = editedTextbox.Text;
                            var item = e.Row.Item;

                            if (item != null)
                            {
                                var property = item.GetType().GetProperty(propertyName);
                                if (property != null)
                                {
                                    try
                                    {
                                        var convertedValue = Convert.ChangeType(newValue, property.PropertyType);
                                        property.SetValue(item, convertedValue);
                                    }
                                    catch (Exception)
                                    {
                                        System.Windows.MessageBox.Show($"Nem megfelelő adat a pirossal jelölt mezőben");
                                        e.Cancel = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            OpenPage OP = new OpenPage();
            OP.Show();
            this.Close();
        }
    }
}


