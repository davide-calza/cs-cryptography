using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCryptography
    {
        [TestMethod]
        public void TestMd5TxtList()
        {
            var md5_1 = ConfigurationManager.AppSettings.Get("txt_file_md5_1").ToUpper();
            var md5_2 = ConfigurationManager.AppSettings.Get("txt_file_md5_2").ToUpper();
            var md5_3 = ConfigurationManager.AppSettings.Get("txt_file_md5_3").ToUpper();
            var list = GenerateTxtFilesList();
            var enumerable = list as string[] ?? list.ToArray();
            var sol = new List<string>
            {
                enumerable.ElementAt(0) + " = " + md5_1,
                enumerable.ElementAt(1) + " = " + md5_2,
                enumerable.ElementAt(2) + " = " + md5_3
            };
            CollectionAssert.AreEqual(ClassAcm.CalculateTxtMd5List(enumerable).ToList(), sol);
        }

        private static IEnumerable<string> GenerateTxtFilesList()
        {
            var dir = ConfigurationManager.AppSettings.Get("test_dir");
            var text1 = ConfigurationManager.AppSettings.Get("txt_file_text_1");
            var text2 = ConfigurationManager.AppSettings.Get("txt_file_text_2");
            var text3 = ConfigurationManager.AppSettings.Get("txt_file_text_3");
            var list = new List<string> {dir + "/test1.txt", dir + "/test2.txt", dir + "/test3.txt"};
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            foreach (var l in list)
            {
                if (File.Exists(l)) File.Delete(l);
                using (var sw = new StreamWriter(l, true))
                {
                    switch (list.IndexOf(l))
                    {
                        case 0:
                            sw.WriteLine(text1);
                            break;
                        case 1:
                            sw.WriteLine(text2);
                            break;
                        case 2:
                            sw.WriteLine(text3);
                            break;
                        default: break;
                    }

                    sw.Close();
                }
            }

            return list;
        }
    }
}