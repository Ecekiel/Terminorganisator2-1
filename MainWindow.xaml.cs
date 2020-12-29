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

namespace TerminOrganisator2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }





        private void Form_Loaded(object sender, RoutedEventArgs e)
        {
            Programm.Uhrzeit_füllen(Cmbx_Stunden, Cmbx_Minuten);
            Programm.Termine_aus_Datei_einlesen(Lv_Termine);
            Btn_speichern.Visibility = Visibility.Hidden;

            System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Terminüberwachung;
            Timer.Start();

        }

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
                Uhrzeit = Lv_Termine.Items[i].ToString().Substring(Lv_Termine.Items[i].ToString().IndexOf("Uhrzeit")+10,5).ToString();
                Datum = Lv_Termine.Items[i].ToString().Substring(Lv_Termine.Items[i].ToString().IndexOf("Datum") + 8, 10).ToString();


                if (Uhrzeit == Uhrzeit_jetzt && Datum == Datum_jetzt)
                {
                    MessageBox.Show(Termin);
                    Lv_Termine.Items.Remove(Lv_Termine.Items[i]);
                }
            }




        }

        private void BtnTermine_Hinzufügen_Click(object sender, RoutedEventArgs e)
        {
            string Termin = Txbx_Termine.Text;
            string Datum = Dapi_Datum.SelectedDate.ToString().Split(' ')[0];
            if (Datum == "")
            {
                Datum = DateTime.Now.ToString("d");
            }
            string Uhrzeit = Cmbx_Stunden.SelectedItem.ToString() + ":" + Cmbx_Minuten.SelectedItem.ToString();
            Termine.Hinzufügen(Termin, Datum, Uhrzeit, Lv_Termine);
            Txbx_Termine.Text = "";
        }

        private void Btn_Termin_Löschen_Click(object sender, RoutedEventArgs e)
        {
            Termine.Löschen(Lv_Termine);
        }

        private void Btn_Alle_Löschen_Click(object sender, RoutedEventArgs e)
        {
            Termine.Alle_Löschen(Lv_Termine);
        }

        private void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Programm.Termine_in_Datei_speichern(Lv_Termine);
        }

        private void Lv_Termine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Termine.In_Datenfelder_schreiben(Lv_Termine, Txbx_Termine, Dapi_Datum, Cmbx_Stunden, Cmbx_Minuten);
            Btn_speichern.Visibility = Visibility.Visible;
        }

        private void Btn_speichern_Click(object sender, RoutedEventArgs e)
        {
            Termine.Terminänderung_speichern(Lv_Termine, Txbx_Termine, Dapi_Datum, Cmbx_Stunden, Cmbx_Minuten);
            Btn_speichern.Visibility = Visibility.Hidden;
            Txbx_Termine.Text = "";
        }




    }
}
