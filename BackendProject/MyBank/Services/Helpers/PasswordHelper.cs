using System;
using System.Security.Cryptography;
using System.Text;

namespace Services.Helpers
{
    public static class PasswordHelper
    {
        public static bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                // Set the salt from the stored value
                hmac.Key = storedSalt;

                // Compute the hash of the provided password
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Compare each byte of the computed hash with the stored hash
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != PasswordHash[i])
                        return false;
                }
            }

            return true;
        }

        public static byte[] PasswordHash(string password, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                // Set the salt from the stored value
                hmac.Key = PasswordSalt;

                // Compute the hash of the provided password
                return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static byte[] PasswordSalt(string UserName)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(UserName);

            using (HMACSHA512 hmac = new HMACSHA512())
            {
                return hmac.ComputeHash(inputBytes);
            }

        }
    }
}
