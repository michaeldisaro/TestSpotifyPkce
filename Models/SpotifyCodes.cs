using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TestWebApplication.Models
{
    public class SpotifyCodes
    {

        public const string ClientId = "your-spotify-client-id-here";
        
        public static string CodeVerifier;

        public static string CodeChallenge;

        public static void Init()
        {
            CodeVerifier = GenerateNonce();
            CodeChallenge = GenerateCodeChallenge(CodeVerifier);
        }

        private static string GenerateNonce()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz123456789";
            var random = new Random();
            var nonce = new char[128];
            for (int i = 0; i < nonce.Length; i++)
            {
                nonce[i] = chars[random.Next(chars.Length)];
            }

            return new string(nonce);
        }

        private static string GenerateCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            var b64Hash = Convert.ToBase64String(hash);
            var code = Regex.Replace(b64Hash, "\\+", "-");
            code = Regex.Replace(code, "\\/", "_");
            code = Regex.Replace(code, "=+$", "");
            return code;
        }

    }
}