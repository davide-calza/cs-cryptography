using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using CryptoLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System;
using System.Text.RegularExpressions;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCryptography
    {
        [TestMethod]
        public void TestACMEncryptFileListOk()
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
                $"File successfully encrypted: {file1}\r\nMD5 = {md5_1}",
                $"File successfully encrypted: {file2}\r\nMD5 = {md5_2}",
                $"File successfully encrypted: {file3}\r\nMD5 = {md5_3}",
            };
            //OK
            CollectionAssert.AreEqual(ClassAcm.EncryptFileList(testList, dir, key).ToList(), sol);
            //Exceptions
            Assert.AreEqual(ClassAcm.EncryptFileList(new List<string>() { "./umpalumpa.txt" }, outDir: dir, key: key).ToList().ElementAt(0), "Exception on encryption: File not found");
            Assert.AreEqual(ClassAcm.EncryptFileList(testList, "./umpalumpa", key).ToList().ElementAt(0), "Exception on encryption: Directory not found");
            Assert.AreEqual(ClassAcm.EncryptFileList(testList, dir, "").ToList().ElementAt(0), "Exception on encryption: Null key");
            File.Create(Path.ChangeExtension(testList.ElementAt(0), ".docx")).Close();
            Assert.AreEqual(ClassAcm.EncryptFileList(new List<string>() { Path.ChangeExtension(testList.ElementAt(0), ".docx") }, dir, key).ToList().ElementAt(0), "Exception on encryption: Wrong file format. This function can encrypt only .txt files");
            File.Create(dir + "/empty.txt").Close();
            Assert.AreEqual(ClassAcm.EncryptFileList(new List<string>() { dir + "/empty.txt" }, dir, key).ToList().ElementAt(0), "Exception on encryption: Empty file");

        }

        [TestMethod]
        public void TestACMDecryptFileList()
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
        public void TestACMVerifyMd5AcmList()
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
            Assert.AreEqual(ClassAcm.VerifyAcmMd5List(testList, "").ToList().ElementAt(0), testList.ElementAt(0) + " = Exception on MD5 verification: Null key");
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
        public void TestACMCalculateMd5TxtList()
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

        [TestMethod]
        public void Test_RSA()
        {
            var pubKey_1 = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var priKey_1 = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";

            var testo__1 = "testing ...";
            var testo__2 = "Hello World!";

            var s1_enc = CryptoUtils.EncryptRSA(testo__1, pubKey_1);
            var s1_dec = CryptoUtils.DecryptRSA(s1_enc, priKey_1);

            Assert.AreEqual(s1_dec, testo__1);

            var s2_enc = CryptoUtils.EncryptRSA(testo__2, pubKey_1);
            var s2_dec = CryptoUtils.DecryptRSA(s2_enc, priKey_1);

            Assert.AreEqual(s2_dec, testo__2);
        }

        [TestMethod]
        public void Test_Base64()
        {
            string Text1 = "Man";
            string Code1 = "TWFu";

            Assert.AreEqual(Text1, CryptoUtils.Base64_Decode(Code1));
            Assert.AreEqual(Code1, CryptoUtils.Base64_Encode(Text1));

            string Text2 = "I.T.T. Marconi Rovereto";
            string Code2 = "SS5ULlQuIE1hcmNvbmkgUm92ZXJldG8=";

            Assert.AreEqual(Text2, CryptoUtils.Base64_Decode(Code2));
            Assert.AreEqual(Code2, CryptoUtils.Base64_Encode(Text2));

            Assert.AreNotEqual(Code2, CryptoUtils.Base64_Encode("ITT - Marconi Rovereto"));
        }

        [TestMethod]
        public void TestRSAEncryptFile()
        {
            GenerateRSATestFiles();
            var rsaDir = ConfigurationManager.AppSettings.Get("rsa_dir");
            var input = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_txt_in");
            var output = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_rsa_out");
            var key = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_pub_key");

            //OK
            Assert.AreEqual(ClassRsa.Encrypt(input, output, key), "File successfully encrypted");
            //Exceptions
            Assert.AreEqual(ClassRsa.Encrypt("carciofo.txt", output, key), "Exception on encryption: carciofo.txt not found");
            Assert.AreEqual(ClassRsa.Encrypt(input, output, "carciofo.xml"), "Exception on encryption: carciofo.xml not found");
            Assert.AreEqual(ClassRsa.Encrypt(input, "./cartella/out.rsa", key), "Exception on encryption: Output directory not found");
            var testInput = rsaDir + '/' + Path.GetFileNameWithoutExtension(input) + ".docx";
            File.Create(testInput).Close();
            Assert.AreEqual(ClassRsa.Encrypt(testInput, output, key), "Exception on encryption: Wrong input file format");
            var testOutput = rsaDir + '/' + Path.GetFileNameWithoutExtension(output) + ".docx";
            File.Create(testOutput).Close();
            Assert.AreEqual(ClassRsa.Encrypt(input, testOutput, key), "Exception on encryption: Wrong output file format");
            var testKey = rsaDir + '/' + Path.GetFileNameWithoutExtension(key) + ".docx";
            File.Create(testKey).Close();
            Assert.AreEqual(ClassRsa.Encrypt(input, output, testKey), "Exception on encryption: Wrong key file format");
            testInput = input;
            File.WriteAllText(testInput, "");
            Assert.AreEqual(ClassRsa.Encrypt(testInput, output, key), "Exception on encryption: Empty text file");
            File.WriteAllText(testInput, "Prova 1");
            testKey = key;
            File.WriteAllText(testKey, "");
            Assert.AreEqual(ClassRsa.Encrypt(input, output, testKey), "Exception on encryption: Empty key file");
        }

        [TestMethod]
        public void TestRSADecryptFile()
        {
            GenerateRSATestFiles();
            GenerateRSATestFiles();
            var rsaDir = ConfigurationManager.AppSettings.Get("rsa_dir");
            var input = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_rsa_in");
            var output = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_txt_out");
            var key = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_priv_key");

            //OK
            Assert.AreEqual(ClassRsa.Decrypt(input, output, key), "File successfully decrypted");
            //Exceptions
            Assert.AreEqual(ClassRsa.Decrypt("carciofo.rsa", output, key), "Exception on decryption: carciofo.rsa not found");
            Assert.AreEqual(ClassRsa.Decrypt(input, output, "carciofo.xml"), "Exception on decryption: carciofo.xml not found");
            Assert.AreEqual(ClassRsa.Decrypt(input, "./cartella/out.txt", key), "Exception on decryption: Output directory not found");
            var testInput = rsaDir + '/' + Path.GetFileNameWithoutExtension(input) + ".docx";
            File.Create(testInput).Close();
            Assert.AreEqual(ClassRsa.Decrypt(testInput, output, key), "Exception on decryption: Wrong input file format");
            var testOutput = rsaDir + '/' + Path.GetFileNameWithoutExtension(output) + ".docx";
            File.Create(testOutput).Close();
            Assert.AreEqual(ClassRsa.Decrypt(input, testOutput, key), "Exception on decryption: Wrong output file format");
            var testKey = rsaDir + '/' + Path.GetFileNameWithoutExtension(key) + ".docx";
            File.Create(testKey).Close();
            Assert.AreEqual(ClassRsa.Decrypt(input, output, testKey), "Exception on decryption: Wrong key file format");
            testInput = input;
            File.WriteAllText(testInput, "");
            Assert.AreEqual(ClassRsa.Decrypt(testInput, output, key), "Exception on decryption: Empty rsa file");
            File.WriteAllText(testInput, "2sZQzP1EYkwxZm66llsuIPEzec4G1GzVpWljDuGfSc20INCO/JyfCfX7wvIE3GtuPjFtn0wKKlO0jxgPJrDchA5XPgCRvhJUzdtfWeW5Nclqs6PCID8hC00hYzncSW6+iPUKPrrXGMqs+mO2t08xFaRpXHmxDCaBCR++PLNjfmw=");
            testKey = key;
            File.WriteAllText(testKey, "");
            Assert.AreEqual(ClassRsa.Decrypt(input, output, testKey), "Exception on decryption: Empty key file");
        }

        private static IEnumerable<string> GenerateTxtFilesList()
        {
            var dir = ConfigurationManager.AppSettings.Get("test_dir");
            var text1 = ConfigurationManager.AppSettings.Get("txt_file_text_1");
            var text2 = ConfigurationManager.AppSettings.Get("txt_file_text_2");
            var text3 = ConfigurationManager.AppSettings.Get("txt_file_text_3");
            var list = new List<string> { dir + "/test1.txt", dir + "/test2.txt", dir + "/test3.txt" };
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
            var list = new List<string> { dir + "/test1.acm", dir + "/test2.acm", dir + "/test3.acm" };
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

        private static void GenerateRSATestFiles()
        {
            var testDir = ConfigurationManager.AppSettings.Get("test_dir");
            var rsaDir = ConfigurationManager.AppSettings.Get("rsa_dir");
            var txtInp = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_txt_in");
            var txtOut = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_txt_out");
            var rsaInp = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_rsa_in");
            var rsaOut = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_rsa_out");
            var pub = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_pub_key");
            var priv = rsaDir + '/' + ConfigurationManager.AppSettings.Get("RSA_priv_key");
            var pubKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var privKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";


            if (!Directory.Exists(testDir)) Directory.CreateDirectory(testDir);
            if (!Directory.Exists(rsaDir)) Directory.CreateDirectory(rsaDir);
            if (File.Exists(txtInp)) File.Delete(txtInp);
            if (File.Exists(rsaInp)) File.Delete(rsaInp);
            if (File.Exists(pub)) File.Delete(pub);
            if (File.Exists(priv)) File.Delete(priv);

            File.Create(txtInp).Close();
            File.WriteAllText(txtInp, "Prova 1");
            File.Create(rsaInp).Close();
            File.WriteAllText(rsaInp, "2sZQzP1EYkwxZm66llsuIPEzec4G1GzVpWljDuGfSc20INCO/JyfCfX7wvIE3GtuPjFtn0wKKlO0jxgPJrDchA5XPgCRvhJUzdtfWeW5Nclqs6PCID8hC00hYzncSW6+iPUKPrrXGMqs+mO2t08xFaRpXHmxDCaBCR++PLNjfmw=");
            File.Create(pub).Close();
            File.WriteAllText(pub, pubKey);
            File.Create(priv).Close();
            File.WriteAllText(priv, privKey);
        }
    }
}