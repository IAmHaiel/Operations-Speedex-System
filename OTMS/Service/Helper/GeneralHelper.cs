namespace OTMS.Service.Helper
{
    public static class GeneralHelper
    {
        public static string ContactNumberFormatter(string contactNumber)
        {
            if (string.IsNullOrEmpty(contactNumber))
            {
                return contactNumber;
            }

            // Ensuring only digits
            contactNumber = new string(contactNumber.Where(char.IsDigit).ToArray());

            // Validate the length
            if (contactNumber.Length != 11 || !contactNumber.StartsWith("09"))
            {
                throw new Exception("Contact Number must be exactly 11 digits and start with 09.");
            }

            // Philippines Contact Number Format: 09XX XXX XXXX
            return $"{contactNumber[..4]} {contactNumber.Substring(4, 3)} {contactNumber.Substring(7, 4)}";
        }
    }
}
