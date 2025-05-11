using System.Text;

namespace server.Services
{
    public class GuidEncoderService : IGuidEncoderService
    {
        public string EncodeGuidToBase64(Guid guid)
        {
            // Convert GUID to byte array
            byte[] guidBytes = guid.ToByteArray();
            // Convert to Base64 and remove padding characters
            string base64 = Convert.ToBase64String(guidBytes)
                .Replace("/", "_")  // URL-safe characters
                .Replace("+", "-")
                .TrimEnd('=');
            return base64;
        }

        public Guid DecodeBase64ToGuid(string base64)
        {
            // Add padding back
            base64 = base64.Replace("_", "/").Replace("-", "+");
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            // Convert back to GUID
            byte[] guidBytes = Convert.FromBase64String(base64);
            return new Guid(guidBytes);
        }
    }
} 