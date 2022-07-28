// -------------------------------------------------------------------------------------------------
// <copyright file="ElementSerializer.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.AutoGenBuilder
{
    using System.Text.Json;

    using SysML2.NET.DTO;

    /// <summary>
    /// The purpose of the <see cref="ElementSerializer"/> is to build <see cref="JObject"/> and a <see cref="AnnouncementOfOpportunity"/> object
    /// </summary>
    internal static class ElementSerializer
    {
        /// <summary>
        /// Serializes an instance of <see cref="Element"/> using an <see cref="Utf8JsonWriter"/>
        /// </summary>
        /// <param name="element">
        /// The <see cref="Element"/> to serialize
        /// </param>
        /// <param name="writer">
        /// The target <see cref="Utf8JsonWriter"/>
        /// </param>
        public static void Serialize(Element element, Utf8JsonWriter writer)
        {
            writer.WriteStartObject();

            // AliasIds 
            writer.WriteStartArray("aliasIds");
            foreach (var aliasId in element.AliasIds)
            {
                writer.WriteStringValue(aliasId);
            }
            writer.WriteEndArray();

            // ElementId
            writer.WritePropertyName("elementId");
            writer.WriteStringValue(element.ElementId);

            // Name
            writer.WritePropertyName("name");
            writer.WriteStringValue(element.Name);

            // OwnedRelationship
            writer.WriteStartArray("ownedRelationship");
            foreach (var ownedRelationship in element.OwnedRelationship)
            {
                writer.WriteStringValue(ownedRelationship);
            }
            writer.WriteEndArray();
            
            // OwningRelationship
            writer.WritePropertyName("owningRelationship");
            if (element.OwningRelationship.HasValue)
            {
                writer.WriteStringValue(element.OwningRelationship.Value);
            }
            else
            {
                writer.WriteNullValue();
            }

            // ShortName
            writer.WritePropertyName("shortName");
            writer.WriteStringValue(element.ShortName);

            writer.WriteEndObject();
        }
    }
}
