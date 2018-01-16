using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCryptography
    {
        [TestMethod]
        public void TestMd5TxtList()
        {                        
            var dir = ConfigurationManager.AppSettings.Get("test_dir");            
            var text_1 = ConfigurationManager.AppSettings.Get("txt_file_text_1");
            var md5_1 = ConfigurationManager.AppSettings.Get("txt_file_md5_1").ToUpper();

            var list = new List<string>() { dir + "/test1.txt", dir + "/test2.txt", dir + "/test3.txt" };
            var sol = new List<string>() { list.ElementAt(0) + " = " + md5_1, list.ElementAt(1) + " = " + md5_1, list.ElementAt(2) + " = " + md5_1 };

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);                

            foreach (var l in list)
            {
                if (File.Exists(l))
                    File.Delete(l);
                using (StreamWriter sw = new StreamWriter(l, true))
                {
                    sw.WriteLine(text_1);
                    sw.Close();
                }
            }

            CollectionAssert.AreEqual(ClassAcm.CalculateTxtMd5List(list).ToList(), sol); 
        }
    }
}
