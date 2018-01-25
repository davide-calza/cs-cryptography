﻿using System;
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
                    case "ENC": Console.WriteLine(Encrypt(inp, outp, keyFile)); break;
                    case "DEC": Console.WriteLine(Decrypt(inp, outp, keyFile)); break;
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
        /// Encrypts a file with RSA algorithm
        /// </summary>
        /// <param name="input">input txt file</param>
        /// <param name="output">output rsa file</param>
        /// <param name="keyFile">xml key file</param>
        /// <returns></returns>
        private static string Encrypt(string input, string output, string keyFile)
        {
            try
            {
                if (!File.Exists(input))
                    throw new Exception(input + " not found");
                if (!File.Exists(keyFile))
                    throw new Exception(keyFile + " not found");
                if (!Directory.Exists(Path.GetDirectoryName(output)))
                    throw new Exception("Output directory not found");

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
        private static string Decrypt(string input, string output, string keyFile)
        {
            try
            {
                if (!File.Exists(input))
                    throw new Exception(input + " not found");
                if (!File.Exists(keyFile))
                    throw new Exception(keyFile + " not found");
                if (!Directory.Exists(Path.GetDirectoryName(output)))
                    throw new Exception("Output directory not found");

                return "File successfully encrypted";
            }
            catch (Exception e)
            {
                return "Exception on decryption: " + e.Message;
            }
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
