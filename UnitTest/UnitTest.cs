using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var text_2 = ConfigurationManager.AppSettings.Get("txt_file_text_2");
            var md5_2 = ConfigurationManager.AppSettings.Get("txt_file_md5_2").ToUpper();
            var text_3 = ConfigurationManager.AppSettings.Get("txt_file_text_3");
            var md5_3 = ConfigurationManager.AppSettings.Get("txt_file_md5_3").ToUpper();
            var list = new List<string> {dir + "/test1.txt", dir + "/test2.txt", dir + "/test3.txt"};
            var sol = new List<string>
            {
                list.ElementAt(0) + " = " + md5_1,
                list.ElementAt(1) + " = " + md5_2,
                list.ElementAt(2) + " = " + md5_3
            };
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            foreach (var l in list)
            {
                if (File.Exists(l)) File.Delete(l);
                using (var sw = new StreamWriter(l, true))
                {
                    switch (list.IndexOf(l))
                    {
                        case 0:
                            sw.WriteLine(text_1);
                            break;
                        case 1:
                            sw.WriteLine(text_2);
                            break;
                        case 2:
                            sw.WriteLine(text_3);
                            break;
                        default: break;
                    }

                    sw.Close();
                }
            }

            CollectionAssert.AreEqual(ClassAcm.CalculateTxtMd5List(list).ToList(), sol);
        }
    }
}