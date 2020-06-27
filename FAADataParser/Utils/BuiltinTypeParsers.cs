using System;

namespace FAADataParser.Utils
{
    internal static class BuiltinTypeParsers
    {
        public static bool ParseString(string input, out object output)
        {
            output = input == "" ? null : input; return true;
        }
        public static bool ParseDate(string input, out object output)
        {
            bool result = DateTime.TryParseExact(input, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date);
            output = result ? (object)date : null;
            return result;
        }
        public static bool ParseInt(string input, out object output)
        {
            bool result = int.TryParse(input, out int value);
            output = result ? (object)value : null;
            return result;
        }
        public static bool ParseDecimal(string input, out object output)
        {
            bool result = decimal.TryParse(input, out decimal value);
            output = result ? (object)value : null;
            return result;
        }
        public static bool ParseBool(string input, out object output)
        {
            output = null;
            switch (input)
            {
                case "Y": output = true; break;
                case "N": output = false; break;
                default: return false;
            }
            return true;
        }
        public static bool ParseDouble(string input, out object output)
        {
            bool result = double.TryParse(input, out double value);
            output = result ? (object)value : null;
            return result;
        }
    }
}
