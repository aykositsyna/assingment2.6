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
        private string _title;
        private bool _isPassed;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                PropertyChange(nameof(Title));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void PropertyChange(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public bool IsPassed
        {
            get
            {
                return _isPassed;
            }
            set
            {
                _isPassed = value;
                PropertyChange(nameof(IsPassed));
            }
        }
        public Subject(string Title, bool IsPassed)
        {
            this.Title = Title;
            this.IsPassed = IsPassed;
        }
    }
}
