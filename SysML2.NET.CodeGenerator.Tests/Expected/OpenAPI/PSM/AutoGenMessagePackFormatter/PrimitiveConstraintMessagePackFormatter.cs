// -------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraintMessagePackFormatter.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack.PSM
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Extensions.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.MessagePack.Core;

    using global::MessagePack;
    using global::MessagePack.Formatters;

    /// <summary>
    /// The purpose of the <see cref="PrimitiveConstraintMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="PrimitiveConstraint"/> type
    /// </summary>
    public class PrimitiveConstraintMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<PrimitiveConstraint>
    {
        /// <summary>
        /// Serializes a <see cref="PrimitiveConstraint"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="primitiveConstraint">
        /// The <see cref="PrimitiveConstraint"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, PrimitiveConstraint primitiveConstraint, MessagePackSerializerOptions options)
        {
            if (primitiveConstraint == null)
            {
                throw new ArgumentNullException(nameof(primitiveConstraint), "The PrimitiveConstraint may not be null");
            }

            writer.WriteArrayHeader(4);

            writer.Write(primitiveConstraint.Inverse);

            writer.WriteString(PrimitiveConstraintOperatorProvider.ToUtf8Bytes(primitiveConstraint.Operator));

            writer.Write(primitiveConstraint.Property);

            if (primitiveConstraint.Value == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(primitiveConstraint.Value.Count);

                foreach (var item in primitiveConstraint.Value)
                {
                    ConstraintValueMessagePackWriter.Write(ref writer, item);
                }
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="PrimitiveConstraint"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="PrimitiveConstraint"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="PrimitiveConstraint"/>.
        /// </returns>
        public PrimitiveConstraint Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var primitiveConstraint = new PrimitiveConstraint();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        primitiveConstraint.Inverse = reader.ReadBoolean();
                        break;

                    case 1:
                        primitiveConstraint.Operator = PrimitiveConstraintOperatorProvider.Parse(reader.ReadStringSequence().Value);
                        break;

                    case 2:
                        primitiveConstraint.Property = reader.ReadString();
                        break;

                    case 3:
                        var valueLength = reader.ReadArrayHeader();
                        primitiveConstraint.Value = new List<ConstraintValue>(valueLength);

                        for (var valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            primitiveConstraint.Value.Add(ConstraintValueMessagePackReader.Read(ref reader));
                        }

                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return primitiveConstraint;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
