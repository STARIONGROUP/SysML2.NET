// -------------------------------------------------------------------------------------------------
// <copyright file="TagMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="TagMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="Tag"/> type
    /// </summary>
    public class TagMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<Tag>
    {
        /// <summary>
        /// Serializes a <see cref="Tag"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="tag">
        /// The <see cref="Tag"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, Tag tag, MessagePackSerializerOptions options)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag), "The Tag may not be null");
            }

            writer.WriteArrayHeader(9);

            WriteGuidBin16(ref writer, tag.Id);

            if (tag.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(tag.Alias.Count);

                foreach (var item in tag.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(tag.Created);

            if (tag.Deleted.HasValue)
            {
                writer.Write(tag.Deleted.Value);
            }
            else
            {
                writer.WriteNil();
            }

            writer.Write(tag.Description);

            writer.Write(tag.Name);

            WriteGuidBin16(ref writer, tag.OwningProject);

            WriteGuidBin16(ref writer, tag.ReferencedCommit);

            WriteGuidBin16(ref writer, tag.TaggedCommit);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="Tag"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="Tag"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="Tag"/>.
        /// </returns>
        public Tag Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var tag = new Tag();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        tag.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        tag.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            tag.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        tag.Created = reader.ReadDateTime();
                        break;

                    case 3:
                        if (reader.TryReadNil())
                        {
                            tag.Deleted = null;
                        }
                        else
                        {
                            tag.Deleted = reader.ReadDateTime();
                        }

                        break;

                    case 4:
                        tag.Description = reader.ReadString();
                        break;

                    case 5:
                        tag.Name = reader.ReadString();
                        break;

                    case 6:
                        tag.OwningProject = ReadGuidBin16(ref reader);
                        break;

                    case 7:
                        tag.ReferencedCommit = ReadGuidBin16(ref reader);
                        break;

                    case 8:
                        tag.TaggedCommit = ReadGuidBin16(ref reader);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return tag;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
