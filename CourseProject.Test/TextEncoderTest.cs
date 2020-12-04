using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CourseProject.Other_classes;

namespace CourseProject.Test
{
    [TestClass]
    public class TextEncoderTest
    {
        private static string[] stringRusSymbArray;
        private static string randomKey = "";
        private static Random rand = new Random();
        private static TextEncoder encoder = new TextEncoder();
        [ClassInitialize]
        public static void EncoderInitialize(TestContext ts)
        {
            stringRusSymbArray = new string[6];
            stringRusSymbArray[0] = "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁ";
            stringRusSymbArray[1] = "йцукенгшщзхъфывапролджэячсмитьбюё";
            stringRusSymbArray[2] = "йцукеНгШЩзхъфЫвапролджэячсМИтЬбюё";
            stringRusSymbArray[3] = "йцукеНгШЩшщзхъфЫывапролджэячсМИимтЬбюё";
            stringRusSymbArray[4] = "Контекст";
            stringRusSymbArray[5] = "";
            for (int i = 0; i < rand.Next(1,101); i++)
            {
                randomKey += TextEncoder.RusAlphabet[rand.Next(0,33)];
                randomKey += TextEncoder.RusUpperAlphabet[rand.Next(0,33)];
            }
        }

        [TestMethod]
        public void Encrypt_NullStringEmptyKey_Returnnull()
        {
            string[] decryptedData = null;
            string key = "";

            TextEncoder encoder = new TextEncoder();
            string[] actual = encoder.Encrypt(decryptedData, key);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Encrypt_NullKey_Returnnull()
        {
            string[] decryptedData = stringRusSymbArray;
            string key = null;

            
            string[] actual = encoder.Encrypt(decryptedData, key);

            Assert.IsNull(actual);
        }
        [TestMethod]
        public void Encrypt_EmptyArrayAnykey_ReturnEmptyArray()
        {
            string[] decryptedData = new string[0];
            string key = randomKey;
            string[] expected = new string[0];


            string[] actual = encoder.Encrypt(decryptedData, key);

            Assert.AreEqual(expected.Length,actual.Length);
        }

        [TestMethod]
        public void Encrypt_NullArrayElementsAnykey_ReturnEmptyArrayElement()
        {
            string[] decryptedData = new string[5];
            for (int i = 0; i < 5; i++)
            {
                decryptedData[i] = "значение";
            }
            decryptedData[3] = null;
           
            string key = "скорпион";
            string[] actual = encoder.Encrypt(decryptedData, key);

            bool b = actual[3] == "";
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void Encrypt_ArrayWithEmptyElementsAnykey_ReturnEmptyArrayElements()
        {
            string[] decryptedData = new string[5];
            for (int i = 0; i < 5; i++)
            {
                decryptedData[i] = "";
            }
           

            string key = randomKey;
            string[] actual = encoder.Encrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(decryptedData[i], actual[i]);
            }
            
        }
        [TestMethod]
        public void Encrypt_ArrayWithNums_ReturnCryptedArrayWithNums()
        {
            string[] decryptedData = new string[5];
                decryptedData[0] = "значение123";
                decryptedData[1] = "123значение";
                decryptedData[2] = "456знач013ение789";
                decryptedData[3] = "456знач013ение789значение";
                decryptedData[4] = "456013789";
            string[] expected = new string[5]
            {
                "щшозфцчт123",
                "123щшозфцчт",
                "456щшоз013фцчт789",
                "456щшоз013фцчт789щшозфцчт",
                "456013789"
            };

            string key = "скорпион";
            string[] actual = encoder.Encrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void Encrypt_ArrayWithOtherSymbols_ReturnCryptedArrayWithOtherSymbols()
        {
            string[] decryptedData = new string[5]
            {
                "scorpioскорпион",
                "scorpioскорпионscorpio",
                 "простые тексты на английском для начинающих (A1 — Beginner и A2 — Elementary)",
                 "текст!\"№;%:?*()_+/=-@$%^&*|}{[]:?><,.~`'снова текст EnglishText",
                 "NowYouSeeSharp"
            };

            string[] expected = new string[5]
            {
                "scorpioгхэбясэы",
                 "scorpioгхэбясэыscorpio",
                 "быэввду ацхагк цо нянъщщъщью оъп эиёцякмйшю (A1 — Beginner ч A2 — Elementary)",
                  "дпщвв!\"№;%:?*()_+/=-@$%^&*|}{[]:?><,.~`'ъььук бхъъб EnglishText",
                 "NowYouSeeSharp"
            };

            string key = "скорпион";
            string[] actual = encoder.Encrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void Encrypt_ArrayWithBigLetters_ReturnCryptedArrayWithBigLetters()
        {
            string[] decryptedData = new string[5]
            {
                "ЭтОСтрОКАаБВГДИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯЁ",
                "scorpioскорПИонscorpio",
                "Нет, Здесь не (return) искать 123",
                "В начале строки",
                "в конце строкИ"
            };

            string[] expected = new string[5]
            {
                "ОэЭВвщЭШСкПТТМЧЧЬЦЫЮЮШЯЯДЮГЁЁАЖЖЛЁКННЗФ",
                "scorpioгхэбЯСэыscorpio",
                "Япб, Шунай яп (return) чвъибй 123",
                "У шозпфу ядыэыш",
                "у хэюён аавщщЩ"
            };

            string key = "скорпион";
            string[] actual = encoder.Encrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void Encrypt_EmptyKey_ReturnEmptyAr()
        {
            string[] expected = new string[0];

            string key = "";
            string[] actual = encoder.Encrypt(stringRusSymbArray, key);
          
                Assert.AreEqual(expected.Length, actual.Length);
            
        }

        [TestMethod]
        public void Encrypt_EmptyKeyBecauseofIgnoreSymbols_ReturnEmptyAr()
        {
            string[] expected = new string[0];

            string key = "it'sEnglishAndOtherSymbolsBadKEY~?><!@#$%^&*()_+|=-№;;%?*<>?,./']`[{}";
            string[] actual = encoder.Encrypt(stringRusSymbArray, key);

            Assert.AreEqual(expected.Length, actual.Length);

        }

        [TestMethod]
        public void Encrypt_KeyWithVariousSymbols_ReturnNull()
        {
            string badKey = "СкОРп123AndEnglish{something}И он here is it";
            string[] expected = encoder.Encrypt(stringRusSymbArray, badKey);

            string goodKey = "Скорпион";
            string[] actual = encoder.Encrypt(stringRusSymbArray, goodKey);

            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }

        }


        

        [TestMethod]
        public void Decrypt_NullStringEmptyKey_Returnnull()
        {
            string[] decryptedData = null;
            string key = "";

            TextEncoder encoder = new TextEncoder();
            string[] actual = encoder.Decrypt(decryptedData, key);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Decrypt_NullKey_Returnnull()
        {
            string[] decryptedData = stringRusSymbArray;
            string key = null;


            string[] actual = encoder.Decrypt(decryptedData, key);

            Assert.IsNull(actual);
        }
        [TestMethod]
        public void Decrypt_EmptyArrayAnykey_ReturnEmptyArray()
        {
            string[] decryptedData = new string[0];
            string key = randomKey;
            string[] expected = new string[0];


            string[] actual = encoder.Decrypt(decryptedData, key);

            Assert.AreEqual(expected.Length, actual.Length);
        }

        [TestMethod]
        public void Decrypt_NullArrayElementsAnykey_ReturnEmptyArrayElement()
        {
            string[] decryptedData = new string[5];
            for (int i = 0; i < 5; i++)
            {
                decryptedData[i] = "значение";
            }
            decryptedData[3] = null;

            string key = randomKey;
            string[] actual = encoder.Decrypt(decryptedData, key);

            bool b = actual[3] == "";
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void Decrypt_ArrayWithEmptyElementsAnykey_ReturnEmptyArrayElements()
        {
            string[] decryptedData = new string[5];
            for (int i = 0; i < 5; i++)
            {
                decryptedData[i] = "";
            }


            string key = randomKey;
            string[] actual = encoder.Decrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(decryptedData[i], actual[i]);
            }

        }
        [TestMethod]
        public void Decrypt_ArrayWithNums_ReturnCryptedArrayWithNums()
        {
            string[] expected = new string[5];
            expected[0] = "значение123";
            expected[1] = "123значение";
            expected[2] = "456знач013ение789";
            expected[3] = "456знач013ение789значение";
            expected[4] = "456013789";
            string[] decryptedData = new string[5]
            {
                "щшозфцчт123",
                "123щшозфцчт",
                "456щшоз013фцчт789",
                "456щшоз013фцчт789щшозфцчт",
                "456013789"
            };

            string key = "скорпион";
            string[] actual = encoder.Decrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        [TestMethod]
        public void Decrypt_ArrayWithOtherSymbols_ReturnCryptedArrayWithOtherSymbols()
        {
            string[] expected = new string[5]
            {
                "scorpioскорпион",
                "scorpioскорпионscorpio",
                 "простые тексты на английском для начинающих (A1 — Beginner и A2 — Elementary)",
                 "текст!\"№;%:?*()_+/=-@$%^&*|}{[]:?><,.~`'снова текст EnglishText",
                 "NowYouSeeSharp"
            };

            string[] decryptedData = new string[5]
            {
                "scorpioгхэбясэы",
                 "scorpioгхэбясэыscorpio",
                 "быэввду ацхагк цо нянъщщъщью оъп эиёцякмйшю (A1 — Beginner ч A2 — Elementary)",
                  "дпщвв!\"№;%:?*()_+/=-@$%^&*|}{[]:?><,.~`'ъььук бхъъб EnglishText",
                 "NowYouSeeSharp"
            };

            string key = "скорпион";
            string[] actual = encoder.Decrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void Decrypt_ArrayWithBigLetters_ReturnCryptedArrayWithBigLetters()
        {
            string[] expected = new string[5]
            {
                "ЭтОСтрОКАаБВГДИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯЁ",
                "scorpioскорПИонscorpio",
                "Нет, Здесь не (return) искать 123",
                "В начале строки",
                "в конце строкИ"
            };

            string[] decryptedData = new string[5]
            {
                "ОэЭВвщЭШСкПТТМЧЧЬЦЫЮЮШЯЯДЮГЁЁАЖЖЛЁКННЗФ",
                "scorpioгхэбЯСэыscorpio",
                "Япб, Шунай яп (return) чвъибй 123",
                "У шозпфу ядыэыш",
                "у хэюён аавщщЩ"
            };

            string key = "скорпион";
            string[] actual = encoder.Decrypt(decryptedData, key);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void Decrypt_EmptyKey_ReturnEmptyAr()
        {
            string[] expected = new string[0];

            string key = "";
            string[] actual = encoder.Decrypt(stringRusSymbArray, key);

            Assert.AreEqual(expected.Length, actual.Length);

        }

        [TestMethod]
        public void Decrypt_EmptyKeyBecauseofIgnoreSymbols_ReturnEmptyAr()
        {
            string[] expected = new string[0];

            string key = "it'sEnglishAndOtherSymbolsBadKEY~?><!@#$%^&*()_+|=-№;;%?*<>?,./']`[{}";
            string[] actual = encoder.Decrypt(stringRusSymbArray, key);

            Assert.AreEqual(expected.Length, actual.Length);

        }

        [TestMethod]
        public void Decrypt_KeyWithVariousSymbols_ReturnRightValue()
        {
            string badKey = "СкОРп123AndEnglish{something}И он here is it";
            string[] expected = encoder.Decrypt(stringRusSymbArray, badKey);

            string goodKey = "Скорпион";
            string[] actual = encoder.Decrypt(stringRusSymbArray, goodKey);

            for (int i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }

        }
    }
}
