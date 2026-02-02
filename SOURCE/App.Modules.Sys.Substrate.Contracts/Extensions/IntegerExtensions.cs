using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for integer operations.
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Checks if a specific bit is set in the integer value.
        /// </summary>
        /// <param name="value">The integer value to check.</param>
        /// <param name="bitPosition">The zero-based position of the bit to check (0-31).</param>
        /// <returns>True if the bit at the specified position is set (1), false otherwise.</returns>
        public static bool BitIsSet(this int value, int bitPosition)
        {
            return (value & (1 << bitPosition)) != 0;
        }
    }
}
