using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.ComponentModel;

namespace assingment2._6
{
    internal class Logic : INotifyPropertyChanged
    {
        public void PropertyChange(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Subject> Subjects = new List<Subject>();
        public ObservableCollection<Subject> ObservableSubjects { get; set; }
        public Subject SelectedSubject { get; set; }
        public Subject NewSubject { get; set; } = new Subject(string.Empty, true );
        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ChangeStatusCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand ShowCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public bool All { get; set; } = true;
        public bool Passed { get; set; } = true;
        public bool NotPassed { get; set; } = false;

        public Logic() 
        { 
            ObservableSubjects = new ObservableCollection<Subject>();
            //ObservableSubjects.Add(new Subject() { Title = "programming", IsPassed = true });
            //ObservableSubjects.Add(new Subject() { Title = "maths", IsPassed = true });
            //ObservableSubjects.Add(new Subject() { Title = "dh", IsPassed = false });
            Subjects.Add(new Subject("programming", true ));
            Subjects.Add(new Subject("maths",  true ));
            Subjects.Add(new Subject("dh", false ));

            foreach (Subject sub in  Subjects)
            {
                ObservableSubjects.Add(sub);
            }
            

            AddCommand = new RelayCommand(AddSubject);
            DeleteCommand = new RelayCommand(DeleteSubject);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
            FilterCommand = new RelayCommand(Filter);
            ShowCommand = new RelayCommand(Show);
            SaveCommand = new RelayCommand(Save);

        }

        private async void Save()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "subjects.txt");
            string str = "";
            foreach(Subject logicSubject in  Subjects)
            {
                if (str != "")
                {
                    str += "\n";
                }

                str += $"{logicSubject.Title},{logicSubject.IsPassed}";
            }
            using(StreamWriter sr = new StreamWriter(path))
            {
                await sr.WriteAsync(str);
            }
        }

        private void Show()
        {
            Subjects.Clear();
            ObservableSubjects.Clear();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "subjects.txt");
            var file = File.ReadAllText(path);
            var bn = file.Split('\n');
            //Trace.WriteLine("here");
            foreach (var s in bn)
            {
                var _arr = s.Split(',');
                Subjects.Add(new Subject( _arr[0], bool.Parse(_arr[1]) ));

            }
            Filter();
        }



        private void AddSubject()
        {
            ObservableSubjects.Add(new Subject(NewSubject.Title, NewSubject.IsPassed ));
            Subjects.Add(new Subject(NewSubject.Title, NewSubject.IsPassed ));
            //Trace.WriteLine("click");
            
        }

        private void DeleteSubject()
        {
            Subjects.Remove(SelectedSubject);
            ObservableSubjects.Remove(SelectedSubject);
        }

        private void ChangeStatus()
        {
            SelectedSubject.IsPassed = !SelectedSubject.IsPassed;
            //Trace.WriteLine(SelectedSubject.IsPassed);
        }

        private void Filter()
        {
            ObservableSubjects.Clear();

            if (All)
            {
                foreach(Subject sub in Subjects)
                {
                    ObservableSubjects.Add(sub);
                }
            }

            if (Passed)
            {
                foreach(Subject sub in Subjects)
                {
                    if(sub.IsPassed == true)
                    {
                        ObservableSubjects.Add(sub);
                    }
                }
            }
            
            if (NotPassed)
            {
                foreach(Subject sub in Subjects)
                {
                    if(sub.IsPassed == false) 
                    {
                        ObservableSubjects.Add(sub);
                    }
                }    
            }
        }
    }
}
