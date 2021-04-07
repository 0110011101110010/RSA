using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics; // BigInteger
using System.IO; // File.Create

namespace RSAWindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs eA)
        {
            int p;
            int q;
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox5.Text) || String.IsNullOrWhiteSpace(textBox6.Text))
                MessageBox.Show("Nepalikite tuščių laukų.");
            else if (!Int32.TryParse(textBox5.Text, out p) || !Int32.TryParse(textBox6.Text, out q))
                MessageBox.Show("p ir q turi būti skaičiai");
            else if (!IsPrimaryNumber(p) || !IsPrimaryNumber(q))
                MessageBox.Show("p ir q turi būti pirminiai skaičiai");
            else if (p * q < 127)
                MessageBox.Show("p ir q skaičių sandaugą negali būti mažesnė už 127. Įveskite kitas reikšmes.");
            else
            {
                string x = textBox1.Text;

                int n = p * q;
                int e = E(p, q);


                string y = RSAEncrypt(x, n, e);

                File.WriteAllText("key.txt", y + "\n" + n + "\n" + e); // update

                textBox3.Text = y;
                textBox4.Text = y;

                button2.Enabled = true;
            }

        }

        private void Button2_Click(object sender, EventArgs eA)
        {
            using (StreamReader reader = new StreamReader("key.txt"))
            {
                int p = 0;
                int q = 0;
                string y = reader.ReadLine();

                if (checkBox1.Checked == false)
                    y = textBox4.Text;
                Int32.TryParse(reader.ReadLine(), out int n);
                Int32.TryParse(reader.ReadLine(), out int e); // update

                for (int j = 2; j < 1009; j++)
                {
                    if (IsPrimaryNumber(j))
                    {
                        if ((q = PrimaryNumber(j, n)) != 0)
                        {
                            p = j;
                            q = PrimaryNumber(j, n);
                            break;
                        }
                    }
                }

                int d = D(p, q);
                string decryptedMessage = RSADecrypt(y, n, d);
                textBox2.Text = decryptedMessage;
            }
        }

        public static string RSAEncrypt(string dataToEncrypt, int n, int e)
        {
            string encryptedData = "";

            for (int i = 0; i < dataToEncrypt.Length; i++)
            {
                BigInteger x = Pow(dataToEncrypt[i], e) % n;

                encryptedData += (char)(x); // Converting to char makes ASCII values to symbols
            }

            return encryptedData;
        }

        public static string RSADecrypt(string dataToDecrypt, int n, int d)
        {
            string decryptedData = null;

            for (int i = 0; i < dataToDecrypt.Length; i++)
            {
                BigInteger x = Pow(dataToDecrypt[i], d) % n;

                decryptedData += (char)(x);
            }

            return decryptedData;
        }

        public static BigInteger Pow(int a, int b)
        {
            BigInteger c = 1;

            for (int i = 0; i < b; i++)
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

                if (GCD(phi, e) == 1) // e yra tinkamas, jei tenkina tokią sąlygą
                    return e;
            }

            return e;
        }

        public static int GCD(int x, int y)
        {
            if (x < y) // daliklis - y
            {
                int tempX = x;
                x = y;
                y = tempX;
            }

            int divisor = y;

            while (x % y > 0)
            {
                divisor = x % y;
                x = y;
                y = divisor;
            }

            return divisor;
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

                if (s[i - 1] != 0 && s[i - 2] == 0)
                {
                    t[i] = si + ti * t[i - 1];
                    s[i] = ti * s[i - 1];
                }
                if (s[i - 1] != 0 && s[i - 2] != 0)
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

        public static bool IsPrimaryNumber(int x)
        {
            for (int i = 2; i < x; i++)
            {
                if (x % i == 0)
                    return false;
            }

            return true;
        }

        // j - pirmas pirminis skaičius (p)
        // i - antras pirminis skaičius (q)
        public static int PrimaryNumber(int j, int n)
        {
            if (IsPrimaryNumber(j))
            {
                for (int i = 2; i < 1009; i++)
                {
                    if (IsPrimaryNumber(i))
                        if (j * i == n)
                            return i;
                }
            }

            return 0;
        }
    }
}
