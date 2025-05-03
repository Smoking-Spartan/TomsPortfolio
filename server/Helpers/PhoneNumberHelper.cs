namespace server.Helpers
{
    public static class PhoneNumberHelper
    {
        public static string Normalize(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return phoneNumber;
            phoneNumber = phoneNumber.Trim();
            if (!phoneNumber.StartsWith("+"))
                phoneNumber = "+" + phoneNumber;
            return phoneNumber;
        }

        public static bool IsValid(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(
                phoneNumber ?? "",
                @"^\+[1-9]\d{1,14}$"
            );
        }
    }
}