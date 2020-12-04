using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using CourseProject.Other_classes;
using CourseProject.windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool isSaved = false;
        private static FileImporter fileImporter = new FileImporter();
        private static FileSaver fileSv = new FileSaver();
        private string[] inpContent;
        private string[] outContent;
        
        public MainWindow()
        {
            InitializeComponent();
            
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        //Даём возможность сохранить файл, если этого не было сделано и убираем мусор
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!isSaved)
            {
                string msg = "Файл не сохранён. Закрыть без сохранения?";
                MessageBoxResult result = MessageBox.Show(msg, "Data App", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            if (e.Cancel) return;

        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            inpContent = fileImporter.ImportFile(true);
            DataIn.Text = "";
            DataOut.Text = "";
            outContent = null;
            if (inpContent == null) return;
            foreach (string item in inpContent)
            {
                DataIn.Text += item;
            }
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(outContent == null)
            {
                MessageBox.Show("Никакие операции над входными данными не были произведены. Нажмите кнопку расшифровать или зашифровать для заполнения поля");
                return;
            }
            if (fileSv.SaveFile(outContent))
            {
                isSaved = true;
                MessageBox.Show("Файл успешно сохранён");
            }
            
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string keyWord = KeyBox.Text;
            if (keyWord == "")
            {
                MessageBox.Show("Поле для ввода ключа пустое. Заполните, пожалуйста, поле русскими буквами. Все остальные символы будут проигнорированы");
                return;
            }
            if (inpContent == null)
            {
                MessageBox.Show("Файл ещё не был загружен. Загрузите файл и попробуйте снова");
                return;
            }
            string[] temp = new TextEncoder().Encrypt(inpContent, keyWord);
            if(temp.Length != 0)
            {
                outContent = temp;
            }
            else
            {
                MessageBox.Show("Поле для ввода ключа содержит исключительно недопустимые символы. Заполните, пожалуйста, поле корректно");
                return;
            }
            DataOut.Text = "";
            foreach (string item in outContent)
            {
                DataOut.Text += item;
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            string keyWord = KeyBox.Text;
            if(keyWord == "")
            {
                MessageBox.Show("Поле для ввода ключа пустое. Заполните, пожалуйста, поле русскими буквами. Все остальные символы будут проигнорированы");
                return;
            }
            if(inpContent == null)
            {
                MessageBox.Show("Файл ещё не был загружен. Загрузите файл и попробуйте снова");
                return;
            }
            string[] temp = new TextEncoder().Decrypt(inpContent, keyWord);
            if (temp.Length != 0)
            {
                outContent = temp;
            }
            else
            {
                MessageBox.Show("Поле для ввода ключа содержит исключительно недопустимые символы.Заполните, пожалуйста, поле корректно");
                return;
            }
            DataOut.Text = "";
            foreach (string item in outContent)
            {
                DataOut.Text += item;
            }
        }
        private void Load_docx_Click(object sender, RoutedEventArgs e)
        {
           ImportButton_Click(sender, e);
        }

        private void Load_txt_Click(object sender, RoutedEventArgs e)
        {
            inpContent = fileImporter.ImportFile(false);
            DataIn.Text = "";
            DataOut.Text = "";
            outContent = null;
            if (inpContent == null) return;
            foreach (string item in inpContent)
            {
                DataIn.Text += item;
            }
        }

        private void Open_docx_Click(object sender, RoutedEventArgs e)
        {
            fileImporter.OpenFile(true);
        }

        private void Open_txt_Click(object sender, RoutedEventArgs e)
        {
            fileImporter.OpenFile(false);
        }

        private void Save_cond_docx_Click(object sender, RoutedEventArgs e)
        {
            SaveButton_Click(sender, e);
        }

        private void Save_cond_txt_Click(object sender, RoutedEventArgs e)
        {
            if (outContent == null)
            {
                MessageBox.Show("Никакие операции над входными данными не были произведены. Нажмите кнопку расшифровать или зашифровать для заполнения поля");
                return;
            }
            if (fileSv.SaveFile(outContent, false, false, false)) 
            {
                isSaved = true;
                MessageBox.Show("Файл успешно сохранён");
            }
        }

        private void Save_direct_docx_Click(object sender, RoutedEventArgs e)
        {
            if (outContent == null)
            {
                MessageBox.Show("Никакие операции над входными данными не были произведены. Нажмите кнопку расшифровать или зашифровать для заполнения поля");
                return;
            }
            if (fileSv.SaveFile(outContent,false,true,true))
            {
                isSaved = true;
                MessageBox.Show("Файл успешно сохранён");
            }
        }

        private void Save_direct_txt_Click(object sender, RoutedEventArgs e)
        {
            if (outContent == null)
            {
                MessageBox.Show("Никакие операции над входными данными не были произведены. Нажмите кнопку расшифровать или зашифровать для заполнения поля");
                return;
            }
            if (fileSv.SaveFile(outContent,false,true,false))
            {
                isSaved = true;
                MessageBox.Show("Файл успешно сохранён");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuEncrypt_Click(object sender, RoutedEventArgs e)
        {
            EncryptButton_Click(sender, e);
        }

        private void MenuDecrypt_Click(object sender, RoutedEventArgs e)
        {
            DecryptButton_Click(sender, e);
        }

        private void MenuKeyEnter_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            KeyWindow window = new KeyWindow();
            window.ShowDialog();
            KeyBox.Text = window.correctKey;
            this.Visibility = Visibility.Visible;
        }

        private void MenuKeyBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            KeyBox.Text = MenuKeyBox.Text;
        }

        private void Beatify_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Beatify.IsChecked)
            {
                DecryptButton.Style = (Style)FindResource("RoundButtonTemplate");
                EncryptButton.Style = (Style)FindResource("RoundButtonTemplate");

                SaveButton.Style = DecryptButton.Style;
                ImportButton.Style = DecryptButton.Style;

                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    new BitmapImage(new Uri(@"../../images/Japan.jpg", UriKind.Relative));
                this.Background = myBrush;
                DataIn.BorderBrush = new SolidColorBrush(Colors.Black);
                DataIn.BorderThickness = new Thickness(1, 1, 1, 1);
                DataOut.BorderBrush = new SolidColorBrush(Colors.Black);
                DataOut.BorderThickness = new Thickness(1, 1, 1, 1);
                KeyBox.BorderBrush = new SolidColorBrush(Colors.Black);
                KeyBox.BorderThickness = new Thickness(1, 1, 1, 1);

            }
            else
            {
                DecryptButton.Style = null;
                EncryptButton.Style = null;
                SaveButton.Style = null;
                ImportButton.Style = null;
                Background = new SolidColorBrush(Colors.White);
                DataIn.BorderBrush = new SolidColorBrush(Colors.LightGray);
                DataOut.BorderBrush = new SolidColorBrush(Colors.LightGray);
                KeyBox.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void DataIn_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            /*
            List<string> list = new List<string>();
            for (int i = 0; i < DataIn.LineCount; i++)
            {
                list.Add(DataIn.GetLineText(i));   
            }
            inpContent = list.ToArray();
            */
        }
    }
}
