// -------------------------------------------------------------------------------------------------
// <copyright file="TagRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="TagRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="TagRequest"/> type
    /// </summary>
    public class TagRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<TagRequest>
    {
        /// <summary>
        /// Serializes a <see cref="TagRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="tagRequest">
        /// The <see cref="TagRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, TagRequest tagRequest, MessagePackSerializerOptions options)
        {
            if (tagRequest == null)
            {
                throw new ArgumentNullException(nameof(tagRequest), "The TagRequest may not be null");
            }

            writer.WriteArrayHeader(4);

            if (tagRequest.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(tagRequest.Alias.Count);

                foreach (var item in tagRequest.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(tagRequest.Description);

            writer.Write(tagRequest.Name);

            WriteGuidBin16(ref writer, tagRequest.TaggedCommit);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="TagRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="TagRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="TagRequest"/>.
        /// </returns>
        public TagRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var tagRequest = new TagRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        var aliasLength = reader.ReadArrayHeader();
                        tagRequest.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            tagRequest.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 1:
                        tagRequest.Description = reader.ReadString();
                        break;

                    case 2:
                        tagRequest.Name = reader.ReadString();
                        break;

                    case 3:
                        tagRequest.TaggedCommit = ReadGuidBin16(ref reader);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return tagRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
