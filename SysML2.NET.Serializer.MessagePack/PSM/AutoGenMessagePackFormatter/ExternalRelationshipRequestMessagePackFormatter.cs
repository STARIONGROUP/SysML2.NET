// -------------------------------------------------------------------------------------------------
// <copyright file="ExternalRelationshipRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="ExternalRelationshipRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="ExternalRelationshipRequest"/> type
    /// </summary>
    public class ExternalRelationshipRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<ExternalRelationshipRequest>
    {
        /// <summary>
        /// Serializes a <see cref="ExternalRelationshipRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="externalRelationshipRequest">
        /// The <see cref="ExternalRelationshipRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, ExternalRelationshipRequest externalRelationshipRequest, MessagePackSerializerOptions options)
        {
            if (externalRelationshipRequest == null)
            {
                throw new ArgumentNullException(nameof(externalRelationshipRequest), "The ExternalRelationshipRequest may not be null");
            }

            writer.WriteArrayHeader(5);

            if (externalRelationshipRequest.Id.HasValue)
            {
                WriteGuidBin16(ref writer, externalRelationshipRequest.Id.Value);
            }
            else
            {
                writer.WriteNil();
            }

            if (externalRelationshipRequest.ElementEnd.HasValue)
            {
                WriteGuidBin16(ref writer, externalRelationshipRequest.ElementEnd.Value);
            }
            else
            {
                writer.WriteNil();
            }

            if (externalRelationshipRequest.ExternalDataEnd.HasValue)
            {
                WriteGuidBin16(ref writer, externalRelationshipRequest.ExternalDataEnd.Value);
            }
            else
            {
                writer.WriteNil();
            }

            writer.Write(externalRelationshipRequest.Language);

            writer.Write(externalRelationshipRequest.Specification);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="ExternalRelationshipRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="ExternalRelationshipRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="ExternalRelationshipRequest"/>.
        /// </returns>
        public ExternalRelationshipRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var externalRelationshipRequest = new ExternalRelationshipRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        if (reader.TryReadNil())
                        {
                            externalRelationshipRequest.Id = null;
                        }
                        else
                        {
                            externalRelationshipRequest.Id = ReadGuidBin16(ref reader);
                        }

                        break;

                    case 1:
                        if (reader.TryReadNil())
                        {
                            externalRelationshipRequest.ElementEnd = null;
                        }
                        else
                        {
                            externalRelationshipRequest.ElementEnd = ReadGuidBin16(ref reader);
                        }

                        break;

                    case 2:
                        if (reader.TryReadNil())
                        {
                            externalRelationshipRequest.ExternalDataEnd = null;
                        }
                        else
                        {
                            externalRelationshipRequest.ExternalDataEnd = ReadGuidBin16(ref reader);
                        }

                        break;

                    case 3:
                        externalRelationshipRequest.Language = reader.ReadString();
                        break;

                    case 4:
                        externalRelationshipRequest.Specification = reader.ReadString();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return externalRelationshipRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
