using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace assingment2._6
{
    public partial class Dicipline : UserControl, INotifyPropertyChanged
    {
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Dicipline()
        {
            InitializeComponent();
        }



        public Dicipline(string str, bool status)
        {
            InitializeComponent();

            Title = str;

            Status = status;


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CheckStatus()
        {
            Status = !Status;
        }

        public void SetFilter(int index)
        {
            this.Visibility = Visibility.Collapsed;

            if (index == 0)
            {
                this.Visibility = Visibility.Visible;
            }
            else
            {
                if (index == 1 && Status)
                {
                    this.Visibility = Visibility.Visible;
                }
                else
                {
                    if (index == 2 && Status == false)
                    {
                        this.Visibility = Visibility.Visible;
                    }
                }
            }
        }
    }
}
