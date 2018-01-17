using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System;
using System.Text.RegularExpressions;

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
            //OK
            CollectionAssert.AreEqual(ClassAcm.EncryptFileList(testList, dir, key).ToList(), sol);
            //Exceptions
            Assert.AreEqual(ClassAcm.EncryptFileList(new List<string>() {"./umpalumpa.txt"}, dir, key).ToList().ElementAt(0), "Exception on encryption: File not found");
            Assert.AreEqual(ClassAcm.EncryptFileList(testList, "./umpalumpa", key).ToList().ElementAt(0), "Exception on encryption: Directory not found");
            Assert.AreEqual(ClassAcm.EncryptFileList(testList, dir, "").ToList().ElementAt(0), "Exception on encryption: Null key");
            File.Create(Path.ChangeExtension(testList.ElementAt(0), ".docx")).Close();
            Assert.AreEqual(ClassAcm.EncryptFileList(new List<string>() {Path.ChangeExtension(testList.ElementAt(0), ".docx")}, dir, key).ToList().ElementAt(0), "Exception on encryption: Wrong file format. This function can encrypt only .txt files");
            File.Create(dir + "/empty.txt").Close();
            Assert.AreEqual(ClassAcm.EncryptFileList(new List<string>() { dir+"/empty.txt" }, dir, key).ToList().ElementAt(0), "Exception on encryption: Empty file");

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
            //OK
            CollectionAssert.AreEqual(ClassAcm.DecryptFileList(testList, dir, key).ToList(), sol);
            //Exceptions//Exceptions
            Assert.AreEqual(ClassAcm.DecryptFileList(new List<string>() { "./umpalumpa.txt" }, dir, key).ToList().ElementAt(0), "Exception on decryption: File not found");
            Assert.AreEqual(ClassAcm.DecryptFileList(testList, "./umpalumpa", key).ToList().ElementAt(0), "Exception on decryption: Directory not found");
            Assert.AreEqual(ClassAcm.DecryptFileList(testList, dir, "").ToList().ElementAt(0), "Exception on decryption: Null key");
            File.Create(Path.ChangeExtension(testList.ElementAt(0), ".docx")).Close();
            Assert.AreEqual(ClassAcm.DecryptFileList(new List<string>() { Path.ChangeExtension(testList.ElementAt(0), ".docx") }, dir, key).ToList().ElementAt(0), "Exception on decryption: Wrong file format. This function can decrypt only .acm files");
            File.Create(dir + "/empty.acm").Close();
            Assert.AreEqual(ClassAcm.DecryptFileList(new List<string>() { dir + "/empty.acm" }, dir, key).ToList().ElementAt(0), "Exception on decryption: Empty file");
            File.WriteAllText(testList.ElementAt(0), File.ReadAllText(testList.ElementAt(0)).Replace("Hash=", "Hash=r"));
            Assert.AreEqual(ClassAcm.DecryptFileList(testList, dir, key).ToList().ElementAt(0), "Exception on decryption: MD5 hash functions not corresponding");
        }

        [TestMethod]
        public void TestVerifyMd5AcmList()
        {
            var dir = ConfigurationManager.AppSettings.Get("test_dir");
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
            //OK
            CollectionAssert.AreEqual(ClassAcm.VerifyAcmMd5List(testList, key).ToList(), sol);
            //Exceptions
            Assert.AreEqual(ClassAcm.VerifyAcmMd5List(new List<string>() { "./umpalumpa.txt" }, key).ToList().ElementAt(0), "./umpalumpa.txt = Exception on MD5 verification: File not found");
            Assert.AreEqual(ClassAcm.VerifyAcmMd5List(testList, "").ToList().ElementAt(0),testList.ElementAt(0) + " = Exception on MD5 verification: Null key");
            File.Create(Path.ChangeExtension(testList.ElementAt(0), ".docx")).Close();
            Assert.AreEqual(ClassAcm.VerifyAcmMd5List(new List<string>() { Path.ChangeExtension(testList.ElementAt(0), ".docx") }, key).ToList().ElementAt(0), Path.ChangeExtension(testList.ElementAt(0), ".docx") + " = Exception on MD5 verification: Wrong file format. This function can verify only .acm files");
            File.Create(dir + "/empty.acm").Close();
            Assert.AreEqual(ClassAcm.VerifyAcmMd5List(new List<string>() { dir + "/empty.acm" }, key).ToList().ElementAt(0), dir + "/empty.acm" + " = Exception on MD5 verification: Empty file");
            File.WriteAllText(testList.ElementAt(0), File.ReadAllText(testList.ElementAt(0)).Replace("Hash=", "Hash=r"));
            Assert.AreEqual(ClassAcm.VerifyAcmMd5List(testList, key).ToList().ElementAt(0), testList.ElementAt(0) + " = Exception on MD5 verification: MD5 hash functions not corresponding");
            File.Delete(testList.ElementAt(0));
            File.Create(testList.ElementAt(0)).Close();
            using (var sw = new StreamWriter(testList.ElementAt(0), true))
            {                
                sw.WriteLine("ACM");
                sw.WriteLine("File=" + Path.GetFileName(testList.ElementAt(0)));
                sw.WriteLine("Hash=");
                sw.WriteLine("Size=" + ConfigurationManager.AppSettings.Get("aes_1").Length);
                sw.WriteLine("Type=AES");
                sw.WriteLine("@@@@@");
                sw.WriteLine(ConfigurationManager.AppSettings.Get("aes_1"));
                sw.Close();
            }
                Assert.AreEqual(ClassAcm.VerifyAcmMd5List(testList, key).ToList().ElementAt(0), testList.ElementAt(0) + " = Exception on MD5 verification: Error on MD5 calculation");
        }

        [TestMethod]
        public void TestCalculateMd5TxtList()
        {
            var dir = ConfigurationManager.AppSettings.Get("test_dir");
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
            //OK
            CollectionAssert.AreEqual(ClassAcm.CalculateTxtMd5List(testList).ToList(), sol);
            //Exceptions
            Assert.AreEqual(ClassAcm.CalculateTxtMd5List(new List<string>() { "./umpalumpa.txt" }).ToList().ElementAt(0), "./umpalumpa.txt = Exception on MD5 calculation: File not found");
            File.Create(Path.ChangeExtension(testList.ElementAt(0), ".docx")).Close();
            Assert.AreEqual(ClassAcm.CalculateTxtMd5List(new List<string>() { Path.ChangeExtension(testList.ElementAt(0), ".docx") }).ToList().ElementAt(0), Path.ChangeExtension(testList.ElementAt(0), ".docx") + " = Exception on MD5 calculation: Wrong file format. This function can encrypt only .txt files");
            File.Create(dir + "/empty.txt").Close();
            Assert.AreEqual(ClassAcm.CalculateTxtMd5List(new List<string>() { dir + "/empty.txt" }).ToList().ElementAt(0), dir + "/empty.txt" + " = Exception on MD5 calculation: Empty file");
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