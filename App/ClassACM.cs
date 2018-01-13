using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FilesEnDecrypter
{
    static class ClassACM
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
            var logs = new List<string>();
            
            try
            {
                if (!Directory.Exists(outDir))
                    throw new Exception("Directory not found");
                if (key == null | key == "")
                    throw new Exception("Null key");
            }
            catch (Exception e)
            {
                logs.Add("Exception on encryption: " + e);
                return logs;
            }

            logs.AddRange(files.Select(f => EncryptFile(f, outDir, key)));

            return logs;
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
            var logs = new List<string>();
            
            try
            {
                if (!Directory.Exists(outDir))
                    throw new Exception("Directory not found");
                if (key == null | key == "")
                    throw new Exception("Null key");
            }
            catch (Exception e)
            {
                logs.Add("Exception on decryption: " + e);
                return logs;
            }

            logs.AddRange(files.Select(f => DecryptFile(f, outDir, key)));

            return logs;
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
                if (key == null | key == "")
                    throw new Exception("Null key");
                
                var fInp = Path.GetFileName(fileName); //input unencrypted file
                var fOut = Path.ChangeExtension(outDir + "/" + fInp, "acm"); //output encrypted file
                
                var sFile = File.ReadAllText(fileName); //text of the file
                
                if (sFile == "")
                    throw new Exception("Empty file");
                
                var sMd5 = CryptoUtils.HashMD5(sFile); //Md5 of the text
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

                return "File successfully encrypted: " + fOut;
            }

            catch (Exception e)
            {
                return "Exception on encryption: " + e;
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
                if (key == null | key == "")
                    throw new Exception("Null key");
                
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
                var sMd5 = Md5FileString(fOut);

                if (sMd5 != sFile.ElementAt(2).Split('=')[1])
                    throw new Exception("MD5 hash functions not corresponding");

                return "File successfully decrypted: " + fOut;;
            }
            catch (Exception e)
            {
                return "Exception on decryption: " + e;
            }
        }

        /// <summary>
        /// Verify MD5 of the input file
        /// </summary>
        /// <param name="fileName">Path of the file </param>
        /// <returns>MD5 Hash</returns>
        /// <exception cref="Exception"></exception>
        public static string Md5FileString(string fileName)
        {
            try
            {
                if(!File.Exists(fileName))
                    throw  new Exception("File not found");

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
                return "Exception on MD5 verify: " + e;
            }
        }
    }
}

