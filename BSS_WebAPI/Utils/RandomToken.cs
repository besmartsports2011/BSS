using System;
using System.Text;

namespace BSS_WebAPI.Utils
{
    public static class RandomToken
    {
        public static string GenerateAlphaNumeric(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                result.Append(valid[random.Next(valid.Length)]);
            }
            return result.ToString();
        }

        public static string GenerateDigitsOnly(int length)
        {
            const string valid = "1234567890";
            StringBuilder result = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                result.Append(valid[random.Next(valid.Length)]);
            }
            return result.ToString();
        }

        public static string GenerateLowerAlphaNumeric(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder result = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                result.Append(valid[random.Next(valid.Length)]);
            }
            return result.ToString();
        }

    }
}