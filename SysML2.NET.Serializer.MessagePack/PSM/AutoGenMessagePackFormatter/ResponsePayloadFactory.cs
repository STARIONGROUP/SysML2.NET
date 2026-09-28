// -------------------------------------------------------------------------------------------------
// <copyright file="ResponsePayloadFactory.cs" company="Starion Group S.A.">
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
    /// The purpose of the <see cref="ResponsePayloadFactory"/> class is to create an
    /// instance of <see cref="ResponsePayload"/>
    /// </summary>
    internal static class ResponsePayloadFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ResponsePayload"/> from the provided <see cref="IEnumerableIResponse"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerableIResponse"/> on the bases of which the <see cref="ResponsePayload"/> will be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="ResponsePayload"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the data items are null
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown when a data item is of a type that the envelope does not carry
        /// </exception>
        internal static ResponsePayload ToResponsePayload(this IEnumerable<IResponse> dataItems)
        {
            if (dataItems == null)
            {
                throw new ArgumentNullException(nameof(dataItems));
            }

            var payload = new ResponsePayload
            {
                Created = DateTime.UtcNow
            };

            foreach (var dataItem in dataItems)
            {
                switch (dataItem)
                {
                    case Branch branch:
                        payload.Branch.Add(branch);
                        break;

                    case Commit commit:
                        payload.Commit.Add(commit);
                        break;

                    case CompositeConstraint compositeConstraint:
                        payload.CompositeConstraint.Add(compositeConstraint);
                        break;

                    case DataDifference dataDifference:
                        payload.DataDifference.Add(dataDifference);
                        break;

                    case DataIdentity dataIdentity:
                        payload.DataIdentity.Add(dataIdentity);
                        break;

                    case DataVersion dataVersion:
                        payload.DataVersion.Add(dataVersion);
                        break;

                    case Error error:
                        payload.Error.Add(error);
                        break;

                    case ExternalData externalData:
                        payload.ExternalData.Add(externalData);
                        break;

                    case ExternalRelationship externalRelationship:
                        payload.ExternalRelationship.Add(externalRelationship);
                        break;

                    case PrimitiveConstraint primitiveConstraint:
                        payload.PrimitiveConstraint.Add(primitiveConstraint);
                        break;

                    case Project project:
                        payload.Project.Add(project);
                        break;

                    case ProjectUsage projectUsage:
                        payload.ProjectUsage.Add(projectUsage);
                        break;

                    case Query query:
                        payload.Query.Add(query);
                        break;

                    case Tag tag:
                        payload.Tag.Add(tag);
                        break;

                    default:
                        throw new NotSupportedException($"The {dataItem.GetType().Name} is not carried by the ResponsePayload.");
                }
            }

            return payload;
        }

        /// <summary>
        /// Flattens the <see cref="ResponsePayload"/> into the data items that it carries
        /// </summary>
        /// <param name="payload">
        /// The <see cref="ResponsePayload"/> that is to be flattened
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerableIResponse"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the payload is null
        /// </exception>
        internal static IEnumerable<IResponse> ToDataItems(this ResponsePayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var dataItems = new List<IResponse>();

            dataItems.AddRange(payload.Branch);
            dataItems.AddRange(payload.Commit);
            dataItems.AddRange(payload.CompositeConstraint);
            dataItems.AddRange(payload.DataDifference);
            dataItems.AddRange(payload.DataIdentity);
            dataItems.AddRange(payload.DataVersion);
            dataItems.AddRange(payload.Error);
            dataItems.AddRange(payload.ExternalData);
            dataItems.AddRange(payload.ExternalRelationship);
            dataItems.AddRange(payload.PrimitiveConstraint);
            dataItems.AddRange(payload.Project);
            dataItems.AddRange(payload.ProjectUsage);
            dataItems.AddRange(payload.Query);
            dataItems.AddRange(payload.Tag);

            return dataItems;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
