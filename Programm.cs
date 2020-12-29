using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TerminOrganisator2
{
    class Programm
    {

        public static void Uhrzeit_füllen(ComboBox Cmbx_Stunden,ComboBox Cmbx_Minuten)
        {
            for (int i = 0; i <= 23; i++)
            {
                Cmbx_Stunden.Items.Add(i.ToString("D2"));
            }
            for (int i = 0; i <= 59; i++)
            {
                Cmbx_Minuten.Items.Add(i.ToString("D2"));
            }
            Cmbx_Stunden.SelectedIndex = 0;
            Cmbx_Minuten.SelectedIndex = 0;
        }

        public static void Termine_aus_Datei_einlesen(ListView Lv_Termine)
        {
            string Datei_mit_Termine = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Termine.txt";
            StreamReader sr = new StreamReader(Datei_mit_Termine);
            string s = sr.ReadToEnd();


            foreach (var item in s.Split('\n'))
            {
                if(item.Length > 0)
                {

                    Lv_Termine.Items.Add(new
                    {
                        Termin = item.ToString().Substring(11, item.ToString().IndexOf("Uhrzeit") - 13).ToString(),
                        Uhrzeit = item.ToString().Substring(item.ToString().IndexOf("Uhrzeit") + 10, 5).ToString(),
                        Datum = item.ToString().Substring(item.ToString().IndexOf("Datum") + 8, 10).ToString()
                        
                    });
                }
              
            }
            sr.Close();
        }

        public static void Termine_in_Datei_speichern(ListView Lv_Termine)
        {
            string Datei_mit_Termine = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Termine.txt";
            StreamWriter sw = new StreamWriter(Datei_mit_Termine);

            for (int i = 0; i < Lv_Termine.Items.Count; i++)
            {
                if(i<Lv_Termine.Items.Count)
                {
                    sw.WriteLine(Lv_Termine.Items[i]);
                }
                else
                {
                    sw.Write(Lv_Termine.Items[i]);
                }

            }
            sw.Close();

        }
              
    }
}
