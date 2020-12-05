using System;
using System.Windows;
using System.IO;
using CourseProject.Other_classes;
using Microsoft.Win32;


namespace CourseProject
{
    public class FileSaver
    {
        private static WordHandler wh = new WordHandler();

        //Позволяет создать файл .docx и записать в него содержимое
        public bool SaveFile(string[] content)
        {
            return SaveFile(content, false, false, true);
        }
        //сохраняет файл с указанием текста для записи, включение предупреждений о перезаписи, 
        //способ указания пути и имени файла, формат файла .docx(doc) или .txt
        public bool SaveFile(string[] content, bool isOverwriteExisting, bool isDirect, bool isWordFile)
        {
            if (isDirect)
            {
                string filePath = GetDirectPath(isOverwriteExisting, isWordFile);
                if (filePath == null)
                {
                    MessageBox.Show("Вы отменили сохранение файла, файл не сохранён!");
                    return false;
                }
                if (isWordFile)
                {
                    return SaveWordFile(content, filePath);
                }
                else
                {

                    return SavetxtFile(content, filePath);
                }
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();


                if (isWordFile)
                {
                    sfd.Filter = "Text documents (.docx)|*.docx";
                    sfd.OverwritePrompt = !isOverwriteExisting;
                    try
                    {
                        sfd.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                    if (sfd.FileName == "")
                    {
                        MessageBox.Show("Файл не выбран");
                        return false;
                    }
                    return SaveWordFile(content, sfd.FileName);
                }
                else
                {
                    sfd.Filter = "Text documents (.txt)|*.txt";
                    sfd.OverwritePrompt = !isOverwriteExisting;
                    try
                    {
                        sfd.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                    if (sfd.FileName == "")
                    {
                        MessageBox.Show("Файл не выбран");
                        return false;
                    }
                    return SavetxtFile(content, sfd.FileName);
                }

            }

        }
        
        private bool SaveWordFile(string[] content, string filePath)
        {

            if (!wh.CreateDocument()) return false;
            if (!wh.WriteAllText(content)) return false;
            if (!wh.SaveDocument(filePath)) return false;
            if (!wh.CloseWord()) return false;

            return true;
        }

        private bool SavetxtFile(string[] content, string filePath)
        {
            try
            {
                File.WriteAllLines(filePath, content);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false; 
            }
            return true;
        }

        //Позволяет выявить у пользователя корректный путь и имя файла
        private string GetDirectPath(bool isOverwriteExisting, bool isWordFile)
        {
            PathToSaveWind pathWind = new PathToSaveWind();//открытие нового окна для указания директории и имени файла

            if (pathWind.ShowDialog() == true)//нажата кнопка ОК
            {
                bool isSuccess = true;//флаг успеха операции
                string destinationPath = pathWind.DirectoryPath;
                string fileName = pathWind.FileName;
                try
                {
                    if (isWordFile)
                    {
                        if (File.Exists($@"{destinationPath}\{fileName}.docx"))
                        {
                            MessageBoxResult result = MessageBox.Show("Файл с таким именем уже существует в указанной директории. Желаете перезаписать файл?", "Перезапись файла", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                            if (result != MessageBoxResult.Yes)
                            {
                                return GetDirectPath(false, isWordFile);
                            }
                            else
                            {
                                File.Delete($@"{destinationPath}\{fileName}.docx");
                            }
                        }
                    }
                    else
                    {
                        if (File.Exists($@"{destinationPath}\{fileName}.txt"))
                        {
                            MessageBoxResult result = MessageBox.Show("Файл с таким именем уже существует в указанной директории. Желаете перезаписать файл?", "Перезапись файла", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                            if (result != MessageBoxResult.Yes)
                            {
                                return GetDirectPath(false, isWordFile);
                            }
                            else
                            {
                                File.Delete($@"{destinationPath}\{fileName}.txt");
                            }
                        }
                    }
                }

                catch (DirectoryNotFoundException)
                {
                    isSuccess = false;
                    MessageBox.Show("Указанной директории не существует, попробуйте ещё");
                }
                catch (IOException ex)
                {
                    isSuccess = false;
                    MessageBox.Show("Ошибка, что-то пошло не так... " + ex.Message);
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    MessageBox.Show(ex.Message);
                }

                if (!isSuccess)//если возникло исключение, пробуем заново сохранить файл
                {
                    return GetDirectPath(isOverwriteExisting, isWordFile);
                }

                if (isWordFile)
                    return $@"{destinationPath}\{fileName}.docx";
                else
                    return $@"{destinationPath}\{fileName}.txt";
            }
            else//Была нажата любая кнопка, кроме ОК
            {
                return null;
            }
        }
    }
}
