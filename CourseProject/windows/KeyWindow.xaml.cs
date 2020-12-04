using CourseProject.Other_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CourseProject;

namespace CourseProject.windows
{
    /// <summary>
    /// Логика взаимодействия для KeyWindow.xaml
    /// </summary>
    public partial class KeyWindow : Window
    {
        internal string correctKey = "";
        public KeyWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }



        private void KeyAgreeButton_Click(object sender, RoutedEventArgs e)
        {
            string correctKey = "";
            bool isContainsUpper = false;
            foreach (char item in FullKeyBox.Text)
            {
                if (TextEncoder.RusUpperAlphabet.Contains(item))
                {
                    correctKey += item.ToString().ToLower();
                    isContainsUpper = true;
                }
                else
                {
                    correctKey += item;
                }
            }
            if (isContainsUpper)
            {
                MessageBox.Show("В данной реализации алгоритма заглавные буквы ключа преобразуются в строчные, будет использован следующий ключ: " + correctKey);
            }
            this.correctKey = correctKey;
            Close();
        }

        private void FullKeyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICollection<TextChange> changes = e.Changes;
            KeyChecker.CheckKey(this, changes);
        }
    }
}
