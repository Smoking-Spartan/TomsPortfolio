using System;

namespace GuidEncodeDecodeTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("GUID Encode/Decode Tool");
            Console.WriteLine("1. Encode GUID to Base64");
            Console.WriteLine("2. Decode Base64 to GUID");
            Console.Write("Choose option (1 or 2): ");
            var option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("Enter GUID (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx): ");
                var guidInput = Console.ReadLine();
                if (Guid.TryParse(guidInput, out Guid guid))
                {
                    string base64 = EncodeGuidToBase64(guid);
                    Console.WriteLine($"Base64 (URL-safe): {base64}");
                }
                else
                {
                    Console.WriteLine("Invalid GUID format.");
                }
            }
            else if (option == "2")
            {
                Console.Write("Enter Base64 string: ");
                var base64Input = Console.ReadLine();
                try
                {
                    Guid guid = DecodeBase64ToGuid(base64Input);
                    Console.WriteLine($"Decoded GUID: {guid}");
                }
                catch
                {
                    Console.WriteLine("Invalid Base64 string for GUID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        // Copy your encoding logic here
        static string EncodeGuidToBase64(Guid guid)
        {
            byte[] guidBytes = guid.ToByteArray();
            string base64 = Convert.ToBase64String(guidBytes)
                .Replace("/", "_")
                .Replace("+", "-")
                .TrimEnd('=');
            return base64;
        }

        // Copy your decoding logic here
        static Guid DecodeBase64ToGuid(string base64)
        {
            base64 = base64.Replace("_", "/").Replace("-", "+");
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            byte[] guidBytes = Convert.FromBase64String(base64);
            return new Guid(guidBytes);
        }
    }
}
