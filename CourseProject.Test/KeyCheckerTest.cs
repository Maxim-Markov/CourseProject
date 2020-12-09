using CourseProject.Other_classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Test
{
    [TestClass]
    public class KeyCheckerTest
    {

        [TestMethod]
        public void CheckKey_EmptyKey_Returnnull()
        {
            string[] decryptedData = null;
            string key = "";

            TextEncoder encoder = new TextEncoder();
            string[] actual = encoder.Encrypt(decryptedData, key);

            Assert.IsNull(actual);
        }
    }
}
