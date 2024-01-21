using System;
using System.Linq;

public static class StringExtension
{
    public static string Capitalize(this string s)
    {
        return char.ToUpper(s[0]) + s.Substring(1);
    }
    
    public static string ToCamelCase(this string s)
    {
        return char.ToLower(s[0]) + s.Substring(1);
    }
    
    public static string AddSpaceBeforeCapitalLetters(this string s)
    {
        return string.Concat(s.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }
}