using System;
using App.Modules.Sys.Factories;

namespace App
{
    /// <summary>
    /// Extension methods for <see cref="Guid"/> objects.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Generate a new Guid using the <see cref="UUIDFactory"/>.
        /// <para>
        /// IMPORTANT: Does not change the value of the Guid - returns a new one.
        /// </para>
        /// </summary>
        /// <param name="_">The Guid (ignored, used for extension method syntax).</param>
        /// <returns>A new sequential Guid.</returns>
        public static Guid Generate(this Guid _)
        {
            return UUIDFactory.NewGuid();
        }
    }
}
