using System;
using System.Collections.Generic;
using System.Reflection;

namespace FAADataParser.NASR
{
    delegate bool ParserDelegate(string input, out object parsedValue);
    interface INASRDataParser
    {
        bool Validate();
    }
    static class NASRDataParserGeneric<T> where T : INASRDataParser, new()
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
            foreach (var field in fieldList)
            {
                PropertyInfo property = t.GetProperty(field.propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                string fieldSubstring = input.Substring(field.fieldBegin, field.fieldLength).Trim();
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
                    if (!field.parserFunc(fieldSubstring, out object parsedValue))
                    {
                        return false;
                    }
                    property.SetValue(output, parsedValue);
                }
            }
            return output.Validate();
        }
    }
}
