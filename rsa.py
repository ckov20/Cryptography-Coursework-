using System;
using System.Numerics;


    class RSA
    {

         public static BigInteger extendedEuclidean(BigInteger a, BigInteger b, ref BigInteger x, ref BigInteger y)
            {
                if (b == 0)
                {
                    x = 1;
                    y = 0;
                    return a; // GCD is a
                }
                BigInteger x1 = 0, y1 = 0;
                BigInteger gcd = extendedEuclidean(b, a % b, ref x1, ref y1);
                x = y1;
                y = x1 - (a / b) * y1;
                return gcd;
            }

       public static void Main(string[] args)
        {

            //Args[] contains string inputs 
            string p_e_str = args[0];
            string p_c_str = args[1];
            string q_e_str = args[2];
            string q_c_str = args[3];
            string cipherText_str = args[4];
            string plainText_str = args[5];

            int p_e = int.Parse(p_e_str);
            BigInteger p_c = BigInteger.Parse(p_c_str);
            int q_e = int.Parse(q_e_str);
            BigInteger q_c = BigInteger.Parse(q_c_str);
            
            //Find P
            BigInteger p_e_2= BigInteger.Pow(2,p_e);
            BigInteger p = p_e_2 - p_c;
            //Console.Write(p);

            //Find Q
            BigInteger q_e_2= BigInteger.Pow(2,q_e);
            BigInteger q = q_e_2 - q_c;
            //Console.Write(q);

            BigInteger e = 65537;

            BigInteger phi = (p-1)*(q-1);
            BigInteger n = p*q;
            //Console.Write(phi);

            //d×e≡1 (mod ϕ(n))
            //need to find 65537*x+phi*y=1
           

            BigInteger x = 0, y = 0;
            BigInteger gcd = ExtendedGCD(e, phi, ref x, ref y);
            BigInteger d = (x % phi + phi) % phi;
            //Console.Write(d);
            //Console.Write(n);

            //ModPow(value,exponent,mod) -> (cipher, d, n)
            BigInteger cipherText = BigInteger.Parse(cipherText_str);
            BigInteger decrypt = BigInteger.ModPow(cipherText,d,n);
            //Console.Write(decrypt);

            BigInteger plainText = BigInteger.Parse(plainText_str);
            BigInteger encrypt = BigInteger.ModPow(plainText, e,n);
            //Console.Write(encrypt);

            Console.Write(decrypt+","+encrypt);

           //Write your code here and do not change the class name.
        }
    }