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
            PrimaryNumbers(11);
            /*
            int a = 0;
            string v1 = "0";
            while (a == 0)
            {
                Console.WriteLine("Kokį veiksmą norite atlikti?\n1. Šifruoti\n2. Dešifruoti\nĮveskite 1 arba 2:");
                v1 = Console.ReadLine();
                if (v1 == "1")
                    a++;
                else
                    Console.WriteLine("Pradžioj turite užšifruoti, kad galėtumėt dešifruoti.");
            }

            a = 0;
            string x = null;
            while (a == 0)
            {
                Console.WriteLine("Įveskite šifruojamą žinutę: ");
                x = Console.ReadLine();
                a++;
            }



            // pradiniai duomenys:
            int p = 11;
            int q = 13;
            //x = "qwertyuiop[]asdfghjkl;'\\zxcvbnm,./1234567890-=ąčęėįšųūž`~!@#$%^&*()_+"; // pradinis tekstas

            // duomenys šifravimui ir dešifravimui:
            int n = p * q; // šifravimui ir dešifravimui
            int e = E(p, q); // patikrinimui: jei p = 11 ir q = 13, tai e = 7
            int d = D(p, q); // patikrinimui: jei p = 11 ir q = 13, tai d = 107


            string y = RSAEncrypt(x, n, e); // užšifruotas tekstas

            string decryptedMessage = RSADecrypt(y, n, d);

            Console.WriteLine("Original message: " + x);
            Console.WriteLine("Encrypted message: " + y);
            Console.WriteLine("Decrypted message: " + decryptedMessage);
            Console.WriteLine("Encrypted message + ");
            */
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
            int phi = (p - 1) * (q - 1);

            for (int i = 0; i < phi - 2; i++)
            {
                e = i + 2;

                if(GCD(phi, e) == 1) // e yra tinkamas, jei tenkina tokią sąlygą
                    return e;
            }
            return e;
        }

        public static int GCD(int x, int y)
        {
            if(x < y) // daliklis - y
            {
                int tempX = x;
                x = y;
                y = tempX;
            }

            int divisor = y;

            while (x % y > 0)
            {
                divisor = x % y;

                //if (divisor == 0) // dalinasi (vadinasi, jau turim didžiausią daliklį)
                  //  return y; // grąžinam didžiausią daliklį

                x = y;
                y = divisor;
            }
            return divisor; // grąžinam didžiausią daliklį
        }

        public static int EGCD(int a, int b) // DBD Euklido algoritmu
        {
            int d = 0;
            int[] s;
            int[] t;
            int[] r;
            int cycleCount = 2;
            if (a < b) // daliklis - y
            {
                int tempA = a;
                a = b;
                b = tempA;
            }

            int x = a, y = b;
            int divisor;
            for (int i = 0; x % y != 0; i++)
            {
                divisor = x % y;

                x = y;
                y = divisor;
                cycleCount++;
            }

            x = a;
            y = b;
            r = new int[cycleCount];
            s = new int[cycleCount];
            t = new int[cycleCount];
            r[0] = x;
            r[1] = y;
            for (int i = 0; x % y != 0; i++)
            {
                divisor = x % y;

                r[i] = x;
                r[i + 1] = y;
                r[i + 2] = divisor;
                x = y;
                y = divisor;
            }

            for (int i = 2; i < cycleCount; i++)
            {
                s[i] = 1;
                t[i] = ((r[i - 2]) / (r[i - 1])) * -1;
                int si = s[i];
                int ti = t[i];
                if (s[i - 1] != 0 && s[i - 2] == 0) // gūd
                {
                    t[i] = si + ti * t[i - 1];
                    s[i] = ti * s[i - 1];
                }
                if (s[i - 1] != 0 && s[i - 2] != 0) // not gūd
                {
                    s[i] = s[i - 2] + ti * s[i - 1];
                    t[i] = t[i - 2] + ti * t[i - 1];
                }
                d = t[i];
            }
            return d;
        }

        public static int D(int p, int q)
        {
            int phi = (p - 1) * (q - 1);
            int e = E(p, q);
            int d = EGCD(phi, e);
            if (d < 0)
                d = d + phi;
            return d;
        }

        public static void PrimaryNumbers(int x)
        {
            for(int i = 2; i < x; i++)
            {
                if(x%i==0)
                {
                    Console.WriteLine(x + " is not a primary number");
                    break;
                }
            }
            Console.WriteLine(x + " is a primary number");
        }
    }
}
