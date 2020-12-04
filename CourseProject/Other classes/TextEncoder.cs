using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Other_classes
{
    public class TextEncoder
    {
        //зашифровать
        public static List<char> RusAlphabet = new List<char>() { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        public static List<char> RusUpperAlphabet = new List<char>() { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };

        public string[] Encrypt(string[] decryptedData, string keyword)
        {
            if(decryptedData == null)
            {
                return null;
            }
            if(keyword == null)
            {
                return null;
            }
            string editedKeyword = GetEditedKeyword(keyword);
            if (editedKeyword == "")
            {
                return new string[0];
            }
            string[] encryptedData = new string[decryptedData.Length];
            for (int i = 0; i < decryptedData.Length; i++)
            {
                string decryptedString = decryptedData[i];
                if (decryptedString == null)
                {
                    encryptedData[i] = "";
                    continue;
                }
                string key = "";
                
                    string encryptedString = "";
                int counter = 0;
                for (int j = 0; j < decryptedString.Length; j++)
                {

                    if (RusAlphabet.Contains(decryptedString[j]))
                    {
                        key += editedKeyword[counter % editedKeyword.Length];
                        
                        encryptedString += RusAlphabet[(RusAlphabet.FindIndex((x) => x == decryptedString[j]) + RusAlphabet.FindIndex((x) => x == key[j])) % 33];
                        counter++;
                    }
                    else if(RusUpperAlphabet.Contains(decryptedString[j]))
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
                int curIndex = RusAlphabet.FindIndex((x) => x == keyword[i]);
                int curUpperIndex = RusUpperAlphabet.FindIndex((x) => x == keyword[i]);
                if (curIndex != -1)
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
