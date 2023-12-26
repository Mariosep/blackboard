public static class StringExtension
{
    public static string Capitalize(this string s)
    {
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}