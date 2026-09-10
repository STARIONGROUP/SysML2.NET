// -------------------------------------------------------------------------------------------------
// <copyright file="ResponseDeSerializationProvider.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.PSM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Delegate provider for the appropriate deserialization method to deserialize a <see cref="IResponse"/>
    /// </summary>
    /// <remarks>
    /// A response and its counterpart in the other family share a <c>@type</c> discriminator, so the caller
    /// states which family it expects rather than the payload declaring it.
    /// </remarks>
    internal static class ResponseDeSerializationProvider
    {
        /// <summary>
        /// Caches the <see cref="DeSerializeResponseDelegate"/> for the <c>@type</c> that is to be deserialized
        /// </summary>
        private static readonly Dictionary<string, DeSerializeResponseDelegate> DeSerializerActionMap =
            new Dictionary<string, DeSerializeResponseDelegate>(StringComparer.OrdinalIgnoreCase)
            {
                { "Branch", BranchDeSerializer.DeSerialize },
                { "Commit", CommitDeSerializer.DeSerialize },
                { "CompositeConstraint", CompositeConstraintDeSerializer.DeSerialize },
                { "DataDifference", DataDifferenceDeSerializer.DeSerialize },
                { "DataIdentity", DataIdentityDeSerializer.DeSerialize },
                { "DataVersion", DataVersionDeSerializer.DeSerialize },
                { "Error", ErrorDeSerializer.DeSerialize },
                { "ExternalData", ExternalDataDeSerializer.DeSerialize },
                { "ExternalRelationship", ExternalRelationshipDeSerializer.DeSerialize },
                { "PrimitiveConstraint", PrimitiveConstraintDeSerializer.DeSerialize },
                { "Project", ProjectDeSerializer.DeSerialize },
                { "ProjectUsage", ProjectUsageDeSerializer.DeSerialize },
                { "Query", QueryDeSerializer.DeSerialize },
                { "Tag", TagDeSerializer.DeSerialize },
            };

        /// <summary>
        /// Provides the delegate that deserializes the specified <c>@type</c>
        /// </summary>
        /// <param name="typeName">
        /// The <c>@type</c> that is to be deserialized
        /// </param>
        /// <returns>
        /// The delegate that deserializes the <c>@type</c>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <c>@type</c> is not supported
        /// </exception>
        internal static DeSerializeResponseDelegate Provide(string typeName)
        {
            if (!DeSerializerActionMap.TryGetValue(typeName, out var action))
            {
                throw new NotSupportedException($"The {typeName} is not supported by the ResponseDeSerializationProvider.");
            }

            return action;
        }

        /// <summary>
        /// Asserts whether the specified <c>@type</c> is supported by the provider
        /// </summary>
        /// <param name="typeName">
        /// The <c>@type</c> for which support is asserted
        /// </param>
        /// <returns>
        /// True when the <c>@type</c> is supported
        /// </returns>
        internal static bool IsTypeSupported(string typeName)
        {
            return typeName != null && DeSerializerActionMap.ContainsKey(typeName);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
