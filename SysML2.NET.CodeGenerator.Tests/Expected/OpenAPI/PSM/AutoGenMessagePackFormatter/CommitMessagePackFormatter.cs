// -------------------------------------------------------------------------------------------------
// <copyright file="CommitMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="CommitMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="Commit"/> type
    /// </summary>
    public class CommitMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<Commit>
    {
        /// <summary>
        /// Serializes a <see cref="Commit"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="commit">
        /// The <see cref="Commit"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, Commit commit, MessagePackSerializerOptions options)
        {
            if (commit == null)
            {
                throw new ArgumentNullException(nameof(commit), "The Commit may not be null");
            }

            writer.WriteArrayHeader(7);

            WriteGuidBin16(ref writer, commit.Id);

            if (commit.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(commit.Alias.Count);

                foreach (var item in commit.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(commit.Created);

            writer.Write(commit.Description);

            writer.Write(commit.Name);

            WriteGuidBin16(ref writer, commit.OwningProject);

            if (commit.PreviousCommit == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(commit.PreviousCommit.Count);

                foreach (var item in commit.PreviousCommit)
                {
                    WriteGuidBin16(ref writer, item);
                }
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="Commit"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="Commit"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="Commit"/>.
        /// </returns>
        public Commit Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var commit = new Commit();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        commit.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        commit.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            commit.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        commit.Created = reader.ReadDateTime();
                        break;

                    case 3:
                        commit.Description = reader.ReadString();
                        break;

                    case 4:
                        commit.Name = reader.ReadString();
                        break;

                    case 5:
                        commit.OwningProject = ReadGuidBin16(ref reader);
                        break;

                    case 6:
                        var previousCommitLength = reader.ReadArrayHeader();
                        commit.PreviousCommit = new List<Guid>(previousCommitLength);

                        for (var valueCounter = 0; valueCounter < previousCommitLength; valueCounter++)
                        {
                            commit.PreviousCommit.Add(ReadGuidBin16(ref reader));
                        }

                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return commit;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
