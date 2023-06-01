using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net.NetworkInformation;

namespace assingment2._6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logic logic;
        public MainWindow()
        {
            InitializeComponent();



            logic = new Logic();
            logic.Subjects = new ObservableCollection<Dicipline>();

            StartFileRead();
            DataContext = logic;

            RBtnAll.IsChecked = true;
        }

        public async Task Save()
        {
            string str = "";
            foreach (Dicipline logicSubject in logic.Subjects)
            {
                if (str != "")
                {
                    str += "\n";
                }

                str += $"{logicSubject.Title},{logicSubject.Status}";
            }
            File.WriteAllText("subjects.txt", str);
        }

        public async Task StartFileRead()
        {
            var file = File.ReadAllText("subjects.txt");

            var bn = file.Split('\n');
            foreach (var s in bn)
            {
                var _array = s.Split(',');
                logic.Subjects.Add(new Dicipline(_array[0], bool.Parse(_array[1])));
            }
        }
        private void BtnChangeStat_Click(object sender, RoutedEventArgs e)
        {
            logic.SelectedSubject.CheckStatus();
            Save();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (logic.SelectedSubject != null)
            {
                logic.Subjects.Remove(logic.SelectedSubject);
                Save();
            }
        }

        private void RBtn_Checked(object sender, RoutedEventArgs e)
        {
            int index = 0;

            if (RBtnPassed.IsChecked == true)
            {
                index = 1;
            }

            if (RBtnNotPassed.IsChecked == true)
            {
                index = 2;
            }

            foreach (Dicipline logicSubject in logic.Subjects)
            {
                logicSubject.SetFilter(index);
            }

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TBDicipName.Text != "" && CBDicipStatus.SelectedIndex != -1)
            {
                var Disc = new Dicipline(TBDicipName.Text, CBDicipStatus.SelectedIndex == 0);
                logic.Subjects.Add(Disc);
                TBDicipName.Text = "";
                CBDicipStatus.SelectedIndex = -1;
                Save();
            }
        }
    }
}
