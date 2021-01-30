using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace TerminOrganisator2.ViewModels
{
    public class AppointmentManagerViewModel
    {
        // Declare the delegate (if using non-generic pattern).
        public delegate void SampleEventHandler(object sender, EventArgs e);

        // Declare the event.
        public event SampleEventHandler SampleEvent;

        // Wrap the event in a protected virtual method
        // to enable derived classes to raise the event.
        protected virtual void RaiseSampleEvent()
        {
            // Raise the event in a thread-safe manner using the ?. operator.
            SampleEvent?.Invoke(this, new EventArgs("Hello"));
        }

        public ObservableCollection<Appointment> Appointments { get; set; }

        private AppointmentManager _manager = new AppointmentManager();
        public int SelIndex { get; set; }

        public AppointmentManagerViewModel()
        {
            this.Appointments = new ObservableCollection<Appointment>(_manager.Termine_aus_Datei_einlesen());
        }

        public AppointmentManagerViewModel(string test)
        {
            this.Appointments = new ObservableCollection<Appointment>();
        }

        private ICommand _deleteAllCommand;
        public ICommand DeleteAllCommand
        {
            get
            {
                return _deleteAllCommand ?? (_deleteAllCommand = new CommandHandler(() => DeleteAll(), () => CanExecute));
            }
        }

        private void ChangeCurrentSelection()
        {
            //DO SOMETHING WHEN SELECTION IS CHANGED
        }

        public bool CanExecute
        {
            get
            {
                // check if executing is allowed, i.e., validate, check if a process is running, etc. 
                return true;
            }
        }

        public void Hinzufügen(Appointment appointment)
        {
            if (appointment != null)
            {
                this.Appointments.Add(appointment);
            }
        }

        public void Delete()
        {
            if(this.Appointments.Count > 0 && this.SelIndex >= 0 && this.SelIndex < this.Appointments.Count)
            {
                this.Appointments.RemoveAt(this.SelIndex);
            }
        }

        public void DeleteAll()
        {
            Appointments.Clear();
        }

        public void PersistData()
        {
            this._manager.Termine_in_Datei_speichern(this.Appointments.ToList());
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
