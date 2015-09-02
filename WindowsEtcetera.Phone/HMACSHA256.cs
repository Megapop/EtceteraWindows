namespace Megapop
{
    public class HMACSHA256
    {
        private System.Security.Cryptography.HMACSHA256 hma;

        public HMACSHA256(byte[] byteKey)
        {
            hma = new System.Security.Cryptography.HMACSHA256(byteKey);
        }

        public byte[] ComputeHash(byte[] byteValue)
        {
            return hma.ComputeHash(byteValue);
        }
    }
}

