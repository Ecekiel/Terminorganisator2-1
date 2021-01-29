using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TerminOrganisator2
{
    public class AppointmentManager
    {
        private string filePath = "";

        public AppointmentManager()
        {
        }

        public AppointmentManager(string filePath)
        {
            this.filePath = filePath;
        }


        public void ReadAppointmentsFile()
        {
            //this.
        }

        public List<Appointment> Termine_aus_Datei_einlesen()
        {
            string appointmentsFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Termine.txt";
            List<Appointment> existingAppointments = new List<Appointment>();
            //Create file if it doesnt exist
            if (!File.Exists(appointmentsFile))
            {
                File.Create(appointmentsFile);
            }
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(appointmentsFile))
                {
                    string item;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((item = sr.ReadLine()) != null)
                    {
                        if (item.Length > 0)
                        {
                            var line = item.Split(';');
                            DateTime parsedDate;
                            existingAppointments.Add(new Appointment
                            {
                                //TODO better parsing 
                                AppointmentDate = DateTime.TryParse(line[0], out parsedDate) ? parsedDate : DateTime.Now,
                                AppointmentRemark = line[1]
                        });
                        }
                    }
                    return existingAppointments;

                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return new List<Appointment>();
            }
            return new List<Appointment>();

        }

        public void Termine_in_Datei_speichern(List<Appointment> appoinments)
        {
            string Datei_mit_Termine = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Termine.txt";
            using (FileStream fs = File.Open(Datei_mit_Termine, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);

                appoinments.ForEach(sw.WriteLine);
                sw.Close();
            }
        }
    }
}
