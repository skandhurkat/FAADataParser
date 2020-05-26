﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FAADataParser.Utils
{
    static class BuiltinTypeParsers
    {
        public static bool ParseString(string input, out object output)
        {
            output = input; return true;
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
    }
}
