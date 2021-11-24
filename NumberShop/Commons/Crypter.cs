using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptSharp;

namespace NumberShop.Commons
{
    public static class Crypter
    {
        public static string Crypt(string origin)
        {
            string salt = BlowfishCrypter.Blowfish.GenerateSalt();
            byte[] keys = Encoding.UTF8.GetBytes(origin);
            return BlowfishCrypter.Blowfish.Crypt(keys, salt);
        }

        public static bool Verify(string origin, string crypted)
        {
            byte[] keys = Encoding.UTF8.GetBytes(origin);
            string crypted2 = CryptSharp.Crypter.Blowfish.Crypt(keys, crypted);
            return BlowfishCrypter.SafeEquals(crypted, crypted2);
        }
    }
}
