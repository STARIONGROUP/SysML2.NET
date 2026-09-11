// -------------------------------------------------------------------------------------------------
// <copyright file="DataIdentityMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="DataIdentityMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="DataIdentity"/> type
    /// </summary>
    public class DataIdentityMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<DataIdentity>
    {
        /// <summary>
        /// Serializes a <see cref="DataIdentity"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="dataIdentity">
        /// The <see cref="DataIdentity"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, DataIdentity dataIdentity, MessagePackSerializerOptions options)
        {
            if (dataIdentity == null)
            {
                throw new ArgumentNullException(nameof(dataIdentity), "The DataIdentity may not be null");
            }

            writer.WriteArrayHeader(4);

            WriteGuidBin16(ref writer, dataIdentity.Id);

            if (dataIdentity.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(dataIdentity.Alias.Count);

                foreach (var item in dataIdentity.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(dataIdentity.Description);

            writer.Write(dataIdentity.Name);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="DataIdentity"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="DataIdentity"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="DataIdentity"/>.
        /// </returns>
        public DataIdentity Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var dataIdentity = new DataIdentity();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        dataIdentity.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        dataIdentity.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            dataIdentity.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        dataIdentity.Description = reader.ReadString();
                        break;

                    case 3:
                        dataIdentity.Name = reader.ReadString();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return dataIdentity;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
