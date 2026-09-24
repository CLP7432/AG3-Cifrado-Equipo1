using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.ComponentModel;

namespace AlgoritmoCifradoAsimetrico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UnicodeEncoding byteConverter = new UnicodeEncoding();

                //Variable menor a 50 caracteres
                string mensajeOriginal = "UNIDEH Hernandez Trinidad Carrasco Flores";

                byte[] dataToEncrypt = byteConverter.GetBytes(mensajeOriginal);
                byte[] encryptedData;
                byte[] decryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
                    decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);
                }
                Console.WriteLine("Mensaje original:        {0}\n", mensajeOriginal);
                Console.WriteLine("Mensaje encriptado:      {0}\n", BitConverter.ToString(encryptedData));
                

                Console.WriteLine("Mensaje desencriptado:    {0}\n", byteConverter.GetString(decryptedData));
                Console.ReadKey();


            }
            catch (ArgumentNullException ex)
            {

            }
        }

        public static byte[] RSAEncrypt(byte[] dataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    encryptedData = RSA.Encrypt(dataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException ex)
            {
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    return null;
                }
            }
        }

        public static byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);

                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return null;
            }
        }
    }
}
