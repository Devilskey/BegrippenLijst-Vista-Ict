using Bcrypt = BCrypt.Net.BCrypt;


namespace Handlers
{
    public class BCryptHandler
    {
        public static string BcrypyBasicEncryption(string password)
        {
            return Bcrypt.EnhancedHashPassword(password, 10);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return Bcrypt.Verify(password, hash, true);
        }
    }
}