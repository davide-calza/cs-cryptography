using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CryptoLib
{
    public static class ClassRsa
    {
        /// <summary>
        /// Encrypts a file with RSA algorithm
        /// </summary>
        /// <param name="input">input txt file</param>
        /// <param name="output">output rsa file</param>
        /// <param name="keyFile">xml key file</param>
        /// <returns></returns>
        public static string Encrypt(string input, string output, string keyFile)
        {
            try
            {
                if (!File.Exists(input))
                    throw new Exception(input + " not found");
                if (!File.Exists(keyFile))
                    throw new Exception(keyFile + " not found");
                if (!Directory.Exists(Path.GetDirectoryName(output)))
                    throw new Exception("Output directory not found");
                if (Path.GetExtension(input) != ".txt")
                    throw new Exception("Wrong input file format");
                if (Path.GetExtension(output) != ".rsa")
                    throw new Exception("Wrong output file format");
                if (Path.GetExtension(keyFile) != ".xml")
                    throw new Exception("Wrong key file format");
                if (string.IsNullOrEmpty(File.ReadAllText(input)))
                    throw new Exception("Empty file");
                if (string.IsNullOrEmpty(File.ReadAllText(keyFile)))
                    throw new Exception("Empty file");

                var res = CryptoUtils.EncryptRSA(File.ReadAllText(input).Trim(), File.ReadAllText(keyFile).Trim());
                if (res.Split(' ')[0] == "Exception")
                    throw new Exception(res);
                File.WriteAllText(output, res);

                return "File successfully encrypted";
            }
            catch (Exception e)
            {
                return "Exception on encryption: " + e.Message;
            }
        }

        /// <summary>
        /// Decode a RSA encrypted file
        /// </summary>
        /// <param name="input">input rsa file</param>
        /// <param name="output">output txt file</param>
        /// <param name="keyFile">xml key file</param>
        /// <returns></returns>
        public static string Decrypt(string input, string output, string keyFile)
        {
            try
            {
                if (!File.Exists(input))
                    throw new Exception(input + " not found");
                if (!File.Exists(keyFile))
                    throw new Exception(keyFile + " not found");
                if (!Directory.Exists(Path.GetDirectoryName(output)))
                    throw new Exception("Output directory not found");
                if (Path.GetExtension(input) != ".rsa")
                    throw new Exception("Wrong input file format");
                if (Path.GetExtension(output) != ".txt")
                    throw new Exception("Wrong output file format");
                if (Path.GetExtension(keyFile) != ".xml")
                    throw new Exception("Wrong key file format");
                if (string.IsNullOrEmpty(File.ReadAllText(input)))
                    throw new Exception("Empty file");
                if (string.IsNullOrEmpty(File.ReadAllText(keyFile)))
                    throw new Exception("Empty file");

                var res = CryptoUtils.DecryptRSA(File.ReadAllText(input).Trim(), File.ReadAllText(keyFile).Trim());
                if (res.Split(' ')[0] == "Exception")
                    throw new Exception(res);
                File.WriteAllText(output, res);

                return "File successfully decrypted";
            }
            catch (Exception e)
            {
                return "Exception on decryption: " + e.Message;
            }
        }
    }
}
