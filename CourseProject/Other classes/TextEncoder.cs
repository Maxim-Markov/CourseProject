using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Other_classes
{
    //Реализация алгоритма следующая: В тексте шифруются только строчные русские буквы(квадрат Вижинере 33x33), 
    //заглавные русские буквы преобразуются в строчные, шифруются и снова преобразуются в заглавные.
    //Остальные символы в тексте игнорируются. В ключевом слове заглавные русские буквы преобразуются в строчные,
    //прочие символы перед шифром из ключевого слова удаляются. При получении ключа для каждой новой строки ключевое слово берётся от начала слова
    //Исключительные ситуации: данных нет - возврат null, ключевого слова нет - возврат null,
    //Преобразованное слово пустое - возврат пустого значения
    //если строка содержит null - в шифрованных данных она становится пустой
    //Сложность алгоритма O(n), где n - количество символов в тексте, худший случай - все большие буквы Я - количество проходов - n*k^4, где k - мощность алфавита
    public class TextEncoder
    {
        
        public static List<char> RusAlphabet = new List<char>() { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        public static List<char> RusUpperAlphabet = new List<char>() { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        //зашифровать данные с использованием указанного ключевого слова
        public string[] Encrypt(string[] decryptedData, string keyword)
        {
            if(decryptedData == null)//данных нет - возврат null
            {
                return null;
            }
            if(keyword == null)//ключевого слова нет - возврат null
            {
                return null;
            }
            string editedKeyword = GetEditedKeyword(keyword);//получаем преобразованное по алгоритму ключевое слово
            if (editedKeyword == "")//Преобразованное слово пустое - возврат пустого значения
            {
                return new string[0];
            }
            string[] encryptedData = new string[decryptedData.Length];//массив зашифрованных строк
            for (int i = 0; i < decryptedData.Length; i++)//для каждой строки текста
            {
                string decryptedString = decryptedData[i];
                if (decryptedString == null)//если строка содержит null - в зашифрованных данных она становится пустой
                {
                    encryptedData[i] = "";
                    continue;
                }

                string key = "";
                string encryptedString = "";
                int counter = 0;//счётчик задействованных символов для ключа
                for (int j = 0; j < decryptedString.Length; j++)//для каждого символа строки
                {

                    if (RusAlphabet.Contains(decryptedString[j]))//проверка, есть ли такой символ в русском алфавите
                    {
                        key += editedKeyword[counter % editedKeyword.Length];//получение символа ключа для текущего символа текста
                        //сложение порядковых номеров в русской азбуке символов текста и ключа, а затем взятие остатка от деления на мощность алфавита
                        encryptedString += RusAlphabet[(RusAlphabet.FindIndex((x) => x == decryptedString[j]) + RusAlphabet.FindIndex((x) => x == key[j])) % 33];
                        counter++;//ключевое слово циклически повторяется для русских букв, на месте остальных символов пробел
                    }
                    else if(RusUpperAlphabet.Contains(decryptedString[j]))//проверка для больших букв
                        {
                        int currTextIndex = RusUpperAlphabet.FindIndex((x) => x == decryptedString[j]);
                        key += editedKeyword[counter % editedKeyword.Length];
                        encryptedString += RusUpperAlphabet[(currTextIndex + RusAlphabet.FindIndex((x) => x == key[j])) % 33];
                        counter++;
                    }
                    else
                    {
                        key += " ";
                        encryptedString += decryptedString[j];
                    }
                }
                encryptedData[i] = encryptedString;
            }
            return encryptedData;
            
        }

        //расшифровать
        public string[] Decrypt(string[] encryptedData, string keyword)
        {
            if (encryptedData == null)
            {
                return null;
            }
            if (keyword == null)
            {
                return null;
            }
            string editedKeyword = GetEditedKeyword(keyword);
            if (editedKeyword == "")
            {
                return new string[0];
            }
            string[] decryptedData = new string[encryptedData.Length];
            for (int i = 0; i < encryptedData.Length; i++)
            {
                string encryptedString = encryptedData[i];
                if (encryptedString == null)
                {
                    decryptedData[i] = "";
                    continue;
                }
                string key = "";

                string decryptedString = "";
                int counter = 0;
                for (int j = 0; j < encryptedString.Length; j++)
                {

                    if (RusAlphabet.Contains(encryptedString[j]))
                    {
                        key += editedKeyword[counter % editedKeyword.Length];
                        decryptedString += RusAlphabet[ (33 + RusAlphabet.FindIndex((x) => x == encryptedString[j]) - RusAlphabet.FindIndex((x) => x == key[j])) % 33];
                        counter++;
                    }
                    else if (RusUpperAlphabet.Contains(encryptedString[j]))
                    {
                        int currTextIndex = RusUpperAlphabet.FindIndex((x) => x == encryptedString[j]);
                        key += editedKeyword[counter % editedKeyword.Length];
                        decryptedString += RusUpperAlphabet[(33 + currTextIndex - RusAlphabet.FindIndex((x) => x == key[j])) % 33];
                        counter++;
                    }
                    else
                    {
                        key += " ";
                        decryptedString += encryptedString[j];
                    }
                }
                decryptedData[i] = decryptedString;
            }
            return decryptedData;
        }
        private string GetEditedKeyword(string keyword)
        {
            string editedKeyword = "";
            for (int i = 0; i < keyword.Length; i++)
            {
                int curIndex = RusAlphabet.FindIndex((x) => x == keyword[i]);//ищет символ ключевого слова в русском алфавите
                int curUpperIndex = RusUpperAlphabet.FindIndex((x) => x == keyword[i]);
                if (curIndex != -1)// если нашли
                {
                    editedKeyword += RusAlphabet[curIndex];
                }
                if (curUpperIndex != -1)
                {
                    editedKeyword += RusAlphabet[curUpperIndex];
                }

            }
            return editedKeyword;
        }
    }
}
