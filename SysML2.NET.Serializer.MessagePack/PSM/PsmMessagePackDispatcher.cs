// -------------------------------------------------------------------------------------------------
// <copyright file="PsmMessagePackDispatcher.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.PSM
{
    using System;
    using System.Reflection;

    using SysML2.NET.Common;

    using global::MessagePack;

    /// <summary>
    /// Writes a value whose declared type is a union, which the Systems Modeling API and Services admits for the
    /// Data and Constraint properties
    /// </summary>
    /// <remarks>
    /// The runtime type cannot be inferred from the declared type, so the value is written as a two element array
    /// that pairs the assembly qualified type name with the value itself.
    /// </remarks>
    internal static class PsmMessagePackDispatcher
    {
        /// <summary>
        /// The assembly that declares every Core and Systems Modeling API and Services data transfer object
        /// </summary>
        private static readonly Assembly DataTransferObjectAssembly = typeof(IData).GetTypeInfo().Assembly;

        /// <summary>
        /// Writes the value, preceded by the discriminator that identifies its runtime type
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value
        /// </param>
        /// <param name="value">
        /// The value that is to be serialized, which may be null
        /// </param>
        /// <param name="options">
        /// The serialization settings to use
        /// </param>
        internal static void Write(ref MessagePackWriter writer, object value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();

                return;
            }

            var type = value.GetType();

            writer.WriteArrayHeader(2);
            writer.Write(type.FullName);

            MessagePackSerializer.Serialize(type, ref writer, value, options);
        }

        /// <summary>
        /// Reads a value that was written by <see cref="Write"/>
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> positioned on the discriminated pair
        /// </param>
        /// <param name="options">
        /// The serialization settings to use
        /// </param>
        /// <returns>
        /// The deserialized value, or null when the pair is nil
        /// </returns>
        /// <exception cref="MessagePackSerializationException">
        /// Thrown when the pair is malformed or the discriminator names no known type
        /// </exception>
        internal static object Read(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            var length = reader.ReadArrayHeader();

            if (length != 2)
            {
                throw new MessagePackSerializationException($"Expected a two element array to carry a union, got {length} elements.");
            }

            var typeName = reader.ReadString();

            var type = DataTransferObjectAssembly.GetType(typeName, throwOnError: false);

            if (type == null)
            {
                throw new MessagePackSerializationException($"The '{typeName}' discriminator names no known data transfer object.");
            }

            return MessagePackSerializer.Deserialize(type, ref reader, options);
        }
    }
}
