// -------------------------------------------------------------------------------------------------
// <copyright file="BranchMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="BranchMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="Branch"/> type
    /// </summary>
    public class BranchMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<Branch>
    {
        /// <summary>
        /// Serializes a <see cref="Branch"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="branch">
        /// The <see cref="Branch"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, Branch branch, MessagePackSerializerOptions options)
        {
            if (branch == null)
            {
                throw new ArgumentNullException(nameof(branch), "The Branch may not be null");
            }

            writer.WriteArrayHeader(9);

            WriteGuidBin16(ref writer, branch.Id);

            if (branch.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(branch.Alias.Count);

                foreach (var item in branch.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(branch.Created);

            if (branch.Deleted.HasValue)
            {
                writer.Write(branch.Deleted.Value);
            }
            else
            {
                writer.WriteNil();
            }

            writer.Write(branch.Description);

            if (branch.Head.HasValue)
            {
                WriteGuidBin16(ref writer, branch.Head.Value);
            }
            else
            {
                writer.WriteNil();
            }

            writer.Write(branch.Name);

            WriteGuidBin16(ref writer, branch.OwningProject);

            WriteGuidBin16(ref writer, branch.ReferencedCommit);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="Branch"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="Branch"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="Branch"/>.
        /// </returns>
        public Branch Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var branch = new Branch();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        branch.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        branch.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            branch.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        branch.Created = reader.ReadDateTime();
                        break;

                    case 3:
                        if (reader.TryReadNil())
                        {
                            branch.Deleted = null;
                        }
                        else
                        {
                            branch.Deleted = reader.ReadDateTime();
                        }

                        break;

                    case 4:
                        branch.Description = reader.ReadString();
                        break;

                    case 5:
                        if (reader.TryReadNil())
                        {
                            branch.Head = null;
                        }
                        else
                        {
                            branch.Head = ReadGuidBin16(ref reader);
                        }

                        break;

                    case 6:
                        branch.Name = reader.ReadString();
                        break;

                    case 7:
                        branch.OwningProject = ReadGuidBin16(ref reader);
                        break;

                    case 8:
                        branch.ReferencedCommit = ReadGuidBin16(ref reader);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return branch;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
