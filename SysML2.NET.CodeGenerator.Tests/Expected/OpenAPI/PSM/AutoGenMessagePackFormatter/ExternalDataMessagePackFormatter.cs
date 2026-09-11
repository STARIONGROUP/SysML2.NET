// -------------------------------------------------------------------------------------------------
// <copyright file="ExternalDataMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="ExternalDataMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="ExternalData"/> type
    /// </summary>
    public class ExternalDataMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<ExternalData>
    {
        /// <summary>
        /// Serializes a <see cref="ExternalData"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="externalData">
        /// The <see cref="ExternalData"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, ExternalData externalData, MessagePackSerializerOptions options)
        {
            if (externalData == null)
            {
                throw new ArgumentNullException(nameof(externalData), "The ExternalData may not be null");
            }

            writer.WriteArrayHeader(2);

            WriteGuidBin16(ref writer, externalData.Id);

            writer.Write(externalData.ResourceIdentifier?.ToString());

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="ExternalData"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="ExternalData"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="ExternalData"/>.
        /// </returns>
        public ExternalData Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var externalData = new ExternalData();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        externalData.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        externalData.ResourceIdentifier = ReadUriOrNull(ref reader);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return externalData;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
