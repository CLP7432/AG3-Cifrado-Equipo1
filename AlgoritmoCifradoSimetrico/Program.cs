using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace AlgoritmoCifradoSimetrico
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string mensajeOriginal = "UNIDEH Hernandez Trinidad Carrasco Flores";
            using (Aes myAes = Aes.Create())
            {
                byte[] mesajeEncryptado = EncriptarStringToBytes_Aes(mensajeOriginal, myAes.Key, myAes.IV);
                Console.WriteLine("Mensaje original:          {0}\n", mensajeOriginal);
                Console.WriteLine("Mensaje Encriptado:        {0}\n", BitConverter.ToString(mesajeEncryptado));

                string mensajeDesencriptado = DesencriptarStringFromBytes_AES(mesajeEncryptado, myAes.Key, myAes.IV);

                Console.WriteLine("Mensaje Desencriptado:      {0}\n", mensajeDesencriptado);

                Console.ReadKey();
            }
        }

        static byte[] EncriptarStringToBytes_Aes(string textoOriginal, byte[] key, byte[] IV)
        {
            //Definicion de argumentos del algoritmo AES
            if(textoOriginal == null || textoOriginal.Length <= 0)
            {
                throw new ArgumentNullException("Textooriginal");
            }
            if(key  == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if(IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("IV");
            }
            byte[] mensajeEncriptado = null;

            using (Aes aesAlgoritmo = Aes.Create())
            {
                aesAlgoritmo.Key = key;
                aesAlgoritmo.IV = IV;

                ICryptoTransform encryptador = aesAlgoritmo.CreateEncryptor(aesAlgoritmo.Key, aesAlgoritmo.IV);

                using (MemoryStream msEcrypt = new MemoryStream())
                {
                    using (CryptoStream cssEncrypt = new CryptoStream(msEcrypt, encryptador, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(cssEncrypt))
                        {
                            swEncrypt.Write(textoOriginal);
                        }
                        mensajeEncriptado = msEcrypt.ToArray();
                    }
                }
            }
            return mensajeEncriptado;
        }

        static string DesencriptarStringFromBytes_AES(byte[] textoEncryiptado, byte[] key, byte[] IV)
        {
            if (textoEncryiptado == null || textoEncryiptado.Length <= 0)
            {
                throw new ArgumentNullException("textoEncryiptado");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentNullException("IV");
            }
            
            string textoDesencriptado = null;

            using (Aes aesAlgiritmo = Aes.Create())
            {
                aesAlgiritmo.Key = key;
                aesAlgiritmo.IV = IV;

                ICryptoTransform desencriptador = aesAlgiritmo.CreateDecryptor(aesAlgiritmo.Key, aesAlgiritmo.IV);

                using (MemoryStream msDecrypt = new MemoryStream(textoEncryiptado))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, desencriptador, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            textoDesencriptado = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return textoDesencriptado;
        }
    }
}
