// -------------------------------------------------------------------------------------------------
// <copyright file="CommitRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="CommitRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="CommitRequest"/> type
    /// </summary>
    public class CommitRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<CommitRequest>
    {
        /// <summary>
        /// Serializes a <see cref="CommitRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="commitRequest">
        /// The <see cref="CommitRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, CommitRequest commitRequest, MessagePackSerializerOptions options)
        {
            if (commitRequest == null)
            {
                throw new ArgumentNullException(nameof(commitRequest), "The CommitRequest may not be null");
            }

            writer.WriteArrayHeader(4);

            if (commitRequest.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(commitRequest.Alias.Count);

                foreach (var item in commitRequest.Alias)
                {
                    writer.Write(item);
                }
            }

            if (commitRequest.Change == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(commitRequest.Change.Count);

                foreach (var item in commitRequest.Change)
                {
                    options.Resolver.GetFormatterWithVerify<DataVersionRequest>().Serialize(ref writer, item, options);
                }
            }

            writer.Write(commitRequest.Description);

            writer.Write(commitRequest.Name);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="CommitRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="CommitRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="CommitRequest"/>.
        /// </returns>
        public CommitRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var commitRequest = new CommitRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        var aliasLength = reader.ReadArrayHeader();
                        commitRequest.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            commitRequest.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 1:
                        var changeLength = reader.ReadArrayHeader();
                        commitRequest.Change = new List<DataVersionRequest>(changeLength);

                        for (var valueCounter = 0; valueCounter < changeLength; valueCounter++)
                        {
                            commitRequest.Change.Add(options.Resolver.GetFormatterWithVerify<DataVersionRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 2:
                        commitRequest.Description = reader.ReadString();
                        break;

                    case 3:
                        commitRequest.Name = reader.ReadString();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return commitRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
