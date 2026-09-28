// -------------------------------------------------------------------------------------------------
// <copyright file="PrimitiveConstraintRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="PrimitiveConstraintRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="PrimitiveConstraintRequest"/> type
    /// </summary>
    public class PrimitiveConstraintRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<PrimitiveConstraintRequest>
    {
        /// <summary>
        /// Serializes a <see cref="PrimitiveConstraintRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="primitiveConstraintRequest">
        /// The <see cref="PrimitiveConstraintRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, PrimitiveConstraintRequest primitiveConstraintRequest, MessagePackSerializerOptions options)
        {
            if (primitiveConstraintRequest == null)
            {
                throw new ArgumentNullException(nameof(primitiveConstraintRequest), "The PrimitiveConstraintRequest may not be null");
            }

            writer.WriteArrayHeader(4);

            if (primitiveConstraintRequest.Inverse.HasValue)
            {
                writer.Write(primitiveConstraintRequest.Inverse.Value);
            }
            else
            {
                writer.WriteNil();
            }

            writer.WriteString(PrimitiveConstraintRequestOperatorProvider.ToUtf8Bytes(primitiveConstraintRequest.Operator));

            writer.Write(primitiveConstraintRequest.Property);

            if (primitiveConstraintRequest.Value == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(primitiveConstraintRequest.Value.Count);

                foreach (var item in primitiveConstraintRequest.Value)
                {
                    ConstraintValueMessagePackWriter.Write(ref writer, item);
                }
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="PrimitiveConstraintRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="PrimitiveConstraintRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="PrimitiveConstraintRequest"/>.
        /// </returns>
        public PrimitiveConstraintRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var primitiveConstraintRequest = new PrimitiveConstraintRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        if (reader.TryReadNil())
                        {
                            primitiveConstraintRequest.Inverse = null;
                        }
                        else
                        {
                            primitiveConstraintRequest.Inverse = reader.ReadBoolean();
                        }

                        break;

                    case 1:
                        primitiveConstraintRequest.Operator = PrimitiveConstraintRequestOperatorProvider.Parse(reader.ReadStringSequence().Value);
                        break;

                    case 2:
                        primitiveConstraintRequest.Property = reader.ReadString();
                        break;

                    case 3:
                        var valueLength = reader.ReadArrayHeader();
                        primitiveConstraintRequest.Value = new List<ConstraintValue>(valueLength);

                        for (var valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            primitiveConstraintRequest.Value.Add(ConstraintValueMessagePackReader.Read(ref reader));
                        }

                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return primitiveConstraintRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
