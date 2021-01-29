using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TerminOrganisator2
{
     class Termine
    {
        public static void Hinzufügen(string Termin, string Datum, string Uhrzeit, ListView Lv_Termine)
        {
            Lv_Termine.Items.Add(new
            {
                Termin,
                Uhrzeit,
                Datum
            });

        }
   
        public static void Löschen(ListView Lv_Termine)
        { 
            Lv_Termine.Items.Remove(Lv_Termine.SelectedItem);
        }

        public static void Alle_Löschen(ListView Lv_Termine)
        {
            Lv_Termine.Items.Clear();
        }

        public static void In_Datenfelder_schreiben(ListView Lv_Termine,TextBox Txbx_Termine,DatePicker Dapi_Datum,ComboBox Cmbx_Stunden, ComboBox Cmbx_Minuten)
        {
            if(Lv_Termine.SelectedIndex >= 0)
            {

                string Uhrzeit = Lv_Termine.SelectedItem.ToString().Substring(Lv_Termine.SelectedItem.ToString().IndexOf("Uhrzeit") + 10, 5).ToString();
                string Datum = Lv_Termine.SelectedItem.ToString().Substring(Lv_Termine.SelectedItem.ToString().IndexOf("Datum") + 8, 10).ToString();
                string Termin = Lv_Termine.SelectedItem.ToString().Substring(10, Lv_Termine.SelectedItem.ToString().IndexOf("Uhrzeit") - 12).ToString();

                Txbx_Termine.Text = Termin;
                Cmbx_Stunden.SelectedItem = Uhrzeit.Substring(0, 2);
                Cmbx_Minuten.SelectedItem = Uhrzeit.Substring(3, 2);
                Dapi_Datum.SelectedDate = DateTime.Parse(Datum);

            }
            
           
        }

        public static void Terminänderung_speichern(ListView Lv_Termine, TextBox Txbx_Termine, DatePicker Dapi_Datum, ComboBox Cmbx_Stunden, ComboBox Cmbx_Minuten)
        {
            int i = Lv_Termine.SelectedIndex;
            if(i >= 0)
            {
                Lv_Termine.Items.RemoveAt(i);
                Lv_Termine.Items.Insert(i, new
                {
                    Termin = Txbx_Termine.Text,
                    Uhrzeit = Cmbx_Stunden.SelectedItem.ToString() + ":" + Cmbx_Minuten.SelectedItem.ToString(),
                    Datum = Dapi_Datum.SelectedDate.ToString().Split(' ')[0]
                });
            }
   
        }

    }


}
