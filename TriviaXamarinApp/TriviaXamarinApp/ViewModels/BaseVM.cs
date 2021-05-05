using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TriviaXamarinApp.ViewModels
{
    abstract public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
    }
}
