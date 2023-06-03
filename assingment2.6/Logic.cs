﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

namespace assingment2._6
{
    internal class Logic
    {
        public List<Subject> Subjects = new List<Subject>();
        public ObservableCollection<Subject> ObservableSubjects { get; set; }
        public Subject SelectedSubject { get; set; }
        public Subject NewSubject { get; set; } = new Subject() { Title = string.Empty, IsPassed = true };
        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ChangeStatusCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public bool All { get; set; } = true;
        public bool Passed { get; set; } = true;
        public bool NotPassed { get; set; } = false;

        public Logic() 
        { 
            ObservableSubjects = new ObservableCollection<Subject>();
            ObservableSubjects.Add(new Subject() { Title = "a", IsPassed = true });
            ObservableSubjects.Add(new Subject() { Title = "b", IsPassed = true });
            ObservableSubjects.Add(new Subject() { Title = "c", IsPassed = false });
            Subjects.Add(new Subject() { Title = "a", IsPassed = true });
            Subjects.Add(new Subject() { Title = "b", IsPassed = true });
            Subjects.Add(new Subject() { Title = "c", IsPassed = false });
            StartFileRead();

            AddCommand = new RelayCommand(AddSubject);
            DeleteCommand = new RelayCommand(DeleteSubject);
            ChangeStatusCommand = new RelayCommand(ChangeStatus);
            FilterCommand = new RelayCommand(Filter);
        }

        public async Task StartFileRead()
        {
            var file = File.ReadAllText("subjects.txt");

            var bn = file.Split('\n');
            foreach (var s in bn)
            {
                var _arr = s.Split(',');
                ObservableSubjects.Add(new Subject() { Title = _arr[0], IsPassed = bool.Parse(_arr[1]) });
            }
        }



        private void AddSubject()
        {
            ObservableSubjects.Add(new Subject() { Title = NewSubject.Title, IsPassed = NewSubject.IsPassed });
            Subjects.Add(new Subject() { Title = NewSubject.Title, IsPassed = NewSubject.IsPassed });
            Trace.WriteLine("click");
            
        }

        private void DeleteSubject()
        {
            ObservableSubjects.Remove(SelectedSubject);
            Subjects.Remove(SelectedSubject);
        }

        private void ChangeStatus()
        {
            SelectedSubject.IsPassed = !SelectedSubject.IsPassed;
            Trace.WriteLine(SelectedSubject.IsPassed);
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
