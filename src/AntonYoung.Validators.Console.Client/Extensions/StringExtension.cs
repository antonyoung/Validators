namespace AntonYoung.Validators.Console.Client.Extensions
{
    internal static class StringExtension
    {
        internal static string FirstCharacter(this string value)
        {
            return value.First().ToString();
        }
    }
}