using System;
using System.Security.Cryptography;
using System.Numerics;
using System.Text;

    class DiffieHellman
    {

        static string decrypt(byte[] cipher,byte[] key,byte[] IV)
        {
            string plain = null;
            using(AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, IV);
                {
                    using(MemoryStream ms = new MemoryStream(cipher))
                    {
                        using(CryptoStream cs = new CryptoStream(ms,decryptor,CryptoStreamMode.Read))
                        {
                            using(StreamReader reader = new StreamReader(cs))
                            {
                                plain = reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            return plain;
        }


        static byte[] encrypt(String plain, byte[] key, byte[] IV)
        {
            byte [] encrypted_Text;
            using(AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, IV);
                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plain);
                        encrypted_Text = ms.ToArray();
                        
                    }
                }
            }
            return encrypted_Text;

        }


       public static void Main(string[] args)
        {
        //Define inputs from args
        string bit_128_IV_hex = args[0];
        string g_e_base_10 = args[1];
        string g_c_base_10 = args[2];
        string N_e_base_10 = args[3];
        string N_c_base_10 = args[4];
        string x_base_10 = args[5];
        string gymodn_base_10 = args[6];
        string cipher_message_c = args[7];
        //Console.Write(cipher_message_c);
        string plaintext_p = args[8];

        //Format inputs
        int X = Int32.Parse(x_base_10);
        string[] bit_128_IV_array = bit_128_IV_hex.Split(' ');
        string[] cipher_message_c_array = cipher_message_c.Split(' ');
        //Console.Write(cipher_message_c_array);
        //Console.Write(' ');
        //Large Number inputs
        BigInteger bi_gymodn_base_10 = BigInteger.Parse(gymodn_base_10);
        int bi_N_e_base_10 = Int32.Parse(N_e_base_10);
        BigInteger bi_N_c_base_10 = BigInteger.Parse(N_c_base_10);
        BigInteger bi_2_N = BigInteger.Pow(2,bi_N_e_base_10);
        BigInteger bi_N = bi_2_N - bi_N_c_base_10;
        //Console.Write(bi_N);
        
        //mod pow: ModPow(BigInteger value, BigInteger exponent, BigInteger modulus)
        BigInteger key = BigInteger.ModPow(bi_gymodn_base_10,X,bi_N); // Key - gxymodN//
        byte[] key_ByteArray = key.ToByteArray();
        
        //Define List
        List<byte> IVlist = new List<byte>();
        List<byte> cipherlist = new List<byte>();


        foreach (var i in bit_128_IV_array)
        {
            byte b = Convert.ToByte(i,16);
            //Console.Write(b);
            //Console.Write(' ');
            byte[] temp_byte = new byte[] {b};
            IVlist.AddRange(temp_byte);
        }
        foreach (var i in cipher_message_c_array)
        {
            byte b = Convert.ToByte(i,16);
            byte[] temp_byte = new byte[] {b};
            cipherlist.AddRange(temp_byte);
        }
        
        
        byte[] IV_byte_array = IVlist.ToArray();
        byte[] cipher_byte_array = cipherlist.ToArray();

        string plain = decrypt(cipher_byte_array, key_ByteArray, IV_byte_array);
        byte[] encrypted = encrypt(plaintext_p, key_ByteArray, IV_byte_array);
        Console.WriteLine(plain+","+BitConverter.ToString(encrypted).Replace("-"," "));
        
        
        
           //Write your code here and do not change the class name.
        }
    }