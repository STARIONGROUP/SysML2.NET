// -------------------------------------------------------------------------------------------------
// <copyright file="ViewDefinitionMessagePackFormatter.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;

    using SysML2.NET.Extensions.Core;

    using global::MessagePack;
    using global::MessagePack.Formatters;

    /// <summary>
    /// The purpose of the <see cref="ViewDefinitionMessagePackFormatter"/> is to provide
    /// the contract for MessagePack (de)serialization of the <see cref="SysML2.NET.Core.DTO.Systems.Views.ViewDefinition"/> type
    /// </summary>
    public class ViewDefinitionMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<SysML2.NET.Core.DTO.Systems.Views.ViewDefinition>
    {
        /// <summary>
        /// Serializes an <see cref="SysML2.NET.Core.DTO.Systems.Views.ViewDefinition"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="dto">
        /// The <see cref="SysML2.NET.Core.DTO.Systems.Views.ViewDefinition"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        public void Serialize(ref MessagePackWriter writer, SysML2.NET.Core.DTO.Systems.Views.ViewDefinition dto, MessagePackSerializerOptions options)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "The ViewDefinition may not be null");
            }

            writer.WriteArrayHeader(12);

            WriteGuidBin16(ref writer, dto.Id);

            writer.WriteArrayHeader(dto.AliasIds.Count);
            for (var i = 0; i < dto.AliasIds.Count; i++)
            {
                writer.Write(dto.AliasIds[i]);
            }

            writer.Write(dto.DeclaredName);

            writer.Write(dto.DeclaredShortName);

            writer.Write(dto.ElementId);

            writer.Write(dto.IsAbstract);

            writer.Write(dto.IsImpliedIncluded);

            writer.Write(dto.IsIndividual);

            writer.Write(dto.IsSufficient);

            writer.Write(dto.IsVariation);

            var ownedRelationship = dto.OwnedRelationship;
            writer.WriteArrayHeader(ownedRelationship.Count);
            for (var i = 0; i < ownedRelationship.Count; i++)
            {
                WriteGuidBin16(ref writer, ownedRelationship[i]);
            }

            if (dto.OwningRelationship.HasValue)
            {
                WriteGuidBin16(ref writer, dto.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNil();
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes an <see cref="SysML2.NET.Core.DTO.Systems.Views.ViewDefinition"/> DTO
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackWriter"/> to deserialize the <see cref="SysML2.NET.Core.DTO.Systems.Views.ViewDefinition"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="SysML2.NET.Core.DTO.Systems.Views.ViewDefinition"/>.
        /// </returns>
        public SysML2.NET.Core.DTO.Systems.Views.ViewDefinition Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var dto = new SysML2.NET.Core.DTO.Systems.Views.ViewDefinition();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                int valueLength;
                int valueCounter;

                switch (i)
                {
                    case 0:
                        dto.Id = ReadGuidBin16(ref reader);
                        break;
                    case 1:
                        valueLength = reader.ReadArrayHeader();
                        dto.AliasIds.Capacity = valueLength;
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            dto.AliasIds.Add(reader.ReadString());
                        }
                        break;
                    case 2:
                        dto.DeclaredName = reader.ReadString();
                        break;
                    case 3:
                        dto.DeclaredShortName = reader.ReadString();
                        break;
                    case 4:
                        dto.ElementId = reader.ReadString();
                        break;
                    case 5:
                        dto.IsAbstract = reader.ReadBoolean();
                        break;
                    case 6:
                        dto.IsImpliedIncluded = reader.ReadBoolean();
                        break;
                    case 7:
                        dto.IsIndividual = reader.ReadBoolean();
                        break;
                    case 8:
                        dto.IsSufficient = reader.ReadBoolean();
                        break;
                    case 9:
                        dto.IsVariation = reader.ReadBoolean();
                        break;
                    case 10:
                        valueLength = reader.ReadArrayHeader();
                        dto.OwnedRelationship.Capacity = valueLength;
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            dto.OwnedRelationship.Add(ReadGuidBin16(ref reader));
                        }
                        break;
                    case 11:
                        if (reader.TryReadNil())
                        {
                            dto.OwningRelationship = null;
                        }
                        else
                        {
                            dto.OwningRelationship = ReadGuidBin16(ref reader);
                        }
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return dto;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
