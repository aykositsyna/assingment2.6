using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GalaSoft.MvvmLight.Command;

namespace assingment2._6
{
    internal class Logic
    {
        //public List<Subject> Subjects = new List<Subject>();
        public ObservableCollection<Subject> Subjects { get; set; }
        public Subject SelectedSubject { get; set; }
        public Subject NewSubject { get; set; } = new Subject() { Title = string.Empty, IsPassed = true };
        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public Logic() 
        { 
            Subjects = new ObservableCollection<Subject>();
           
            StartFileRead();

            AddCommand = new RelayCommand(AddSubject);
            DeleteCommand = new RelayCommand(DeleteSubject);
        }

        public async Task StartFileRead()
        {
            var file = File.ReadAllText("subjects.txt");

            var bn = file.Split('\n');
            foreach (var s in bn)
            {
                var _arr = s.Split(',');
                Subjects.Add(new Subject() { Title = _arr[0], IsPassed = bool.Parse(_arr[1]) });
            }
        }



        private void AddSubject()
        {
            Subjects.Add(new Subject() { Title = NewSubject.Title, IsPassed = NewSubject.IsPassed });
            Console.WriteLine("click");
            //ObservableSubjects.Add(new Subject() { Title = NewSubject.Title, IsPassed = NewSubject.IsPassed });
        }

        private void DeleteSubject()
        {
            Subjects.Remove(SelectedSubject);
        }


    }
}
