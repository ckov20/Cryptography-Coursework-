using System;
using System.IO;
using System.Collections;
using System.Linq;

    class Steganography
    {

       public static void Main(string[] args)
        {
            byte[] bmpBytes = new byte[] { 
            0x42,0x4D,0x4C,0x00,0x00,0x00,0x00,0x00, 
            0x00,0x00,0x1A,0x00,0x00,0x00,0x0C,0x00, 
            0x00,0x00,0x04,0x00,0x04,0x00,0x01,0x00, 
            0x18,0x00,0x00,0x00,0xFF,0xFF,0xFF,0xFF, 
            0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF,0xFF, 
            0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00, 
            0x00,0x00,0xFF,0x00,0x00,0xFF,0xFF,0xFF, 
            0xFF,0x00,0x00,0xFF,0xFF,0xFF,0xFF,0xFF, 
            0xFF,0x00,0x00,0x00,0xFF,0xFF,0xFF,0x00, 
            0x00,0x00 
            }; 
            //Console.WriteLine(BitConverter.ToString(bmpBytes).Replace("-", " "));

        byte[] subArray = new byte[bmpBytes.Length - 26];
        Array.Copy(bmpBytes, 26, subArray, 0, subArray.Length);
        //Console.WriteLine(string.Join(" ", Array.ConvertAll(subArray, b => b.ToString("X2"))));
    
       // Split input into array
        string[] hexValues = args[0].Split(' ');
        foreach (string i in hexValues)
        {
            //Console.Write(i);
        }
        
        // Convert array of hex strings into hex bytes 
        byte[] byteValues = new byte[hexValues.Length];
        for (int i = 0; i < hexValues.Length; i++)
        {
            byte temp = Convert.ToByte(hexValues[i], 16);
            byteValues[i] = temp;
            //Console.Write(byteValues[i]);
        }

        // Convert to binary string 
        string[] binaryStringValues = new string[byteValues.Length];
        for (int j = 0; j < byteValues.Length; j++)
        {
            string temp = Convert.ToString(byteValues[j], 2).PadLeft(8, '0');
            binaryStringValues[j] = temp;
            //Console.Write(binaryStringValues[j]);
        }

        // Split up bytes in 2-bit increments, add to a new array to be XOR'd with the bitmap bytes 
        string[] binaryTwoBits = new string[4*binaryStringValues.Length];
        for (int k = 0; k < binaryStringValues.Length; k++)
        {
            string[] tempArray = new string[4];
            for (int l = 0; l < 8; l += 2  )
                {
                    tempArray[l / 2] = binaryStringValues[k].Substring(l, 2);
                    //Console.Write(tempArray[0]);
                    binaryTwoBits[4*k] = tempArray[0];
                    binaryTwoBits[4*k + 1] = tempArray[1];
                    binaryTwoBits[4*k + 2] = tempArray[2];
                    binaryTwoBits[4*k + 3] = tempArray[3];
                }
            
        }

        // Convert the two-bit strings into bytes 
        byte[] binaryTwoBitsBytes = new byte[binaryTwoBits.Length];
        for (int m = 0; m < binaryTwoBits.Length; m++)
        {
            byte temp = Convert.ToByte(binaryTwoBits[m], 2);
            binaryTwoBitsBytes[m] = temp;
            //Console.Write(binaryTwoBitsBytes[m]);
        }

    


        //Console.Write(binaryTwoBitsBytes[0]);
        
        //XOR the two-bits with the bitmap bytes 
        byte [] hiddenBits = new byte[binaryTwoBitsBytes.Length];
        for (int n = 0; n < binaryTwoBitsBytes.Length; n++ )
        {
            byte temp = (byte)(subArray[n]^binaryTwoBitsBytes[n]);
            hiddenBits[n] = temp;
        }

       
         //Console.Write(hiddenBits[8]);

        foreach (byte i in hiddenBits)
        {
            //Console.Write(i);
            //Console.Write(" ");
        }

        int testing = 0;
        testing = subArray[2]^binaryTwoBitsBytes[2];
        //Console.Write(testing);

        //Console.WriteLine(BitConverter.ToString(hiddenBits).Replace("-", " "));

        byte[] output = new byte[26];
        for (int i = 0; i < 26; i++)
        {
            output[i] = bmpBytes[i];
        }
        
        //Console.WriteLine(BitConverter.ToString(output).Replace("-", " "));

        byte[] result = output.Concat(hiddenBits).ToArray();

        Console.WriteLine(BitConverter.ToString(result).Replace("-", " "));

        //BitConverter.ToString(result).Replace("-", " ");

        }
    }