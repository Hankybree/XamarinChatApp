using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ChatApp.Annotations;
using Xamarin.Forms;

namespace ChatApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _busy;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected void RefreshCanExecute(List<ICommand> buttonCommands)
        {
            foreach (var command in buttonCommands)
            {
                ((Command)command).ChangeCanExecute();
            }
        }
        
        public bool IsBusy
        {
            get => _busy;
            set
            {
                if (_busy == value) return;
                _busy = value;
                OnPropertyChanged();
            }
        }
    }
}