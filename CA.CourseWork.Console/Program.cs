using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CA.CourseWork.Crypto;
using CA.CourseWork.Crypto.Interfaces;

namespace CA.CourseWork.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ////var initial = new byte[] {0x05, 0xe8, 0xbc, 0x26, 0x91, 0xd4, 0x2d, 0xe0, 0x02, 0xf6, 0xf8, 0x44, 0x20, 0x49, 0xe9, 0x3f};
            //var init = "THISISADATATESTA";
            //var bytes = GetBytes(init);
            
            //PrintArray(bytes);
            //var watch = new Stopwatch();
            //IEncryptable encoder = new AESCryptoEncoder();
            //watch.Start();
            //var encoded = encoder.Encode(bytes, "thisismyfirstkey");
            //watch.Stop();
            //PrintArray(encoded);
            //Console.WriteLine(watch.ElapsedMilliseconds + " ms.");

            //IDecryptable decoder = new AESCryptoDecoder();
            //watch.Start();
            //var decoded = decoder.Decode(encoded, "thisismyfirstkey");
            //watch.Stop();
            //PrintArray(decoded);

            //Console.WriteLine(GetString(decoded));
            //Console.WriteLine(watch.ElapsedMilliseconds + " ms.");

            ////IEncryptable des = new DESCryptoEncoder();

            ////var encoded = des.Encode(new byte[] { 0xaa, 0xbb, 0xab, 0x12, 0xff, 0x34, 0xc3, 0xc9 }, new byte[] { 0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe });

            ////IDecryptable decoder = new DESCryptoDecoder();

            ////var decoded = decoder.Decode(encoded, new byte[] {0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe});

            //////IEncryptable gost = new GOSTCryptoEncoder();

            //////var encoded = gost.Encode(new byte[] { 0xaa, 0xbb, 0xab, 0x12, 0xff, 0x34, 0xc3, 0xc9, 0xaa, 0xbb, 0xab, 0x12 }, new byte[] { 0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe, 0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe, 0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe, 0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe });

            //////IDecryptable decoder = new GOSTCryptoDecoder();

            //////var decoded = decoder.Decode(encoded,
            //////    new byte[]
            //////    {
            //////        0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe, 0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe, 0x01,
            //////        0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe, 0x01, 0x01, 0xcc, 0xcd, 0xdc, 0xfc, 0xed, 0xfe
            //////    });

            IEncryptable gost = new GOSTCryptoEncoder();

            var encoded = gost.Encode("ASDADDDDDADA", "qwertyuiqwertyui");

            IDecryptable decoder = new GOSTCryptoDecoder();

            var decoded = decoder.Decode(encoded, "qwertyuiqwertyui");

            Console.ReadKey();
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        private static void PrintMatrix(IEnumerable<byte[]> matrix)
        {
            Console.Write("Matrix: ");
            foreach (var row in matrix)
            {
                Console.Write("\n");
                PrintArray(row);
            }
            Console.Write("\n");
        }

        private static void PrintArray(IEnumerable<byte> array)
        {
            Console.Write("Array: ");
            foreach (var a in array)
            {
                Console.Write(a.ToString() + " ");
            }
            Console.Write("\n");
        }
    }
}
