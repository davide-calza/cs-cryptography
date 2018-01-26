using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CryptoLib;

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

                //command
                var com = args[0].ToUpper(); 
                if (com != "ENC" && com != "DEC")
                    throw new Exception("Unknown command");

                //XML file containing key
                var keyFile = args[1];

                //input file
                var inp = args[2];

                //output file
                var outp = args[3];
                
                switch(com)
                {
                    case "ENC": Console.WriteLine(ClassRsa.Encrypt(inp, outp, keyFile)); break;
                    case "DEC": Console.WriteLine(ClassRsa.Decrypt(inp, outp, keyFile)); break;
                    default: throw new Exception("Unknown command"); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Help();
            }		
        }        

        /// <summary>
        /// Print help summary
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
