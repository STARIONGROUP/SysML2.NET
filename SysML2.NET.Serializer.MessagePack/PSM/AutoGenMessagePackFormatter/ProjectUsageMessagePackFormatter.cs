// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectUsageMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="ProjectUsageMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="ProjectUsage"/> type
    /// </summary>
    public class ProjectUsageMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<ProjectUsage>
    {
        /// <summary>
        /// Serializes a <see cref="ProjectUsage"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="projectUsage">
        /// The <see cref="ProjectUsage"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, ProjectUsage projectUsage, MessagePackSerializerOptions options)
        {
            if (projectUsage == null)
            {
                throw new ArgumentNullException(nameof(projectUsage), "The ProjectUsage may not be null");
            }

            writer.WriteArrayHeader(3);

            WriteGuidBin16(ref writer, projectUsage.Id);

            WriteGuidBin16(ref writer, projectUsage.UsedCommit);

            WriteGuidBin16(ref writer, projectUsage.UsedProject);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="ProjectUsage"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="ProjectUsage"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="ProjectUsage"/>.
        /// </returns>
        public ProjectUsage Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var projectUsage = new ProjectUsage();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        projectUsage.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        projectUsage.UsedCommit = ReadGuidBin16(ref reader);
                        break;

                    case 2:
                        projectUsage.UsedProject = ReadGuidBin16(ref reader);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return projectUsage;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
