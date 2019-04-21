using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace siberianoilrush_server.Security
{
    public static class AuthSecurity
    {
        public static bool IsPasswordValid(string passwordToValidate, int salt, byte[] correctPasswordHash)
        {
            var hashedPassword = ComputePasswordHash(passwordToValidate, salt);

            return hashedPassword.SequenceEqual(correctPasswordHash);
        }

        public static int GenerateSaltForPassword()
        {
            var rng = new RNGCryptoServiceProvider();
            var saltBytes = new byte[4];
            rng.GetNonZeroBytes(saltBytes);
            return (saltBytes[0] << 24) + (saltBytes[1] << 16) + (saltBytes[2] << 8) + saltBytes[3];
        }

        public static byte[] ComputePasswordHash(string password, int salt)
        {
            var saltBytes = new byte[4];
            saltBytes[0] = (byte)(salt >> 24);
            saltBytes[1] = (byte)(salt >> 16);
            saltBytes[2] = (byte)(salt >> 8);
            saltBytes[3] = (byte)(salt);

            var passwordBytes = UTF8Encoding.UTF8.GetBytes(password);

            var preHashed = new byte[saltBytes.Length + passwordBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, preHashed, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, preHashed, passwordBytes.Length, saltBytes.Length);

            var sha1 = SHA1.Create();
            return sha1.ComputeHash(preHashed);
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            var rnd = new Random();

            while (0 < length--)
                res.Append(valid[rnd.Next(valid.Length)]);

            return res.ToString();
        }
    }
}
