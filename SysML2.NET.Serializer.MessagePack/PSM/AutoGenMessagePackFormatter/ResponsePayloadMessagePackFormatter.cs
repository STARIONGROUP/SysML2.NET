// -------------------------------------------------------------------------------------------------
// <copyright file="ResponsePayloadMessagePackFormatter.cs" company="Starion Group S.A.">
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

    using SysML2.NET.PSM.DTO;

    using global::MessagePack;
    using global::MessagePack.Formatters;

    /// <summary>
    /// The purpose of the <see cref="ResponsePayloadMessagePackFormatter"/> is to provide
    /// the contract for serialization of the <see cref="ResponsePayload"/> type
    /// </summary>
    internal class ResponsePayloadMessagePackFormatter : IMessagePackFormatter<ResponsePayload>
    {
        /// <summary>
        /// Serializes a <see cref="ResponsePayload"/>.
        /// </summary>
        /// <param name="writer">
        /// The writer to use when serializing the <see cref="ResponsePayload"/>.
        /// </param>
        /// <param name="payload">
        /// The <see cref="ResponsePayload"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the payload is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, ResponsePayload payload, MessagePackSerializerOptions options)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "The ResponsePayload may not be null");
            }

            var formatterResolver = options.Resolver;

            writer.WriteArrayHeader(15);

            writer.Write(payload.Created);

            writer.WriteArrayHeader(payload.Branch.Count);
            foreach (var branchDto in payload.Branch)
            {
                formatterResolver.GetFormatterWithVerify<Branch>().Serialize(ref writer, branchDto, options);
            }

            writer.WriteArrayHeader(payload.Commit.Count);
            foreach (var commitDto in payload.Commit)
            {
                formatterResolver.GetFormatterWithVerify<Commit>().Serialize(ref writer, commitDto, options);
            }

            writer.WriteArrayHeader(payload.CompositeConstraint.Count);
            foreach (var compositeConstraintDto in payload.CompositeConstraint)
            {
                formatterResolver.GetFormatterWithVerify<CompositeConstraint>().Serialize(ref writer, compositeConstraintDto, options);
            }

            writer.WriteArrayHeader(payload.DataDifference.Count);
            foreach (var dataDifferenceDto in payload.DataDifference)
            {
                formatterResolver.GetFormatterWithVerify<DataDifference>().Serialize(ref writer, dataDifferenceDto, options);
            }

            writer.WriteArrayHeader(payload.DataIdentity.Count);
            foreach (var dataIdentityDto in payload.DataIdentity)
            {
                formatterResolver.GetFormatterWithVerify<DataIdentity>().Serialize(ref writer, dataIdentityDto, options);
            }

            writer.WriteArrayHeader(payload.DataVersion.Count);
            foreach (var dataVersionDto in payload.DataVersion)
            {
                formatterResolver.GetFormatterWithVerify<DataVersion>().Serialize(ref writer, dataVersionDto, options);
            }

            writer.WriteArrayHeader(payload.Error.Count);
            foreach (var errorDto in payload.Error)
            {
                formatterResolver.GetFormatterWithVerify<Error>().Serialize(ref writer, errorDto, options);
            }

            writer.WriteArrayHeader(payload.ExternalData.Count);
            foreach (var externalDataDto in payload.ExternalData)
            {
                formatterResolver.GetFormatterWithVerify<ExternalData>().Serialize(ref writer, externalDataDto, options);
            }

            writer.WriteArrayHeader(payload.ExternalRelationship.Count);
            foreach (var externalRelationshipDto in payload.ExternalRelationship)
            {
                formatterResolver.GetFormatterWithVerify<ExternalRelationship>().Serialize(ref writer, externalRelationshipDto, options);
            }

            writer.WriteArrayHeader(payload.PrimitiveConstraint.Count);
            foreach (var primitiveConstraintDto in payload.PrimitiveConstraint)
            {
                formatterResolver.GetFormatterWithVerify<PrimitiveConstraint>().Serialize(ref writer, primitiveConstraintDto, options);
            }

            writer.WriteArrayHeader(payload.Project.Count);
            foreach (var projectDto in payload.Project)
            {
                formatterResolver.GetFormatterWithVerify<Project>().Serialize(ref writer, projectDto, options);
            }

            writer.WriteArrayHeader(payload.ProjectUsage.Count);
            foreach (var projectUsageDto in payload.ProjectUsage)
            {
                formatterResolver.GetFormatterWithVerify<ProjectUsage>().Serialize(ref writer, projectUsageDto, options);
            }

            writer.WriteArrayHeader(payload.Query.Count);
            foreach (var queryDto in payload.Query)
            {
                formatterResolver.GetFormatterWithVerify<Query>().Serialize(ref writer, queryDto, options);
            }

            writer.WriteArrayHeader(payload.Tag.Count);
            foreach (var tagDto in payload.Tag)
            {
                formatterResolver.GetFormatterWithVerify<Tag>().Serialize(ref writer, tagDto, options);
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="ResponsePayload"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="ResponsePayload"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="ResponsePayload"/>.
        /// </returns>
        public ResponsePayload Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var formatterResolver = options.Resolver;

            var payload = new ResponsePayload();

            var slotCounter = reader.ReadArrayHeader();

            for (var i = 0; i < slotCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        payload.Created = reader.ReadDateTime();
                        break;

                    case 1:
                        var branchLength = reader.ReadArrayHeader();
                        payload.Branch.Capacity = branchLength;

                        for (var valueCounter = 0; valueCounter < branchLength; valueCounter++)
                        {
                            payload.Branch.Add(formatterResolver.GetFormatterWithVerify<Branch>().Deserialize(ref reader, options));
                        }

                        break;

                    case 2:
                        var commitLength = reader.ReadArrayHeader();
                        payload.Commit.Capacity = commitLength;

                        for (var valueCounter = 0; valueCounter < commitLength; valueCounter++)
                        {
                            payload.Commit.Add(formatterResolver.GetFormatterWithVerify<Commit>().Deserialize(ref reader, options));
                        }

                        break;

                    case 3:
                        var compositeConstraintLength = reader.ReadArrayHeader();
                        payload.CompositeConstraint.Capacity = compositeConstraintLength;

                        for (var valueCounter = 0; valueCounter < compositeConstraintLength; valueCounter++)
                        {
                            payload.CompositeConstraint.Add(formatterResolver.GetFormatterWithVerify<CompositeConstraint>().Deserialize(ref reader, options));
                        }

                        break;

                    case 4:
                        var dataDifferenceLength = reader.ReadArrayHeader();
                        payload.DataDifference.Capacity = dataDifferenceLength;

                        for (var valueCounter = 0; valueCounter < dataDifferenceLength; valueCounter++)
                        {
                            payload.DataDifference.Add(formatterResolver.GetFormatterWithVerify<DataDifference>().Deserialize(ref reader, options));
                        }

                        break;

                    case 5:
                        var dataIdentityLength = reader.ReadArrayHeader();
                        payload.DataIdentity.Capacity = dataIdentityLength;

                        for (var valueCounter = 0; valueCounter < dataIdentityLength; valueCounter++)
                        {
                            payload.DataIdentity.Add(formatterResolver.GetFormatterWithVerify<DataIdentity>().Deserialize(ref reader, options));
                        }

                        break;

                    case 6:
                        var dataVersionLength = reader.ReadArrayHeader();
                        payload.DataVersion.Capacity = dataVersionLength;

                        for (var valueCounter = 0; valueCounter < dataVersionLength; valueCounter++)
                        {
                            payload.DataVersion.Add(formatterResolver.GetFormatterWithVerify<DataVersion>().Deserialize(ref reader, options));
                        }

                        break;

                    case 7:
                        var errorLength = reader.ReadArrayHeader();
                        payload.Error.Capacity = errorLength;

                        for (var valueCounter = 0; valueCounter < errorLength; valueCounter++)
                        {
                            payload.Error.Add(formatterResolver.GetFormatterWithVerify<Error>().Deserialize(ref reader, options));
                        }

                        break;

                    case 8:
                        var externalDataLength = reader.ReadArrayHeader();
                        payload.ExternalData.Capacity = externalDataLength;

                        for (var valueCounter = 0; valueCounter < externalDataLength; valueCounter++)
                        {
                            payload.ExternalData.Add(formatterResolver.GetFormatterWithVerify<ExternalData>().Deserialize(ref reader, options));
                        }

                        break;

                    case 9:
                        var externalRelationshipLength = reader.ReadArrayHeader();
                        payload.ExternalRelationship.Capacity = externalRelationshipLength;

                        for (var valueCounter = 0; valueCounter < externalRelationshipLength; valueCounter++)
                        {
                            payload.ExternalRelationship.Add(formatterResolver.GetFormatterWithVerify<ExternalRelationship>().Deserialize(ref reader, options));
                        }

                        break;

                    case 10:
                        var primitiveConstraintLength = reader.ReadArrayHeader();
                        payload.PrimitiveConstraint.Capacity = primitiveConstraintLength;

                        for (var valueCounter = 0; valueCounter < primitiveConstraintLength; valueCounter++)
                        {
                            payload.PrimitiveConstraint.Add(formatterResolver.GetFormatterWithVerify<PrimitiveConstraint>().Deserialize(ref reader, options));
                        }

                        break;

                    case 11:
                        var projectLength = reader.ReadArrayHeader();
                        payload.Project.Capacity = projectLength;

                        for (var valueCounter = 0; valueCounter < projectLength; valueCounter++)
                        {
                            payload.Project.Add(formatterResolver.GetFormatterWithVerify<Project>().Deserialize(ref reader, options));
                        }

                        break;

                    case 12:
                        var projectUsageLength = reader.ReadArrayHeader();
                        payload.ProjectUsage.Capacity = projectUsageLength;

                        for (var valueCounter = 0; valueCounter < projectUsageLength; valueCounter++)
                        {
                            payload.ProjectUsage.Add(formatterResolver.GetFormatterWithVerify<ProjectUsage>().Deserialize(ref reader, options));
                        }

                        break;

                    case 13:
                        var queryLength = reader.ReadArrayHeader();
                        payload.Query.Capacity = queryLength;

                        for (var valueCounter = 0; valueCounter < queryLength; valueCounter++)
                        {
                            payload.Query.Add(formatterResolver.GetFormatterWithVerify<Query>().Deserialize(ref reader, options));
                        }

                        break;

                    case 14:
                        var tagLength = reader.ReadArrayHeader();
                        payload.Tag.Capacity = tagLength;

                        for (var valueCounter = 0; valueCounter < tagLength; valueCounter++)
                        {
                            payload.Tag.Add(formatterResolver.GetFormatterWithVerify<Tag>().Deserialize(ref reader, options));
                        }

                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return payload;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
