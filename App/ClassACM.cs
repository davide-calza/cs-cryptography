using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FilesEnDecrypter
{
    class ClassACM
    {
        public List<String> EncodeFileList(List<String> files, string outDir, string key)
        {
            return null;
        }
        public List<String> DecodeFileList(List<String> files, string outDir, string key)
        {
            return null;
        }

        /// <summary>
        /// Metodo per cifrare un file in formato ACM (Algoritmo Crittografico Marconi)
        /// </summary>
        /// <param name="fileName">Nome del file es: "c:\lavoro-temp\prova.txt"</param>
        /// <param name="outDir">Directory di output es: "c:\lavoro-temp"</param>
        /// <param name="key">Chiave simmetrica di cifratura</param>
        /// <returns>Log dell'azione o di ventuali errori</returns>
        public string EncodeFile(string fileName, string outDir, string key)
        {
            string f_inp = Path.GetFileName(fileName);
            string f_out = Path.ChangeExtension(outDir + "\\" + f_inp, "acm");

            string sFile = File.ReadAllText(fileName);
            string sMD5 = Crypto_Utils.HashMD5(sFile);
            string sACM = Crypto_Utils.EncryptAES(sFile, key);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("ACM");
            sb.AppendLine("File=" + f_inp);
            sb.AppendLine("Hash=" + sMD5);
            sb.AppendLine("Size=" + sACM.Length);
            sb.AppendLine("Type=AES");
            sb.AppendLine("@@@@@");
            sb.Append(sACM);

            File.WriteAllText(f_out, sb.ToString());

            return "\nFile cifrato correttamente: " + f_out + "\nMd5 = " + sMD5;
        }

        public string DecodeFile(string fileName, string outDir, string key)
        {
            string f_inp = Path.GetFileNameWithoutExtension(fileName);
            string f_out = outDir + "\\" + f_inp + "_dec.txt";

            List<string> sFile = File.ReadLines(fileName).ToList();

            StringBuilder sb = new StringBuilder();

            for (int i = 6; i < sFile.Count; i++)
            {
                string sACM = Crypto_Utils.DecryptAES(sFile.ElementAt(i), key);
                sb.Append(sACM);
            }

            File.WriteAllText(f_out, sb.ToString());

            string sMd5 = Md5FileString(f_out);
            string log = "ERRORE nella decifratura del file: MD5 non corrispondente";

            if (sMd5 == sFile.ElementAt(2).Split('=')[1])
                log = "\nFile decifrato correttamente: " + f_out + "\nMd5 = " + Md5FileString(f_out);

            return log;
        }

        public string Md5FileString(string fileName)
        {
            return Crypto_Utils.HashMD5(File.ReadAllText(fileName));
        }
    }
}

