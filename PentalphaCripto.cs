using System.Security.Cryptography;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace BikeMessenger
{
    class PentalphaCripto
    {
        public byte[] LvrCalculoMD5(string pValorAconvertir)
        {
            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            sSourceData = pValorAconvertir;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return tmpHash;
        }

        public byte[] LvrCalculoSHA256(string pValorAconvertir)
        {
            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            sSourceData = pValorAconvertir;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            tmpHash = new SHA256Managed().ComputeHash(tmpSource);
            return tmpHash;
        }

        public byte[] LvrCalculoSHA512(string pValorAconvertir)
        {
            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            sSourceData = pValorAconvertir;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            tmpHash = new SHA512Managed().ComputeHash(tmpSource);
            return tmpHash;
        }

        public string LvrByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public string LvrRegionGeografica()
        {
            var geographicRegion = new Windows.Globalization.GeographicRegion();
            var code = geographicRegion.CodeTwoLetter;
            return code;
        }

        public string LvrGenRandomData(uint length)
        {
            // Define the length, in bytes, of the buffer.

            // Generate random data and copy it to a buffer.
            IBuffer buffer = CryptographicBuffer.GenerateRandom(length);

            // Encode the buffer to a hexadecimal string (for display).
            string randomHex = CryptographicBuffer.EncodeToHexString(buffer);

            return randomHex;
        }
    }
}
