// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="TransitionFeatureMembershipMessagePackFormatter"/> is to provide
    /// the contract for MessagePack (de)serialization of the <see cref="SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership"/> type
    /// </summary>
    public class TransitionFeatureMembershipMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership>
    {
        /// <summary>
        /// Serializes an <see cref="SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="dto">
        /// The <see cref="SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        public void Serialize(ref MessagePackWriter writer, SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership dto, MessagePackSerializerOptions options)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "The TransitionFeatureMembership may not be null");
            }

            writer.WriteArrayHeader(13);

            WriteGuidBin16(ref writer, dto.Id);

            writer.WriteArrayHeader(dto.AliasIds.Count);
            for (var i = 0; i < dto.AliasIds.Count; i++)
            {
                writer.Write(dto.AliasIds[i]);
            }

            writer.Write(dto.DeclaredName);

            writer.Write(dto.DeclaredShortName);

            writer.Write(dto.ElementId);

            writer.Write(dto.IsImplied);

            writer.Write(dto.IsImpliedIncluded);

            var kindBytes = TransitionFeatureKindProvider.ToUtf8LowerBytes(dto.Kind);
            writer.WriteString(kindBytes);

            var ownedRelatedElement = dto.OwnedRelatedElement;
            writer.WriteArrayHeader(ownedRelatedElement.Count);
            for (var i = 0; i < ownedRelatedElement.Count; i++)
            {
                WriteGuidBin16(ref writer, ownedRelatedElement[i]);
            }

            var ownedRelationship = dto.OwnedRelationship;
            writer.WriteArrayHeader(ownedRelationship.Count);
            for (var i = 0; i < ownedRelationship.Count; i++)
            {
                WriteGuidBin16(ref writer, ownedRelationship[i]);
            }

            if (dto.OwningRelatedElement.HasValue)
            {
                WriteGuidBin16(ref writer, dto.OwningRelatedElement.Value);
            }
            else
            {
                writer.WriteNil();
            }

            if (dto.OwningRelationship.HasValue)
            {
                WriteGuidBin16(ref writer, dto.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNil();
            }

            var visibilityBytes = VisibilityKindProvider.ToUtf8LowerBytes(dto.Visibility);
            writer.WriteString(visibilityBytes);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes an <see cref="SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership"/> DTO
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackWriter"/> to deserialize the <see cref="SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership"/>.
        /// </returns>
        public SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var dto = new SysML2.NET.Core.DTO.Systems.States.TransitionFeatureMembership();

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
                        dto.IsImplied = reader.ReadBoolean();
                        break;
                    case 6:
                        dto.IsImpliedIncluded = reader.ReadBoolean();
                        break;
                    case 7:
                        var kindSequence = reader.ReadStringSequence();
                        dto.Kind = TransitionFeatureKindProvider.Parse(kindSequence.Value);
                        break;
                    case 8:
                        valueLength = reader.ReadArrayHeader();
                        dto.OwnedRelatedElement.Capacity = valueLength;
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            dto.OwnedRelatedElement.Add(ReadGuidBin16(ref reader));
                        }
                        break;
                    case 9:
                        valueLength = reader.ReadArrayHeader();
                        dto.OwnedRelationship.Capacity = valueLength;
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            dto.OwnedRelationship.Add(ReadGuidBin16(ref reader));
                        }
                        break;
                    case 10:
                        if (reader.TryReadNil())
                        {
                            dto.OwningRelatedElement = null;
                        }
                        else
                        {
                            dto.OwningRelatedElement = ReadGuidBin16(ref reader);
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
                    case 12:
                        var visibilitySequence = reader.ReadStringSequence();
                        dto.Visibility = VisibilityKindProvider.Parse(visibilitySequence.Value);
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
