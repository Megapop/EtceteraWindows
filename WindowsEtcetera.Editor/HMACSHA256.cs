
namespace Megapop
{
    public class Hmacsha256
    {
        private System.Security.Cryptography.HMACSHA256 hma;

        public Hmacsha256(byte[] byteKey)
        {
            hma = new System.Security.Cryptography.HMACSHA256(byteKey);
        }

        public byte[] ComputeHash(byte[] byteValue)
        {
            return hma.ComputeHash(byteValue);
        }
    }
}

