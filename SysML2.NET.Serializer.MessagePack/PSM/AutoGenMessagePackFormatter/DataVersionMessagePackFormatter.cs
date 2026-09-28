// -------------------------------------------------------------------------------------------------
// <copyright file="DataVersionMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="DataVersionMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="DataVersion"/> type
    /// </summary>
    public class DataVersionMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<DataVersion>
    {
        /// <summary>
        /// Serializes a <see cref="DataVersion"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="dataVersion">
        /// The <see cref="DataVersion"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, DataVersion dataVersion, MessagePackSerializerOptions options)
        {
            if (dataVersion == null)
            {
                throw new ArgumentNullException(nameof(dataVersion), "The DataVersion may not be null");
            }

            writer.WriteArrayHeader(6);

            WriteGuidBin16(ref writer, dataVersion.Id);

            if (dataVersion.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(dataVersion.Alias.Count);

                foreach (var item in dataVersion.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(dataVersion.Description);

            if (dataVersion.Identity == null)
            {
                writer.WriteNil();
            }
            else
            {
                options.Resolver.GetFormatterWithVerify<DataIdentity>().Serialize(ref writer, dataVersion.Identity, options);
            }

            writer.Write(dataVersion.Name);

            PsmMessagePackDispatcher.Write(ref writer, dataVersion.Payload, options);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="DataVersion"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="DataVersion"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="DataVersion"/>.
        /// </returns>
        public DataVersion Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var dataVersion = new DataVersion();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        dataVersion.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        dataVersion.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            dataVersion.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        dataVersion.Description = reader.ReadString();
                        break;

                    case 3:
                        dataVersion.Identity = options.Resolver.GetFormatterWithVerify<DataIdentity>().Deserialize(ref reader, options);
                        break;

                    case 4:
                        dataVersion.Name = reader.ReadString();
                        break;

                    case 5:
                        dataVersion.Payload = (SysML2.NET.Common.IData)PsmMessagePackDispatcher.Read(ref reader, options);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return dataVersion;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
