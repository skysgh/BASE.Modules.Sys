using System;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for string manipulation.
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// Easier way to see if a string contains a given string 
        /// in a case insensitive manner.
        /// </summary>
        /// <param name="text">The text to search in.</param>
        /// <param name="value">The value to find.</param>
        /// <param name="stringComparison">The comparison type.</param>
        /// <returns>True if found.</returns>
        public static bool Contains(this string text, string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            if (text == null || value == null)
            {
                return false;
            }
            return text.Contains(value, stringComparison);
        }

        /// <summary>
        /// Given a word for a single word, pluralise it (naively).
        /// Cat => Cats, Story => Stories, Hats => Hats, etc. 
        /// </summary>
        /// <param name="singleWord">The word to pluralize.</param>
        /// <returns>The pluralized word.</returns>
        public static string SimplePluralise(this string singleWord)
        {
            if (singleWord.EndsWith('y'))
            {
                singleWord = string.Concat(singleWord.AsSpan(0, singleWord.Length - 1), "ies");
            }
            else if (!singleWord.EndsWith('s'))
            {
                singleWord += "s";
            }
            return singleWord;
        }
    }
}


