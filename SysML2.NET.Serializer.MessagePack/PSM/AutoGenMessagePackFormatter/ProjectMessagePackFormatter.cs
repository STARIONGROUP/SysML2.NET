// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="ProjectMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="Project"/> type
    /// </summary>
    public class ProjectMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<Project>
    {
        /// <summary>
        /// Serializes a <see cref="Project"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="project">
        /// The <see cref="Project"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, Project project, MessagePackSerializerOptions options)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project), "The Project may not be null");
            }

            writer.WriteArrayHeader(6);

            WriteGuidBin16(ref writer, project.Id);

            if (project.Alias == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(project.Alias.Count);

                foreach (var item in project.Alias)
                {
                    writer.Write(item);
                }
            }

            writer.Write(project.Created);

            WriteGuidBin16(ref writer, project.DefaultBranch);

            writer.Write(project.Description);

            writer.Write(project.Name);

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="Project"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="Project"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="Project"/>.
        /// </returns>
        public Project Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var project = new Project();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        project.Id = ReadGuidBin16(ref reader);
                        break;

                    case 1:
                        var aliasLength = reader.ReadArrayHeader();
                        project.Alias = new List<string>(aliasLength);

                        for (var valueCounter = 0; valueCounter < aliasLength; valueCounter++)
                        {
                            project.Alias.Add(reader.ReadString());
                        }

                        break;

                    case 2:
                        project.Created = reader.ReadDateTime();
                        break;

                    case 3:
                        project.DefaultBranch = ReadGuidBin16(ref reader);
                        break;

                    case 4:
                        project.Description = reader.ReadString();
                        break;

                    case 5:
                        project.Name = reader.ReadString();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return project;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
