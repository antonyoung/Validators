namespace AntonYoung.Validators.Console.Extensions
{
    internal static class StringExtensions
    {
        internal static bool IsHelp(this string value)
        {
            return string.Equals(value, "--help", StringComparison.OrdinalIgnoreCase) 
                || string.Equals(value, "-h", StringComparison.OrdinalIgnoreCase);
        }
    }
}