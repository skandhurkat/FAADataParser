using System;
using System.Collections.Generic;
using System.Reflection;

namespace FAADataParser.NASR
{
    internal delegate bool ParserDelegate(string input, out object parsedValue);

    internal interface INASRDataParser
    {
        bool Validate();
    }

    internal static class NASRDataParserGeneric<T> where T : INASRDataParser, new()
    {
        public static bool TryParse(
            string input,
            int expectedInputLength,
            string prefixString,
            List<(int fieldBegin, int fieldLength, ParserDelegate parserFunc, string propertyName, bool nullable)> fieldList,
            out T output
            )
        {
            output = new T();
            Type t = output.GetType();
            if (input.Length != expectedInputLength)
            {
                return false;
            }
            int lengthOfPrefixString = prefixString.Length;
            if (input.Substring(0, lengthOfPrefixString) != prefixString)
            {
                return false;
            }
            foreach ((int fieldBegin, int fieldLength, ParserDelegate parserFunc, string propertyName, bool nullable) field in fieldList)
            {
                PropertyInfo property = t.GetProperty(field.propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                string fieldSubstring = input.Substring(field.fieldBegin, field.fieldLength).Trim();
                if (!field.parserFunc(fieldSubstring, out object parsedValue))
                {
                    if (fieldSubstring.Length == 0)
                    {
                        if (field.nullable)
                        {
                            property.SetValue(output, null);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    property.SetValue(output, parsedValue);
                }
            }
            return output.Validate();
        }
    }
}
