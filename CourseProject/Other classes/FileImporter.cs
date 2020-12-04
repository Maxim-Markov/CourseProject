using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows;

using System.Reflection;
using System.Diagnostics;

namespace CourseProject.Other_classes
{
    
    public class FileImporter
    {
        private static WordHandler wh = new WordHandler();
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
                try
                {
                    allText = File.ReadAllLines(ofd.FileName);

                    foreach (string item in allText)
                    {
                        if(item != "") allEditedText.Add(item + "\r");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
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
                if (!wh.OpenWord(ofd.FileName, true)) return null;//открываем файл для чтения
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
                    Process.Start(ofd.FileName);

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
                
                return wh.OpenWord(ofd.FileName,false);
            }
            else
            {
                MessageBox.Show("Файл не выбран");
                return false;
            }
        }
    }
}
