// -------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlySequenceExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
