using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCryptography
    {
        /********************ENCRYPT********************/
        [TestMethod]
        public void TestEncryptFileListOk()
        {
            var dir = ConfigurationManager.AppSettings.Get("test_dir");
            var key = ConfigurationManager.AppSettings.Get("secret_key");
            var md5_1 = ConfigurationManager.AppSettings.Get("txt_file_md5_1").ToUpper();
            var md5_2 = ConfigurationManager.AppSettings.Get("txt_file_md5_2").ToUpper();
            var md5_3 = ConfigurationManager.AppSettings.Get("txt_file_md5_3").ToUpper();
            var testList = GenerateTxtFilesList().ToList();
            var file1 = Path.ChangeExtension(testList.ElementAt(0), "acm");
            var file2 = Path.ChangeExtension(testList.ElementAt(1), "acm");
            var file3 = Path.ChangeExtension(testList.ElementAt(2), "acm");

            //OK return
            var sol = new List<string>()
            {
                "File successfully encrypted: " + file1 + "\r\nMD5 = " + md5_1,
                "File successfully encrypted: " + file2 + "\r\nMD5 = " + md5_2,
                "File successfully encrypted: " + file3 + "\r\nMD5 = " + md5_3,
            };
            CollectionAssert.AreEqual(ClassAcm.EncryptFileList(testList, dir, key).ToList(), sol);         
            Assert.AreEqual(ClassAcm.EncryptFileList(testList, "./umpalumpa", key).ToList().ElementAt(0), "Exception on encryption: Directory not found");
            Assert.AreEqual(ClassAcm.EncryptFileList(testList, dir, "").ToList().ElementAt(0), "Exception on encryption: Null key");
        }

        [TestMethod] 
        public void TestDecryptFileList()
        {
            var dir = ConfigurationManager.AppSettings.Get("test_dir");
            var key = ConfigurationManager.AppSettings.Get("secret_key");
            var testList = GenerateAcmFilesList().ToList();
            var sol = new List<string>()
            {
                "File successfully decrypted: " + dir + "/" + Path.GetFileNameWithoutExtension(testList.ElementAt(0)) +
                "_dec.txt",
                "File successfully decrypted: " + dir + "/" + Path.GetFileNameWithoutExtension(testList.ElementAt(1)) +
                "_dec.txt",
                "File successfully decrypted: " + dir + "/" + Path.GetFileNameWithoutExtension(testList.ElementAt(2)) +
                "_dec.txt"
            };
            CollectionAssert.AreEqual(ClassAcm.DecryptFileList(testList, dir, key).ToList(), sol);
            Assert.AreEqual(ClassAcm.DecryptFileList(testList, "./umpalumpa", key).ToList().ElementAt(0), "Exception on decryption: Directory not found");
            Assert.AreEqual(ClassAcm.DecryptFileList(testList, dir, "").ToList().ElementAt(0), "Exception on decryption: Null key");
        }

        [TestMethod]
        public void TestVerifyMd5AcmList()
        {
            var key = ConfigurationManager.AppSettings.Get("secret_key");
            var md5_1 = ConfigurationManager.AppSettings.Get("txt_file_md5_1").ToUpper();
            var md5_2 = ConfigurationManager.AppSettings.Get("txt_file_md5_2").ToUpper();
            var md5_3 = ConfigurationManager.AppSettings.Get("txt_file_md5_3").ToUpper();
            var testList = GenerateAcmFilesList().ToList();
            var sol = new List<string>()
            {
                testList.ElementAt(0) + " = " + md5_1 + " successfully verified",
                testList.ElementAt(1) + " = " + md5_2 + " successfully verified",
                testList.ElementAt(2) + " = " + md5_3 + " successfully verified"
            };
            CollectionAssert.AreEqual(ClassAcm.VerifyAcmMd5List(testList, key).ToList(), sol);
        }

        [TestMethod]
        public void TestCalculateMd5TxtList()
        {
            var md5_1 = ConfigurationManager.AppSettings.Get("txt_file_md5_1").ToUpper();
            var md5_2 = ConfigurationManager.AppSettings.Get("txt_file_md5_2").ToUpper();
            var md5_3 = ConfigurationManager.AppSettings.Get("txt_file_md5_3").ToUpper();
            var testList = GenerateTxtFilesList().ToList();
            var sol = new List<string>
            {
                testList.ElementAt(0) + " = " + md5_1,
                testList.ElementAt(1) + " = " + md5_2,
                testList.ElementAt(2) + " = " + md5_3
            };
            CollectionAssert.AreEqual(ClassAcm.CalculateTxtMd5List(testList).ToList(), sol);
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

        private static IEnumerable<string> GenerateAcmFilesList()
        {
            var dir = ConfigurationManager.AppSettings.Get("test_dir");
            var md5_1 = ConfigurationManager.AppSettings.Get("txt_file_md5_1").ToUpper();
            var md5_2 = ConfigurationManager.AppSettings.Get("txt_file_md5_2").ToUpper();
            var md5_3 = ConfigurationManager.AppSettings.Get("txt_file_md5_3").ToUpper();
            var aes1 = ConfigurationManager.AppSettings.Get("aes_1");
            var aes2 = ConfigurationManager.AppSettings.Get("aes_2");
            var aes3 = ConfigurationManager.AppSettings.Get("aes_3");
            var list = new List<string> {dir + "/test1.acm", dir + "/test2.acm", dir + "/test3.acm"};
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            foreach (var l in list)
            {
                if (File.Exists(l)) File.Delete(l);
                using (var sw = new StreamWriter(l, true))
                {
                    sw.WriteLine("ACM");
                    sw.WriteLine("File=" + Path.GetFileName(l));
                    switch (list.IndexOf(l))
                    {
                        case 0:
                            sw.WriteLine("Hash=" + md5_1);
                            sw.WriteLine("Size=" + aes1.Length);
                            sw.WriteLine("Type=AES");
                            sw.WriteLine("@@@@@");
                            sw.WriteLine(aes1);
                            break;
                        case 1:
                            sw.WriteLine("Hash=" + md5_2);
                            sw.WriteLine("Size=" + aes2.Length);
                            sw.WriteLine("Type=AES");
                            sw.WriteLine("@@@@@");
                            sw.WriteLine(aes2);
                            break;
                        case 2:
                            sw.WriteLine("Hash=" + md5_3);
                            sw.WriteLine("Size=" + aes3.Length);
                            sw.WriteLine("Type=AES");
                            sw.WriteLine("@@@@@");
                            sw.WriteLine(aes3);
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