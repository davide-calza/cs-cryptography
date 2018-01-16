using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cryptography
{
    public static class ClassAcm
    {
        /// <summary>
        /// Encrypt a list of files
        /// </summary>
        /// <param name="files">List of files to encrypt</param>
        /// <param name="outDir">Output directory</param>
        /// <param name="key">Symmetric key</param>
        /// <returns>List of logs</returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<string> EncryptFileList(IEnumerable<string> files, string outDir, string key)
        {
            return files.Select(f => EncryptFile(f, outDir, key));
        }

        /// <summary>
        /// Decrypt a list of files
        /// </summary>
        /// <param name="files">List of files to decrypt</param>
        /// <param name="outDir">Output directory</param>
        /// <param name="key">Symmetric key</param>
        /// <returns>List of logs</returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<string> DecryptFileList(IEnumerable<string> files, string outDir, string key)
        {
            return files.Select(f => DecryptFile(f, outDir, key));
        }

        /// <summary>
        /// Calculate MD5 of a list of .acm files
        /// </summary>
        /// <param name="files">Files to calculate the MD5 of</param>
        /// <param name="key">Symmetric key</param>
        /// <returns>logs</returns>
        public static IEnumerable<string> VerifyAcmMd5List(IEnumerable<string> files, string key)
        {
            var logs = new List<string>();

            try
            { 
                if (string.IsNullOrEmpty(key))
                    throw new Exception("Null key");
            }
            catch (Exception e)
            {
                logs.Add("Exception on MD5 verification: " + e.Message);
                return logs;
            }

            logs = files.Select(f => f + " = " + VerifyMd5AcmFile(f, key)).ToList();

            return logs;
        }

        /// <summary>
        /// Calculate MD5 of a list of .txt files
        /// </summary>
        /// <param name="files">Files to calculate the MD5 of</param>
        /// <returns>logs</returns>
        public static IEnumerable<string> CalculateTxtMd5List(IEnumerable<string> files)
        {
            return files.Select(f => f + " = " + TxtMd5FileString(f)).ToList();
        }

        /// <summary>
        /// Encrypt a file in ACM format (Algoritmo Crittografico Marconi)
        /// </summary>
        /// <param name="fileName">Path of the file to encrypt</param>
        /// <param name="outDir">Output directory</param>
        /// <param name="key">Symmetric key</param>
        /// <returns>Logs</returns>
        /// <exception cref="Exception"></exception>
        private static string EncryptFile(string fileName, string outDir, string key)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception("File not found");
                if (!Directory.Exists(outDir))
                    throw new Exception("Directory not found");
                if (string.IsNullOrEmpty(key))
                    throw new Exception("Null key");
                if (Path.GetExtension(fileName) != ".txt")
                    throw new Exception("Wrong file format. This function can encrypt only .txt files");

                var fInp = Path.GetFileName(fileName); //input unencrypted file
                var fOut = Path.ChangeExtension(outDir + "/" + fInp, "acm"); //output encrypted file

                var sFile = File.ReadAllText(fileName); //text of the file

                if (sFile == "")
                    throw new Exception("Empty file");

                var sMd5 = TxtMd5FileString(fileName); //Md5 of the text
                var sAcm = CryptoUtils.EncryptAES(sFile, key); //encrypted text

                var sb = new StringBuilder();

                //Encrypted file structure
                sb.AppendLine("ACM");
                sb.AppendLine("File=" + fInp);
                sb.AppendLine("Hash=" + sMd5);
                sb.AppendLine("Size=" + sAcm.Length);
                sb.AppendLine("Type=AES");
                sb.AppendLine("@@@@@");
                sb.Append(sAcm);

                //Write on file (fOut)
                File.WriteAllText(fOut, sb.ToString());

                return "File successfully encrypted: " + fOut + "\r\nMD5 = " + sMd5;
            }

            catch (Exception e)
            {
                return "Exception on encryption: " + e.Message;
            }
        }

        /// <summary>
        /// Decrypt a file in ACM format (Algoritmo Crittografico Marconi)
        /// </summary>
        /// <param name="fileName">Path of the file to decrypt</param>
        /// <param name="outDir">Output directory</param>
        /// <param name="key">Symmetric key</param>
        /// <returns>Logs</returns>
        private static string DecryptFile(string fileName, string outDir, string key)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception("File not found");
                if (!Directory.Exists(outDir))
                    throw new Exception("Directory not found");
                if (string.IsNullOrEmpty(key))
                    throw new Exception("Null key");
                if (Path.GetExtension(fileName) != ".acm")
                    throw new Exception("Wrong file format. This function can decrypt only .acm files");

                var fInp = Path.GetFileNameWithoutExtension(fileName); //input encrypted file
                var fOut = outDir + "/" + fInp + "_dec.txt"; //output unencrypted file

                var sFile = File.ReadLines(fileName).ToList(); //encrypted text

                if (sFile.Count == 0)
                    throw new Exception("Empty file");

                var sb = new StringBuilder();

                //Read text
                for (var i = 6; i < sFile.Count; i++)
                {
                    var sAcm = CryptoUtils.DecryptAES(sFile.ElementAt(i), key);
                    sb.Append(sAcm);
                }

                //Write on file
                File.WriteAllText(fOut, sb.ToString());

                //MD5 verify
                var sMd5 = TxtMd5FileString(fOut);

                if (sMd5 != AcmMd5FileString(fileName))
                    throw new Exception("MD5 hash functions not corresponding");

                return "File successfully decrypted: " + fOut;
            }
            catch (Exception e)
            {
                return "Exception on decryption: " + e.Message;
            }
        }

        /// <summary>
        /// Calculate MD5 of the .txt input file
        /// </summary>
        /// <param name="fileName">Path of the file </param>
        /// <returns>MD5 Hash</returns>
        /// <exception cref="Exception"></exception>
        private static string TxtMd5FileString(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception("File not found");
                if (Path.GetExtension(fileName) != ".txt")
                    throw new Exception("Wrong file format. This function can encrypt only .txt files");

                var sFile = File.ReadAllText(fileName); //text

                if (sFile == "")
                    throw new Exception("Empty file");

                var md5 = CryptoUtils.HashMD5(sFile);

                if (md5 == null)
                    throw new Exception("Error on MD5 calculation");

                return md5;
            }
            catch (Exception e)
            {
                return "Exception on MD5 calculation: " + e;
            }
        }

        /// <summary>
        /// Calculate MD5 of the .acm input file
        /// </summary>
        /// <param name="fileName">Path of the file </param>
        /// <returns>MD5 Hash</returns>
        /// <exception cref="Exception"></exception>
        private static string AcmMd5FileString(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception("File not found");
                if (Path.GetExtension(fileName) != ".acm")
                    throw new Exception("Wrong file format. This function can encrypt only .acm files");

                var sFile = File.ReadAllLines(fileName).ToList(); //text

                if (sFile.Count == 0)
                    throw new Exception("Empty file");

                var md5 = sFile.ElementAt(2).Split('=')[1];

                if (md5 == null)
                    throw new Exception("Error on MD5 calculation");

                return md5;
            }
            catch (Exception e)
            {
                return "Exception on MD5 calculation: " + e;
            }
        }

        /// <summary>
        /// Verify MD5 of the .acm input file
        /// </summary>
        /// <param name="fileName">Path of the file </param>
        /// <param name="key">Symmetric key</param>
        /// <returns>MD5 Hash</returns>
        /// <exception cref="Exception"></exception>
        private static string VerifyMd5AcmFile(string fileName, string key)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new Exception("File not found");
                if (Path.GetExtension(fileName) != ".acm")
                    throw new Exception("Wrong file format. This function can encrypt only .acm files");

                var sFile = File.ReadAllLines(fileName).ToList(); //text

                if (sFile.Count == 0)
                    throw new Exception("Empty file");

                var plainMd5 = AcmMd5FileString(fileName);
                var calcMd5 = CryptoUtils.HashMD5(CryptoUtils.DecryptAES(sFile.ElementAt(6), key));

                if (plainMd5 != calcMd5)
                    throw new Exception("Md5 not coincident!");

                return plainMd5 + " successfully verified";
            }
            catch (Exception e)
            {
                return "Exception on MD5 verify: " + e;
            }
        }
    }
}
