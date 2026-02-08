// -------------------------------------------------------------------------------------------------
// <copyright file="MessagePackFormatterBase.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;
    using System.Buffers;

    using global::MessagePack;
    
    using SysML2.NET.Extensions.Comparers;

    /// <summary>
    /// Provides shared low-level helper functionality for MessagePack formatters,
    /// including optimized binary encoding and decoding of common value types.
    /// </summary>
    /// <remarks>
    /// This base class exists to centralize performance-critical serialization logic
    /// that is reused across multiple MessagePack formatters.
    /// </remarks>
    public abstract class MessagePackFormatterBase
    {
        /// <summary>
        /// Thread-local reusable buffer for serializing <see cref="Guid"/> values
        /// as fixed-size 16-byte MessagePack binary values.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The buffer is allocated once per thread and reused to avoid per-call allocations
        /// and <see cref="ArrayPool{T}"/> overhead.
        /// </para>
        /// <para>
        /// The buffer length is always exactly 16 bytes, making it safe to pass directly
        /// to <see cref="MessagePackWriter.WriteRaw(ReadOnlySpan{byte})"/> after writing
        /// a <c>bin(16)</c> header.
        /// </para>
        /// </remarks>
        [ThreadStatic]
        private static byte[] guidBuffer;

        /// <summary>
        /// Shared <see cref="GuidComparer"/> instance used to provide deterministic
        /// ordering of <see cref="Guid"/> values during serialization.
        /// </summary>
        /// <remarks>
        /// This comparer is stateless and thread-safe and is shared across all
        /// formatter instances to avoid repeated allocations.
        /// </remarks>
        protected static readonly GuidComparer GuidComparer = new();

        /// <summary>
        /// Writes a <see cref="Guid"/> value as a MessagePack <c>bin(16)</c> field.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> used to write the binary representation.
        /// </param>
        /// <param name="value">
        /// The <see cref="Guid"/> value to serialize.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method encodes the <see cref="Guid"/> as a fixed-length 16-byte binary value
        /// instead of using <see cref="Guid.ToByteArray"/>, avoiding per-call allocations.
        /// </para>
        /// <para>
        /// A thread-local reusable buffer is used to maximize throughput while remaining
        /// safe with respect to <see cref="MessagePackWriter"/> span lifetime rules.
        /// </para>
        /// <para>
        /// This encoding is compatible with MessagePack v3.1.4, which does not provide
        /// built-in <see cref="Guid"/> serialization helpers.
        /// </para>
        /// </remarks>
        protected static void WriteGuidBin16(ref MessagePackWriter writer, Guid value)
        {
            var buffer = guidBuffer ??= new byte[16];
            value.TryWriteBytes(buffer);

            writer.WriteBinHeader(16);
            writer.WriteRaw(buffer); // safe because buffer is exactly 16 bytes
        }

        /// <summary>
        /// Reads a <see cref="Guid"/> value encoded as a MessagePack <c>bin(16)</c> field.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> from which to read the binary value.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="Guid"/> value.
        /// </returns>
        /// <exception cref="MessagePackSerializationException">
        /// Thrown when the value is <c>nil</c> or when the binary payload length is not exactly 16 bytes.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method supports both single-segment and multi-segment
        /// <see cref="ReadOnlySequence{T}"/> inputs without allocating intermediate arrays.
        /// </para>
        /// <para>
        /// For multi-segment sequences, a stack-allocated temporary buffer is used to
        /// assemble the 16-byte payload before constructing the <see cref="Guid"/>.
        /// </para>
        /// <para>
        /// This method assumes the value was written using <see cref="WriteGuidBin16"/>.
        /// </para>
        /// </remarks>
        protected static Guid ReadGuidBin16(ref MessagePackReader reader)
        {
            var bytes = reader.ReadBytes();
            if (!bytes.HasValue)
            {
                throw new MessagePackSerializationException("Expected Guid as bin(16), got nil.");
            }

            var seq = bytes.Value;
            if (seq.Length != 16)
            {
                throw new MessagePackSerializationException($"Expected Guid as 16 bytes, got {seq.Length} bytes.");
            }

            if (seq.IsSingleSegment)
            {
                return new Guid(seq.FirstSpan);
            }

            Span<byte> tmp = stackalloc byte[16];
            seq.CopyTo(tmp);
            return new Guid(tmp);
        }
    }
}
