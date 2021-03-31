using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {

            int p = 11;
            int q = 13;
            int n = p * q; // šifravimui ir dešifravimui
            // e ir d turi būti apskaičiuojami metode
            int e = E(p, q);
            int d = 103; // dešifravimui
            string message = "qwertyuiop[]asdfghjkl;'\\zxcvbnm,./1234567890-=ąčęėįšųūž`~!@#$%^&*()_+"; // šifruojamas tekstas

            string encryptedMessage = RSAEncrypt(message, n, e);

            string decryptedMessage = RSADecrypt(encryptedMessage, n, d);

            Console.WriteLine("Original message: " + message);
            Console.WriteLine("Encrypted message: " + encryptedMessage);
            Console.WriteLine("Decrypted message: " + decryptedMessage);
        }

        public static string RSAEncrypt(string dataToEncrypt, int n, int e)
        {
            if(n * e < 127)
            {
                // Set default values
            }

            // Define integers
            string encryptedData = "";

            // Encryption process
            for(int i = 0; i < dataToEncrypt.Length; i++)
            {
                BigInteger x = Pow(dataToEncrypt[i], e) % n;
                //Console.Write(x + " "); // Testing values

                encryptedData += (char)(x); // Converting to char makes ASCII values to symbols
            }
            //Console.WriteLine(); // Testing values

            return encryptedData;
        }

        public static string RSADecrypt(string dataToDecrypt, int n, int d)
        {
            if (n * d < 127)
            {
                // Set default values
            }

            // Define integers
            string decryptedData = null;

            // Encryption process
            for (int i = 0; i < dataToDecrypt.Length; i++)
            {
                BigInteger x = Pow(dataToDecrypt[i], d) % n;
                //Console.Write(x + " "); // Testing values

                decryptedData += (char)(x);
            }
            //Console.WriteLine(); // Testing values

            return decryptedData;
        }

        public static BigInteger Pow(int a, int b)
        {
            BigInteger c = 1;
            for(int i = 0; i < b; i++)
            {
                c = c * a;
            }
            return c;
        }

        public static int E(int p, int q)
        {
            int e = 0;
            int phi;
            phi = (p - 1) * (q - 1);
            for (int i = 0; i < phi - 2; i++)
            {
                e = i + 2;
                if(GCD(phi, e) == 1)
                {
                    return e;
                }
            }
            return e;
        }

        public static int GCD(int x, int y)
        {
            if(x < y)
            {
                int tempX = x;
                x = y;
                y = tempX;
            }

            int divisor = 0;

            while (divisor != 1)
            {
                divisor = x % y;
                if (divisor == 0)
                {
                    divisor = y;
                    break;
                }
                x = y;
                y = divisor;
            }

            return divisor;
        }
    }
}
