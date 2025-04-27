using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BMZSApi.Models
{
    public static class PasswordHasher
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new HMACSHA256();

            passwordSalt = hmac.Key; //Titkosításhoz kellő kulcs
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //Titkosított jelszó
        }
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            var hmac = new HMACSHA256(storedSalt); //Beállítjuk az eltárolt kulcsot
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //Titkosítjuk a megadott jelszót
            return computedHash.SequenceEqual(storedHash); //megnézzük, hogy a megadott jelszó titkosított alakja megegyezik-e az eltárolt jelszóval
        }
    }
}