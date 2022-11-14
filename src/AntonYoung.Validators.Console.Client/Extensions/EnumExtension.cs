namespace AntonYoung.Validators.Console.Client.Extensions
{
    internal static class EnumExtension
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
