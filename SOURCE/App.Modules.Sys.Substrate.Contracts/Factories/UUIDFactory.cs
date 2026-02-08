using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Factories
{



    /// <summary>
    /// A helper class to develop a new Guid
    /// in a time sequential manner.
    /// <para>
    /// It's primary purpose is to develop Db tables
    /// that append new records at the bottom (faster)
    /// rather than randomly throughout a table (slower), 
    /// in doing so decreasing the amount of disorder
    /// </para>
    /// <para>
    /// Also, using a Guid removes the need for an incrementing
    /// Int Id and it's locking required -- so increases performance
    /// significantly.
    /// </para>
    /// <para>
    /// Use of a Guid permits developing of records on many 
    /// devices with an acceptably very low risk of conflict.
    /// </para>
    /// </summary>
    public static partial class UUIDFactory
    {
        /// <summary>
        /// Enumeration of ways to develop a Guid
        /// </summary>
        public enum SequentialGuidType
        {
            /// <summary>
            /// TODO: Describe better
            /// </summary>
            SequentialAsString,
            /// <summary>
            /// TODO: Describe better
            /// </summary>
            SequentialAsBinary,
            /// <summary>
            /// TODO: Describe better
            /// </summary>
            SequentialAtEnd
        }

        private static readonly Random _random = new Random();

        /// <summary>
        /// Static constructor
        /// </summary>
        static UUIDFactory()
        {
        }

        /// <summary>
        /// Develop a new Guid
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            return NewGuid(SequentialGuidType.SequentialAtEnd);
        }

        /// <summary>
        /// Develop a new Guid
        /// </summary>
        /// <param name="guidType"></param>
        /// <returns></returns>
        public static Guid NewGuid(SequentialGuidType guidType)
        {
            var randomBytes = new byte[10];
            _random.NextBytes(randomBytes);

            //private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();
            //_rng.GetBytes(randomBytes);


            var timestamp = DateTime.UtcNow.Ticks / 10000L;
            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to reverse the order
                    // of the Data1 and Data2 blocks on little-endian systems.
                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }
            return new Guid(guidBytes);
        }
    }
}