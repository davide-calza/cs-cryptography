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
    public class UnitTestFileManager
    {
        [TestMethod]
        public void TestMd5TxtList()
        {                        
            var dir = ConfigurationManager.AppSettings.Get("test_dir");  
            var text = ConfigurationManager.AppSettings.Get("txt_file_text");
            var md5 = ConfigurationManager.AppSettings.Get("txt_file_md5");

            var list = new List<string>() { dir + "/test1.txt", dir + "/test2.txt", dir + "/test3.txt" };
            var sol = new List<string>() { list.ElementAt(0) + " = " + md5, list.ElementAt(1) + " = " + md5, list.ElementAt(2) + " = " + md5 };

            if (Directory.Exists(dir))
            {
                Directory.Delete(dir);
                Directory.CreateDirectory(dir);
            }

            foreach (var l in list)
            {                

                if (File.Exists(l))
                {
                    File.Delete(l);
                    File.Create(l);
                    File.AppendText(text);
                }
            }

            var testList = ClassAcm.CalculateTxtMd5List(list);

            foreach (var l in testList)
            {
                MessageBox.Show(l.ToString());
            }

            Assert.AreEqual(ClassAcm.CalculateTxtMd5List(list), sol); 
        }
    }
}
