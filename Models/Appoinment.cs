using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminOrganisator2
{
    public class Appointment: INotifyPropertyChanged
    {
        private DateTime _appointmentDate;
        private string _appointmentRemark;

        public override string ToString()
        {
            return $"{this.AppointmentDate.ToString("yyyy/MM/dd HH:mm:ss")};{this.AppointmentRemark}";
        }

        public DateTime AppointmentDate
        {
            get
            {
                return _appointmentDate;
            }
            set
            {
                _appointmentDate = value;
                OnPropertyChanged("AppointmentDate");
            }
        }

        public string AppointmentRemark
        {
            get
            {
                return _appointmentRemark;
            }
            set
            {
                _appointmentRemark = value;
                OnPropertyChanged("AppointmentRemark");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
