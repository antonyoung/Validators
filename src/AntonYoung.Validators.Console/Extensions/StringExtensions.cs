namespace AntonYoung.Validators.Console.Extensions
{
    public static class StringExtensions
    {
        public static bool IsHelp(this string value)
        {
            return string.Equals(value, "--help", StringComparison.OrdinalIgnoreCase) 
                || string.Equals(value, "-h", StringComparison.OrdinalIgnoreCase);
        }
    }
}