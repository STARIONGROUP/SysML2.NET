// -------------------------------------------------------------------------------------------------
// <copyright file="QueryRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="QueryRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="QueryRequest"/> type
    /// </summary>
    public class QueryRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<QueryRequest>
    {
        /// <summary>
        /// Serializes a <see cref="QueryRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="queryRequest">
        /// The <see cref="QueryRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, QueryRequest queryRequest, MessagePackSerializerOptions options)
        {
            if (queryRequest == null)
            {
                throw new ArgumentNullException(nameof(queryRequest), "The QueryRequest may not be null");
            }

            writer.WriteArrayHeader(5);

            if (queryRequest.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(queryRequest.Alias.Count);

                foreach (var item in queryRequest.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(queryRequest.Description);

            writer.Write(queryRequest.Name);

            if (queryRequest.Select == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(queryRequest.Select.Count);

                foreach (var item in queryRequest.Select)
                {
                    writer.Write(item);
                }
            }

            PsmMessagePackDispatcher.Write(ref writer, queryRequest.Where, options);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="QueryRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="QueryRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="QueryRequest"/>.
        /// </returns>
        public QueryRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var queryRequest = new QueryRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        var aliasLength = reader.ReadArrayHeader();
                        queryRequest.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            queryRequest.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 1:
                        queryRequest.Description = reader.ReadString();
                        break;

                    case 2:
                        queryRequest.Name = reader.ReadString();
                        break;

                    case 3:
                        var selectLength = reader.ReadArrayHeader();
                        queryRequest.Select = new List<string>(selectLength);

                        for (var valueCounter = 0; valueCounter < selectLength; valueCounter++)
                        {
                            queryRequest.Select.Add(reader.ReadString());
                        }

                        break;

                    case 4:
                        queryRequest.Where = (IConstraintRequest)PsmMessagePackDispatcher.Read(ref reader, options);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return queryRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
