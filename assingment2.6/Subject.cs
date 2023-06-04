using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assingment2._6
{
    internal class Subject : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public bool IsPassed { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void PropertyChange(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
