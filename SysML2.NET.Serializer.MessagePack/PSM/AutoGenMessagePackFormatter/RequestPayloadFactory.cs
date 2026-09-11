// -------------------------------------------------------------------------------------------------
// <copyright file="RequestPayloadFactory.cs" company="Starion Group S.A.">
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

    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// The purpose of the <see cref="RequestPayloadFactory"/> class is to create an
    /// instance of <see cref="RequestPayload"/>
    /// </summary>
    internal static class RequestPayloadFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="RequestPayload"/> from the provided <see cref="IEnumerableIRequest"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerableIRequest"/> on the bases of which the <see cref="RequestPayload"/> will be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="RequestPayload"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the data items are null
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when a data item is of a type that the envelope does not carry
        /// </exception>
        internal static RequestPayload ToRequestPayload(this IEnumerable<IRequest> dataItems)
        {
            if (dataItems == null)
            {
                throw new ArgumentNullException(nameof(dataItems));
            }

            var payload = new RequestPayload
            {
                Created = DateTime.UtcNow
            };

            foreach (var dataItem in dataItems)
            {
                switch (dataItem)
                {
                    case BranchRequest branchRequest:
                        payload.BranchRequest.Add(branchRequest);
                        break;

                    case CommitRequest commitRequest:
                        payload.CommitRequest.Add(commitRequest);
                        break;

                    case CompositeConstraintRequest compositeConstraintRequest:
                        payload.CompositeConstraintRequest.Add(compositeConstraintRequest);
                        break;

                    case DataIdentityRequest dataIdentityRequest:
                        payload.DataIdentityRequest.Add(dataIdentityRequest);
                        break;

                    case DataVersionRequest dataVersionRequest:
                        payload.DataVersionRequest.Add(dataVersionRequest);
                        break;

                    case ExternalDataRequest externalDataRequest:
                        payload.ExternalDataRequest.Add(externalDataRequest);
                        break;

                    case ExternalRelationshipRequest externalRelationshipRequest:
                        payload.ExternalRelationshipRequest.Add(externalRelationshipRequest);
                        break;

                    case PrimitiveConstraintRequest primitiveConstraintRequest:
                        payload.PrimitiveConstraintRequest.Add(primitiveConstraintRequest);
                        break;

                    case ProjectRequest projectRequest:
                        payload.ProjectRequest.Add(projectRequest);
                        break;

                    case ProjectUsageRequest projectUsageRequest:
                        payload.ProjectUsageRequest.Add(projectUsageRequest);
                        break;

                    case QueryRequest queryRequest:
                        payload.QueryRequest.Add(queryRequest);
                        break;

                    case TagRequest tagRequest:
                        payload.TagRequest.Add(tagRequest);
                        break;

                    default:
                        throw new NotSupportedException($"The {dataItem.GetType().Name} is not carried by the RequestPayload.");
                }
            }

            return payload;
        }

        /// <summary>
        /// Flattens the <see cref="RequestPayload"/> into the data items that it carries
        /// </summary>
        /// <param name="payload">
        /// The <see cref="RequestPayload"/> that is to be flattened
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerableIRequest"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the payload is null
        /// </exception>
        internal static IEnumerable<IRequest> ToDataItems(this RequestPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var dataItems = new List<IRequest>();

            dataItems.AddRange(payload.BranchRequest);
            dataItems.AddRange(payload.CommitRequest);
            dataItems.AddRange(payload.CompositeConstraintRequest);
            dataItems.AddRange(payload.DataIdentityRequest);
            dataItems.AddRange(payload.DataVersionRequest);
            dataItems.AddRange(payload.ExternalDataRequest);
            dataItems.AddRange(payload.ExternalRelationshipRequest);
            dataItems.AddRange(payload.PrimitiveConstraintRequest);
            dataItems.AddRange(payload.ProjectRequest);
            dataItems.AddRange(payload.ProjectUsageRequest);
            dataItems.AddRange(payload.QueryRequest);
            dataItems.AddRange(payload.TagRequest);

            return dataItems;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
