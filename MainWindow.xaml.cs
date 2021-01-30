using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerminOrganisator2.ViewModels;
using System.Globalization;


namespace TerminOrganisator2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppointmentManagerViewModel ViewModel { get; set; }

        //POLYMORPHISMUS
        //DYNAMISCHE BINDUNG

        public MainWindow()
        {
            this.ViewModel = new AppointmentManagerViewModel();
            InitializeComponent();
            this.DataContext = this.ViewModel;
        }

        private void Form_Loaded(object sender, RoutedEventArgs e)
        {
            Program.Termine_aus_Datei_einlesen(Lv_Termine);
            this.ViewModel.SampleEvent += ShowMessage;
            //Btn_speichern.Visibility = Visibility.Hidden;

            //System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
            //Timer.Interval = TimeSpan.FromSeconds(1);
            //Timer.Tick += Terminüberwachung;
            //Timer.Start();

        }

        //TODO 
        //Lager das Event in das ViewModel aus und überprüfe im ViewModel selbst
        //jede Sekunde ob ein Termin ausgelaufen ist oder nicht. 
        //Falls der Termin ausgelaufen ist löse ein Event aus.
        //In der MainWindow.xaml.cs horchst du auf dieses Event und falls es ausgelöst wird dann zeigst du eine MessageBox an.

        void ShowMessage(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        //TODO rewrite so it works with changes made to model/viewmodel
        void Terminüberwachung(object sender, EventArgs e)
        {

            string Uhrzeit_jetzt = DateTime.Now.ToString("t");
            string Datum_jetzt = DateTime.Now.ToString("d");
            string Uhrzeit;
            string Datum;
            string Termin;

            for (int i = 0; i < Lv_Termine.Items.Count; i++)
            {
                Termin = Lv_Termine.Items[i].ToString().Substring(11, Lv_Termine.Items[i].ToString().IndexOf("Uhrzeit") - 13).ToString();
                Uhrzeit = Lv_Termine.Items[i].ToString().Substring(Lv_Termine.Items[i].ToString().IndexOf("Uhrzeit") + 10, 5).ToString();
                Datum = Lv_Termine.Items[i].ToString().Substring(Lv_Termine.Items[i].ToString().IndexOf("Datum") + 8, 10).ToString();


                if (Uhrzeit == Uhrzeit_jetzt && Datum == Datum_jetzt)
                {
                    
                    MessageBox.Show(Termin);
                    Lv_Termine.Items.Remove(Lv_Termine.Items[i]);
                }
            }
        }


        ////TODO do with command - see deleteall for reference
        //private void BtnTermine_Hinzufügen_Click(object sender, RoutedEventArgs e)
        //{
        //    string Termin = Txbx_Termine.Text;
        //    DateTime date = Dapi_Datum.Value == null ? DateTime.Now : Dapi_Datum.Value.Value;
        //    var culture = CultureInfo.CurrentCulture;
        //    this.ViewModel.Hinzufügen(new Appointment
        //    {
        //        AppointmentDate = date.Date,
        //        AppointmentRemark = Termin

        //    });

        //}

        //TODO do with command - see deleteall for reference
        private void Btn_Termin_Löschen_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Delete();
        }

        //private void Btn_Alle_Löschen_Click(object sender, RoutedEventArgs e)
        //{
        //    this.ViewModel.DeleteAll();
        //}


        private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModel.PersistData();
            //Program.Termine_in_Datei_speichern(Lv_Termine);
        }

        private void Lv_Termine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Termine.In_Datenfelder_schreiben(Lv_Termine, Txbx_Termine, Dapi_Datum, Cmbx_Stunden, Cmbx_Minuten);
            //Btn_speichern.Visibility = Visibility.Visible;
        }

        private void Btn_speichern_Click(object sender, RoutedEventArgs e)
        {
            //Termine.Terminänderung_speichern(Lv_Termine, Txbx_Termine, Dapi_Datum, Cmbx_Stunden, Cmbx_Minuten);
            //Btn_speichern.Visibility = Visibility.Hidden;
            //Txbx_Termine.Text = "";
        }




    }
}
