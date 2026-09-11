// -------------------------------------------------------------------------------------------------
// <copyright file="QueryMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="QueryMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="Query"/> type
    /// </summary>
    public class QueryMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<Query>
    {
        /// <summary>
        /// Serializes a <see cref="Query"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="query">
        /// The <see cref="Query"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, Query query, MessagePackSerializerOptions options)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query), "The Query may not be null");
            }

            writer.WriteArrayHeader(7);

            WriteGuidBin16(ref writer, query.Id);

            if (query.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(query.Alias.Count);

                foreach (var item in query.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(query.Description);

            writer.Write(query.Name);

            WriteGuidBin16(ref writer, query.OwningProject);

            if (query.Select == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(query.Select.Count);

                foreach (var item in query.Select)
                {
                    writer.Write(item);
                }
            }

            PsmMessagePackDispatcher.Write(ref writer, query.Where, options);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="Query"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="Query"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="Query"/>.
        /// </returns>
        public Query Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var query = new Query();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        query.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        query.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            query.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        query.Description = reader.ReadString();
                        break;

                    case 3:
                        query.Name = reader.ReadString();
                        break;

                    case 4:
                        query.OwningProject = ReadGuidBin16(ref reader);
                        break;

                    case 5:
                        var selectLength = reader.ReadArrayHeader();
                        query.Select = new List<string>(selectLength);

                        for (var valueCounter = 0; valueCounter < selectLength; valueCounter++)
                        {
                            query.Select.Add(reader.ReadString());
                        }

                        break;

                    case 6:
                        query.Where = (IConstraint)PsmMessagePackDispatcher.Read(ref reader, options);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return query;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
