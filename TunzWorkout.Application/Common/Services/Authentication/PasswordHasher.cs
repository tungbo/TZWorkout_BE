using ErrorOr;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace TunzWorkout.Application.Common.Services.Authentication
{
    public partial class PasswordHasher : IPasswordHasher
    {
        //private static readonly Regex PasswordRegex = StrongPasswordRegex();
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName algorithmName = HashAlgorithmName.SHA256;
        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithmName ,HashSize);
            return $"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}";
        }

        public bool IsCorrectPassword(string password, string hashedPassword)
        {
            string[] parts = hashedPassword.Split('-');
            byte[] salt = Convert.FromHexString(parts[0]);
            byte[] hash = Convert.FromHexString(parts[1]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithmName, HashSize);

            //return inputHash.SequenceEqual(hash);
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        //[GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
        //private static partial Regex StrongPasswordRegex();
    }
}
