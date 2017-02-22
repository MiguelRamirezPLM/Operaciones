using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace PLMCryptographyComponent
{
    public class CryptographyComponent
    {
        string  pass = "kasam";
        string  salt = "kasam";
        string  hashAlgorithm = "SHA";
        int     passIter = 2;
        string  vector = "%dk9qos$wib3at1!";
        int keysize = 128;
        

        #region Constructors

        public CryptographyComponent() { }

        #endregion

        #region Public Methods

        public string encrypt(string cadena)
        {
            byte[] vectorbytes = Encoding.ASCII.GetBytes(vector);
            byte[] saltbytes = Encoding.ASCII.GetBytes(salt);

            byte[] cadenaBytes = Encoding.UTF8.GetBytes(cadena);

            PasswordDeriveBytes password = new PasswordDeriveBytes(pass, saltbytes, hashAlgorithm, passIter);

            byte[] keybytes = password.GetBytes(keysize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keybytes, vectorbytes);

            MemoryStream memstream = new MemoryStream();
 
            CryptoStream crypstream = new CryptoStream(memstream,encryptor,CryptoStreamMode.Write);

            crypstream.Write(cadenaBytes, 0, cadenaBytes.Length);

            crypstream.FlushFinalBlock();

            byte[] cadenaEncripBytes = memstream.ToArray();

            memstream.Close();

            crypstream.Close();

            string cadenaEncrip = Convert.ToBase64String(cadenaEncripBytes);

            return cadenaEncrip;

        }

        public string decrypt(string cadenaEncrip)
        {
            byte[] vectorbytes = Encoding.ASCII.GetBytes(vector);
            byte[] saltbytes = Encoding.ASCII.GetBytes(salt);

            byte[] cadenaEncripBytes = Convert.FromBase64String(cadenaEncrip);

            PasswordDeriveBytes password = new PasswordDeriveBytes(pass, saltbytes, hashAlgorithm, passIter);

            byte[] keybytes = password.GetBytes(keysize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keybytes, vectorbytes);

            MemoryStream memstream = new MemoryStream(cadenaEncripBytes);

            CryptoStream crypstream = new CryptoStream(memstream, decryptor, CryptoStreamMode.Read);

            byte[] cadenaBytes = new byte[cadenaEncripBytes.Length];

            int decryptedByteCount = crypstream.Read(cadenaBytes, 0, cadenaBytes.Length);

            memstream.Close();

            crypstream.Close();

            string cadena = Encoding.UTF8.GetString(cadenaBytes, 0, decryptedByteCount);

            return cadena;

        }

        public string addHash(string cadena)
        {
            byte[] cadenaBytes = Encoding.ASCII.GetBytes(cadena);

            HashAlgorithm hAlgorithm = new SHA1CryptoServiceProvider();

            byte[] hashBytes = hAlgorithm.ComputeHash(cadenaBytes);

            string hash = Convert.ToBase64String(hashBytes);

            return hash;
            
        }



        #endregion


    }
}
