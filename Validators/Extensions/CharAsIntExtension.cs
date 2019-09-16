using System;
using System.Text;


namespace Validators.Extensions
{

    /// <summary>
    ///     used as extension to convert <see cref="Char[]"/> as an <see cref="int"/>, used for iban value sanity check.
    /// </summary>
    public static class CharAsIntExtension
    {

        /// <summary>
        ///     used as internal business logic for sanity check, based on the found index.
        /// </summary>
        private readonly static string _alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        

        /// <summary>
        ///     used as the values that has to be converted to int for the sanity check.
        /// </summary>
        /// <param name="value">
        ///     used as the <see cref="char[]"/> to be converted as <see cref="int"/> value used for the sanity check.
        /// </param>
        /// <returns>
        ///     <see cref="int"/> value used for the sanity check.
        /// </returns>
        public static int CharAsInt(this char[] value)
        {
            var stringBuilder = new StringBuilder(value.Length * 2);

            foreach (var alphaNumber in value)
                stringBuilder.Append(CharAsInt(alphaNumber));

            if (!int.TryParse(stringBuilder.ToString(), out int result))
                throw new ArgumentException(nameof(value));

            return result;
        }


        // todo: convert as extension

        /// <summary>
        ///     used as to convert a letter to an int.
        /// </summary>
        /// <param name="value">
        ///     used as the char, that has to be converted.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     throws exception <paramref name="value"/> has not been found.
        /// </exception>
        /// <returns>
        ///     <see cref="int"/> value of <paramref name="value"/> to be used for the sanity check.
        /// </returns>
        private static int CharAsInt(this char value)
        {
            var index = _alphabet.IndexOf(value);

            // => value not found!
            if (index == -1)
                throw new ArgumentOutOfRangeException(nameof(value));

            // => where 0 = 0, 1 = 1, ..., 9 = 9, A = 10, B = 11, ..., Z = 35
            return index;
        }
    }
}
