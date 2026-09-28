// -------------------------------------------------------------------------------------------------
// <copyright file="DataVersionRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="DataVersionRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="DataVersionRequest"/> type
    /// </summary>
    public class DataVersionRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<DataVersionRequest>
    {
        /// <summary>
        /// Serializes a <see cref="DataVersionRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="dataVersionRequest">
        /// The <see cref="DataVersionRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, DataVersionRequest dataVersionRequest, MessagePackSerializerOptions options)
        {
            if (dataVersionRequest == null)
            {
                throw new ArgumentNullException(nameof(dataVersionRequest), "The DataVersionRequest may not be null");
            }

            writer.WriteArrayHeader(5);

            if (dataVersionRequest.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(dataVersionRequest.Alias.Count);

                foreach (var item in dataVersionRequest.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(dataVersionRequest.Description);

            if (dataVersionRequest.Identity == null)
            {
                writer.WriteNil();
            }
            else
            {
                options.Resolver.GetFormatterWithVerify<DataIdentityRequest>().Serialize(ref writer, dataVersionRequest.Identity, options);
            }

            writer.Write(dataVersionRequest.Name);

            PsmMessagePackDispatcher.Write(ref writer, dataVersionRequest.Payload, options);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="DataVersionRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="DataVersionRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="DataVersionRequest"/>.
        /// </returns>
        public DataVersionRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var dataVersionRequest = new DataVersionRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        var aliasLength = reader.ReadArrayHeader();
                        dataVersionRequest.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            dataVersionRequest.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 1:
                        dataVersionRequest.Description = reader.ReadString();
                        break;

                    case 2:
                        dataVersionRequest.Identity = options.Resolver.GetFormatterWithVerify<DataIdentityRequest>().Deserialize(ref reader, options);
                        break;

                    case 3:
                        dataVersionRequest.Name = reader.ReadString();
                        break;

                    case 4:
                        dataVersionRequest.Payload = (SysML2.NET.Common.IDataRequest)PsmMessagePackDispatcher.Read(ref reader, options);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return dataVersionRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
