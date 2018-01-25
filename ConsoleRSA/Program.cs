using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleRSA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Help();
                    return;
                }
                if (args.Length != 4)
                    throw new Exception("Syntax error");

                var com = args[0].ToUpper();
                if (com != "ENC" && com != "DEC")
                    throw new Exception("Unknown command");

                var keyFile = args[1];
                var inp = args[2];
                var outp = args[3];

                switch(com)
                {
                    case "ENC": Console.WriteLine(Encode(inp, outp, keyFile)); break;
                    case "DEC": Console.WriteLine(Decode(inp, outp, keyFile)); break;
                    default: throw new Exception("Unknown command"); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Help();
            }		
        }
        private static string Encode(string input, string output, string keyFile)
        {
            // verifica parametri e cifrare
            return "Encode ... da fare ...";
        }
        private static string Decode(string input, string output, string keyFile)
        {
            // verifica parametri e decifrare
            return "Decode ... da fare ...";
        }
        /// <summary>
        /// Print help summaryS
        /// </summary>
        private static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("************");
            Console.WriteLine("RSA Console");
            Console.WriteLine("************");
            Console.WriteLine("Syntax: RSA.exe [ENC|DEC] <Keyfilename> <inputfilename> <outputfilename>");
            Console.WriteLine("  es: RSA.exe ENC Chiave.pub.xml plaintext.txt plaintext.rsa");
            Console.WriteLine("  es: RSA.exe DEC Chiave.pri.xml plaintext.rsa plaintext.txt");
            Console.WriteLine();
        }
    }
}
