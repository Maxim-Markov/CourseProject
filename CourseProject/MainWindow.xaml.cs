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
        private static bool isSaved = false;//был ли файл сохранён
        private static FileImporter fileImporter = new FileImporter();
        private static FileSaver fileSv = new FileSaver();
        private string[] inpContent;//Загруженный текст из файла
        private string[] outContent;//Преобразованный текст
        
        public MainWindow()
        {
            InitializeComponent();
            
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        //Даём возможность сохранить файл, если этого не было сделано
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
        //Заполняет массив и текстовое поле данными
        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            inpContent = fileImporter.ImportFile(true);
            if (inpContent == null) return;
            DataIn.Text = "";
            DataOut.Text = "";
            outContent = null;
            foreach (string item in inpContent)
            {
                DataIn.Text += item;
            }
            
        }
        //отвечает за сохранение через проводник в .docx файл
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
        //Зашифровывает полученный текст из входного массива с помощью ключа, заполняет выходной массив и поле преобразованных данных
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
        //Расшифровывает полученный текст из входного массива с помощью ключа, заполняет выходной массив и поле преобразованных данных
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
        //загружает текст из файла .docx в входной массив и отображает его на экран
        private void Load_docx_Click(object sender, RoutedEventArgs e)
        {
           ImportButton_Click(sender, e);
        }
        //загружает текст из файла .txt в входной массив и отображает его на экран
        private void Load_txt_Click(object sender, RoutedEventArgs e)
        {
            inpContent = fileImporter.ImportFile(false);
            if (inpContent == null) return;
            DataIn.Text = "";
            DataOut.Text = "";
            outContent = null;           
            foreach (string item in inpContent)
            {
                DataIn.Text += item;
            }
        }
        // открывает файл.docx в word
        private void Open_docx_Click(object sender, RoutedEventArgs e)
        {
            fileImporter.OpenFile(true);
        }
        // открывает файл.txt в блокноте
        private void Open_txt_Click(object sender, RoutedEventArgs e)
        {
            fileImporter.OpenFile(false);
        }
        // сохраняет файл .docx через проводник
        private void Save_cond_docx_Click(object sender, RoutedEventArgs e)
        {
            SaveButton_Click(sender, e);
        }
        // сохраняет файл .txt через проводник
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
        // сохраняет файл .docx через ручное указание пути в созданную форму
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
            else
            {
                string msg = "Файл не сохранён. Желаете повторить попытку сохранения?";
                MessageBoxResult result = MessageBox.Show(msg, "Сохранение файла", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Save_direct_txt_Click(sender, e);
                }
            }
        }
        // сохраняет файл .txt через проводник
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
            else
            {
                string msg = "Файл не сохранён. Желаете повторить попытку сохранения?";
                MessageBoxResult result = MessageBox.Show(msg, "Сохранение файла", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Save_direct_txt_Click(sender, e);
                }
            }
        }
        // закрывает приложение
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
        //открывает диалоговое окно с безопасным вводом ключа
        private void MenuKeyEnter_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            KeyWindow window = new KeyWindow();
            window.ShowDialog();
            KeyBox.Text = window.correctKey;
            this.Visibility = Visibility.Visible;
        }
        //синхронизирует менюшный ввод ключа и основной
        private void MenuKeyBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            KeyBox.Text = MenuKeyBox.Text;
        }
        //делает красиво с помощью стиля кнопок, textbox, фона
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
            //для возможности ручного ввода текста в textBox DataIn
           /* DataIn.TextWrapping = TextWrapping.NoWrap;
            DataIn.IsReadOnly = false;
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
