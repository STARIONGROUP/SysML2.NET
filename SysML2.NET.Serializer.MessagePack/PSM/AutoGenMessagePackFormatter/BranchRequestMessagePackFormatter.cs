// -------------------------------------------------------------------------------------------------
// <copyright file="BranchRequestMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="BranchRequestMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="BranchRequest"/> type
    /// </summary>
    public class BranchRequestMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<BranchRequest>
    {
        /// <summary>
        /// Serializes a <see cref="BranchRequest"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="branchRequest">
        /// The <see cref="BranchRequest"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, BranchRequest branchRequest, MessagePackSerializerOptions options)
        {
            if (branchRequest == null)
            {
                throw new ArgumentNullException(nameof(branchRequest), "The BranchRequest may not be null");
            }

            writer.WriteArrayHeader(4);

            if (branchRequest.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(branchRequest.Alias.Count);

                foreach (var item in branchRequest.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(branchRequest.Description);

            if (branchRequest.Head.HasValue)
            {
                WriteGuidBin16(ref writer, branchRequest.Head.Value);
            }
            else
            {
                writer.WriteNil();
            }

            writer.Write(branchRequest.Name);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="BranchRequest"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="BranchRequest"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="BranchRequest"/>.
        /// </returns>
        public BranchRequest Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var branchRequest = new BranchRequest();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        var aliasLength = reader.ReadArrayHeader();
                        branchRequest.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            branchRequest.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 1:
                        branchRequest.Description = reader.ReadString();
                        break;

                    case 2:
                        if (reader.TryReadNil())
                        {
                            branchRequest.Head = null;
                        }
                        else
                        {
                            branchRequest.Head = ReadGuidBin16(ref reader);
                        }

                        break;

                    case 3:
                        branchRequest.Name = reader.ReadString();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return branchRequest;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
