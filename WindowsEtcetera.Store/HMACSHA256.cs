using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace Megapop
{
    public class HMACSHA256
    {
        private IBuffer key;

        public HMACSHA256(byte[] byteKey)
        {
            key = CryptographicBuffer.CreateFromByteArray(byteKey);
        }

        public byte[] ComputeHash(byte[] byteValue)
        {
            var objMacProv = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
            var hash = objMacProv.CreateHash(key);
            var msg = CryptographicBuffer.CreateFromByteArray(byteValue);
            hash.Append(msg);
            byte[] arrByteNew;
            CryptographicBuffer.CopyToByteArray(hash.GetValueAndReset(), out arrByteNew);
            return arrByteNew;
        }
    }
}

