// ------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlySequenceExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack.Helpers
{
    using System;
    using System.Buffers;

    /// <summary>
    /// Extension methods for the <see cref="ReadOnlySequenceExtensions"/> class
    /// </summary>
    public static class ReadOnlySequenceExtensions
    {
        /// <summary>
        /// Converts a nullable ReadOnlySequence of nullable byte to a Guid
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static Guid ToGuid(this ReadOnlySequence<byte>? sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence), "the sequence may not be null");
            }

            if (!sequence.HasValue)
            {
                throw new ArgumentException("the sequence does not have a value");
            }

            return new Guid(sequence.Value.ToArray());
        }
    }
}
