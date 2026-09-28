// -------------------------------------------------------------------------------------------------
// <copyright file="DataIdentityRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="DataIdentityRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="DataIdentityRequest"/> type
    /// </summary>
    public class DataIdentityRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<DataIdentityRequest>
    {
        /// <summary>
        /// Serializes a <see cref="DataIdentityRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="dataIdentityRequest">
        /// The <see cref="DataIdentityRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, DataIdentityRequest dataIdentityRequest, MessagePackSerializerOptions options)
        {
            if (dataIdentityRequest == null)
            {
                throw new ArgumentNullException(nameof(dataIdentityRequest), "The DataIdentityRequest may not be null");
            }

            writer.WriteArrayHeader(4);

            WriteGuidBin16(ref writer, dataIdentityRequest.Id);

            if (dataIdentityRequest.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(dataIdentityRequest.Alias.Count);

                foreach (var item in dataIdentityRequest.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(dataIdentityRequest.Description);

            writer.Write(dataIdentityRequest.Name);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="DataIdentityRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="DataIdentityRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="DataIdentityRequest"/>.
        /// </returns>
        public DataIdentityRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var dataIdentityRequest = new DataIdentityRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        dataIdentityRequest.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        dataIdentityRequest.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            dataIdentityRequest.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        dataIdentityRequest.Description = reader.ReadString();
                        break;

                    case 3:
                        dataIdentityRequest.Name = reader.ReadString();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return dataIdentityRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
