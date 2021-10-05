using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ConsoleApp1
{
    class Program
    {
        /*
         * Пароль вообще не стоит хранить нигде ни на сервере ни в БД, а в простейшем случае должны хранить лишь Salt и Hash-сумму пароля.
         */
        static void Main(string[] args)
        {
            /*
            int salt = -1525557863;//GenerateSaltForPassword();

            byte[] hash = ComputePasswordHash("testpasswod", salt);
            byte[] hash1 = ComputePasswordHash("testpasswod", salt);
            Console.WriteLine(salt);
            Console.WriteLine(String.Join("", hash));
            Console.WriteLine(String.Join("", hash1));

            string salt2 = "Ju5yFy35Jk";
            byte[] hash7 = ComputePasswordHash("testpasswod", salt2);
            byte[] hash8 = ComputePasswordHash("testpasswod", salt2);
            Console.WriteLine(salt2);
            Console.WriteLine(String.Join("", hash7));
            Console.WriteLine(String.Join("", hash8));
            */


            Console.WriteLine("Microsoft");
            byte[] salt2 = new byte[128 / 8];

            /*{ 122, 35, 50, 2, 42, 31, 75, 11, 238, 112, 151, 72, 174, 84, 63, 188 };
            Salt: eiMyAiofSwvucJdIrlQ/vA==
            Hashed: sfHmMqi8keVLeMhAjKTRYf/xLynCecl5Fo5gNz3wVDM=*/
            using (var rngCsp = new RNGCryptoServiceProvider())  //криптографический генератор случайных чисел
            {
                rngCsp.GetNonZeroBytes(salt2);
            }

            byte[] salt = { 122, 35, 50, 2, 42, 31, 75, 11, 238, 112, 151, 72, 174, 84, 63, 188 };

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(    //алгоритм PBKDF2
            password: "testpassword",
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            string saltToBase64 = Convert.ToBase64String(salt);
            Console.WriteLine($"Salt: {saltToBase64}");
            Console.WriteLine($"Hashed: {hashed}");


            Console.WriteLine("Проверка");
            Console.WriteLine(IsPasswordValid("testpassword", "eiMyAiofSwvucJdIrlQ/vA==", "sfHmMqi8keVLeMhAjKTRYf/xLynCecl5Fo5gNz3wVDM="));


        }

        static bool IsPasswordValid(string password, string salt, string hash)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return hashed.SequenceEqual(hash);
        }

        /*
        // Генератор соли
        public static int GenerateSaltForPassword()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[4];
            rng.GetNonZeroBytes(saltBytes);
            return (((int)saltBytes[0]) << 24) + (((int)saltBytes[1]) << 16) + (((int)saltBytes[2]) << 8) + ((int)saltBytes[3]);
        }

        // хеширование
        private static byte[] ComputePasswordHash(string password, int salt)
        {
            byte[] saltBytes = new byte[4];
            saltBytes[0] = (byte)(salt >> 24);
            saltBytes[1] = (byte)(salt >> 16);
            saltBytes[2] = (byte)(salt >> 8);
            saltBytes[3] = (byte)(salt);

            byte[] passwordBytes = UTF8Encoding.UTF8.GetBytes(password);

            byte[] preHashed = new byte[saltBytes.Length + passwordBytes.Length];
            System.Buffer.BlockCopy(passwordBytes, 0, preHashed, 0, passwordBytes.Length);
            System.Buffer.BlockCopy(saltBytes, 0, preHashed, passwordBytes.Length, saltBytes.Length);

            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(preHashed);
        }

        private static byte[] ComputePasswordHash(string password, string salt)
        {
            byte[] saltBytes = new byte[4];
            saltBytes[0] = (byte)(salt.Length >> 24);
            saltBytes[1] = (byte)(salt.Length >> 16);
            saltBytes[2] = (byte)(salt.Length >> 8);
            saltBytes[3] = (byte)(salt.Length);

            byte[] passwordBytes = UTF8Encoding.UTF8.GetBytes(password);

            byte[] preHashed = new byte[saltBytes.Length + passwordBytes.Length];
            System.Buffer.BlockCopy(passwordBytes, 0, preHashed, 0, passwordBytes.Length);
            System.Buffer.BlockCopy(saltBytes, 0, preHashed, passwordBytes.Length, saltBytes.Length);

            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(preHashed);
        }

        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }


        // проверка хешированного пароля и введенного для авторизации
        private bool IsPasswordValid(string passwordToValidate, int salt, byte[] correctPasswordHash)
        {
            byte[] hashedPassword = ComputePasswordHash(passwordToValidate, salt);

            return hashedPassword.SequenceEqual(correctPasswordHash);
        }
        */
    }
}
