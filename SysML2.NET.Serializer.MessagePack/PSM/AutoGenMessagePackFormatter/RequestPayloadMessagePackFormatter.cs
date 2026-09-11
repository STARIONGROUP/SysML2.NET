// -------------------------------------------------------------------------------------------------
// <copyright file="RequestPayloadMessagePackFormatter.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="RequestPayloadMessagePackFormatter"/> is to provide
    /// the contract for serialization of the <see cref="RequestPayload"/> type
    /// </summary>
    internal class RequestPayloadMessagePackFormatter : IMessagePackFormatter<RequestPayload>
    {
        /// <summary>
        /// Serializes a <see cref="RequestPayload"/>.
        /// </summary>
        /// <param name="writer">
        /// The writer to use when serializing the <see cref="RequestPayload"/>.
        /// </param>
        /// <param name="payload">
        /// The <see cref="RequestPayload"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the payload is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, RequestPayload payload, MessagePackSerializerOptions options)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "The RequestPayload may not be null");
            }

            var formatterResolver = options.Resolver;

            writer.WriteArrayHeader(13);

            writer.Write(payload.Created);

            writer.WriteArrayHeader(payload.BranchRequest.Count);
            foreach (var branchRequestDto in payload.BranchRequest)
            {
                formatterResolver.GetFormatterWithVerify<BranchRequest>().Serialize(ref writer, branchRequestDto, options);
            }

            writer.WriteArrayHeader(payload.CommitRequest.Count);
            foreach (var commitRequestDto in payload.CommitRequest)
            {
                formatterResolver.GetFormatterWithVerify<CommitRequest>().Serialize(ref writer, commitRequestDto, options);
            }

            writer.WriteArrayHeader(payload.CompositeConstraintRequest.Count);
            foreach (var compositeConstraintRequestDto in payload.CompositeConstraintRequest)
            {
                formatterResolver.GetFormatterWithVerify<CompositeConstraintRequest>().Serialize(ref writer, compositeConstraintRequestDto, options);
            }

            writer.WriteArrayHeader(payload.DataIdentityRequest.Count);
            foreach (var dataIdentityRequestDto in payload.DataIdentityRequest)
            {
                formatterResolver.GetFormatterWithVerify<DataIdentityRequest>().Serialize(ref writer, dataIdentityRequestDto, options);
            }

            writer.WriteArrayHeader(payload.DataVersionRequest.Count);
            foreach (var dataVersionRequestDto in payload.DataVersionRequest)
            {
                formatterResolver.GetFormatterWithVerify<DataVersionRequest>().Serialize(ref writer, dataVersionRequestDto, options);
            }

            writer.WriteArrayHeader(payload.ExternalDataRequest.Count);
            foreach (var externalDataRequestDto in payload.ExternalDataRequest)
            {
                formatterResolver.GetFormatterWithVerify<ExternalDataRequest>().Serialize(ref writer, externalDataRequestDto, options);
            }

            writer.WriteArrayHeader(payload.ExternalRelationshipRequest.Count);
            foreach (var externalRelationshipRequestDto in payload.ExternalRelationshipRequest)
            {
                formatterResolver.GetFormatterWithVerify<ExternalRelationshipRequest>().Serialize(ref writer, externalRelationshipRequestDto, options);
            }

            writer.WriteArrayHeader(payload.PrimitiveConstraintRequest.Count);
            foreach (var primitiveConstraintRequestDto in payload.PrimitiveConstraintRequest)
            {
                formatterResolver.GetFormatterWithVerify<PrimitiveConstraintRequest>().Serialize(ref writer, primitiveConstraintRequestDto, options);
            }

            writer.WriteArrayHeader(payload.ProjectRequest.Count);
            foreach (var projectRequestDto in payload.ProjectRequest)
            {
                formatterResolver.GetFormatterWithVerify<ProjectRequest>().Serialize(ref writer, projectRequestDto, options);
            }

            writer.WriteArrayHeader(payload.ProjectUsageRequest.Count);
            foreach (var projectUsageRequestDto in payload.ProjectUsageRequest)
            {
                formatterResolver.GetFormatterWithVerify<ProjectUsageRequest>().Serialize(ref writer, projectUsageRequestDto, options);
            }

            writer.WriteArrayHeader(payload.QueryRequest.Count);
            foreach (var queryRequestDto in payload.QueryRequest)
            {
                formatterResolver.GetFormatterWithVerify<QueryRequest>().Serialize(ref writer, queryRequestDto, options);
            }

            writer.WriteArrayHeader(payload.TagRequest.Count);
            foreach (var tagRequestDto in payload.TagRequest)
            {
                formatterResolver.GetFormatterWithVerify<TagRequest>().Serialize(ref writer, tagRequestDto, options);
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="RequestPayload"/>.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="RequestPayload"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="RequestPayload"/>.
        /// </returns>
        public RequestPayload Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var formatterResolver = options.Resolver;

            var payload = new RequestPayload();

            var slotCounter = reader.ReadArrayHeader();

            for (var i = 0; i < slotCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        payload.Created = reader.ReadDateTime();
                        break;

                    case 1:
                        var branchRequestLength = reader.ReadArrayHeader();
                        payload.BranchRequest.Capacity = branchRequestLength;

                        for (var valueCounter = 0; valueCounter < branchRequestLength; valueCounter++)
                        {
                            payload.BranchRequest.Add(formatterResolver.GetFormatterWithVerify<BranchRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 2:
                        var commitRequestLength = reader.ReadArrayHeader();
                        payload.CommitRequest.Capacity = commitRequestLength;

                        for (var valueCounter = 0; valueCounter < commitRequestLength; valueCounter++)
                        {
                            payload.CommitRequest.Add(formatterResolver.GetFormatterWithVerify<CommitRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 3:
                        var compositeConstraintRequestLength = reader.ReadArrayHeader();
                        payload.CompositeConstraintRequest.Capacity = compositeConstraintRequestLength;

                        for (var valueCounter = 0; valueCounter < compositeConstraintRequestLength; valueCounter++)
                        {
                            payload.CompositeConstraintRequest.Add(formatterResolver.GetFormatterWithVerify<CompositeConstraintRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 4:
                        var dataIdentityRequestLength = reader.ReadArrayHeader();
                        payload.DataIdentityRequest.Capacity = dataIdentityRequestLength;

                        for (var valueCounter = 0; valueCounter < dataIdentityRequestLength; valueCounter++)
                        {
                            payload.DataIdentityRequest.Add(formatterResolver.GetFormatterWithVerify<DataIdentityRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 5:
                        var dataVersionRequestLength = reader.ReadArrayHeader();
                        payload.DataVersionRequest.Capacity = dataVersionRequestLength;

                        for (var valueCounter = 0; valueCounter < dataVersionRequestLength; valueCounter++)
                        {
                            payload.DataVersionRequest.Add(formatterResolver.GetFormatterWithVerify<DataVersionRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 6:
                        var externalDataRequestLength = reader.ReadArrayHeader();
                        payload.ExternalDataRequest.Capacity = externalDataRequestLength;

                        for (var valueCounter = 0; valueCounter < externalDataRequestLength; valueCounter++)
                        {
                            payload.ExternalDataRequest.Add(formatterResolver.GetFormatterWithVerify<ExternalDataRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 7:
                        var externalRelationshipRequestLength = reader.ReadArrayHeader();
                        payload.ExternalRelationshipRequest.Capacity = externalRelationshipRequestLength;

                        for (var valueCounter = 0; valueCounter < externalRelationshipRequestLength; valueCounter++)
                        {
                            payload.ExternalRelationshipRequest.Add(formatterResolver.GetFormatterWithVerify<ExternalRelationshipRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 8:
                        var primitiveConstraintRequestLength = reader.ReadArrayHeader();
                        payload.PrimitiveConstraintRequest.Capacity = primitiveConstraintRequestLength;

                        for (var valueCounter = 0; valueCounter < primitiveConstraintRequestLength; valueCounter++)
                        {
                            payload.PrimitiveConstraintRequest.Add(formatterResolver.GetFormatterWithVerify<PrimitiveConstraintRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 9:
                        var projectRequestLength = reader.ReadArrayHeader();
                        payload.ProjectRequest.Capacity = projectRequestLength;

                        for (var valueCounter = 0; valueCounter < projectRequestLength; valueCounter++)
                        {
                            payload.ProjectRequest.Add(formatterResolver.GetFormatterWithVerify<ProjectRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 10:
                        var projectUsageRequestLength = reader.ReadArrayHeader();
                        payload.ProjectUsageRequest.Capacity = projectUsageRequestLength;

                        for (var valueCounter = 0; valueCounter < projectUsageRequestLength; valueCounter++)
                        {
                            payload.ProjectUsageRequest.Add(formatterResolver.GetFormatterWithVerify<ProjectUsageRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 11:
                        var queryRequestLength = reader.ReadArrayHeader();
                        payload.QueryRequest.Capacity = queryRequestLength;

                        for (var valueCounter = 0; valueCounter < queryRequestLength; valueCounter++)
                        {
                            payload.QueryRequest.Add(formatterResolver.GetFormatterWithVerify<QueryRequest>().Deserialize(ref reader, options));
                        }

                        break;

                    case 12:
                        var tagRequestLength = reader.ReadArrayHeader();
                        payload.TagRequest.Capacity = tagRequestLength;

                        for (var valueCounter = 0; valueCounter < tagRequestLength; valueCounter++)
                        {
                            payload.TagRequest.Add(formatterResolver.GetFormatterWithVerify<TagRequest>().Deserialize(ref reader, options));
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
