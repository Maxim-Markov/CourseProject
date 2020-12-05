using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using System.Diagnostics;

namespace CourseProject.Other_classes
{
    
    public class FileImporter
    {
        private static WordHandler wh = new WordHandler();
        //позволяет загрузить массив строк текста из файла с указанием формата .docx(.doc) или .txt
        public string[] ImportFile(bool isWordFormat)
        {
            if (isWordFormat)
            {
                return ImportDocxFile();
            }
            else
            {
                return ImportTxtFile();
            }
        }
        //позволяет открыть файл в приложении указанием формата .docx(.doc) или .txt
        public bool OpenFile(bool isWordFormat)
        {
            if (isWordFormat)
            {
                return OpenWordFile();
            }
            else
            {
                return OpenTxtFile();
            }
        }

        private string[] ImportTxtFile()
        {
            //открываем проводник
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text documents (.txt)|*.txt";
            try
            {
                ofd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            if (ofd.FileName != "") // проверка на выбор файла
            {
                string[] allText = null;
                List<string> allEditedText = new List<string>();

                //проверка на кодировку UTF или дефолтную системы
                Byte[] bytes = File.ReadAllBytes(ofd.FileName);
                    Encoding encoding = null;
                    String text = null;
                UTF8Encoding encUtf8NoBom = new UTF8Encoding(false, true);
                try
                { 
                text = encUtf8NoBom.GetString(bytes);
                encoding = encUtf8NoBom;
                  }
                 catch (ArgumentException)
            {
                // Подтверждение, что кодировка не UTF8
            }
        
                //используем кодировку системы по умолчанию
                if (encoding == null)
                {
                 encoding = Encoding.Default;
                }

                try
                {
                    allText = File.ReadAllLines(ofd.FileName, encoding);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
                foreach (string item in allText)
                    {
                        if(item != "") 
                        allEditedText.Add(item + "\r");
                    
                    }  
                return allEditedText.ToArray();
            }
            else
            {
                MessageBox.Show("Файл не выбран");
                return null;
            }
        }

        private string[] ImportDocxFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text documents (.docx)|*.docx";
            try
            {
                ofd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            if (ofd.FileName != "") // проверка на выбор файла
            {
                if (!wh.OpenWord(ofd.FileName, true,false)) return null;//открываем файл для чтения
                string[] allText = wh.GetAllText();
                wh.CloseWord();
                return allText;
            }
            else
            {
                MessageBox.Show("Файл не выбран");
                return null;
            }
        }
        private bool OpenTxtFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text documents (.txt)|*.txt";
            try
            {
                ofd.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            if (ofd.FileName != "") // проверка на выбор файла
            {
                try
                {
                    Process.Start(ofd.FileName);//откроется в связанном с типом формата приложении

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Файл не выбран");
                return false;
            }
        }

        private bool OpenWordFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text documents (.docx)|*.docx";
            try
            {
                ofd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            if (ofd.FileName != "") // проверка на выбор файла
            {
                
                return wh.OpenWord(ofd.FileName,false,true);//с возможностью редактирования документа
            }
            else
            {
                MessageBox.Show("Файл не выбран");
                return false;
            }
        }
    }
}
