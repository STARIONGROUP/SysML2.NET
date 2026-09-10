// -------------------------------------------------------------------------------------------------
// <copyright file="SerializationProvider.cs" company="Starion Group S.A.">
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
    using System.Text.Json;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Delegate provider for the appropriate serialization method to serialize a <see cref="Type"/>
    /// </summary>
    internal static class SerializationProvider
    {
        /// <summary>
        /// Caches the delegate <see cref="Action{Object, Utf8JsonWriter, SerializationModeKind, Boolean}"/> for the
        /// <see cref="Type"/> that is to be serialized
        /// </summary>
        private static readonly Dictionary<Type, Action<object, Utf8JsonWriter, SerializationModeKind, bool>> SerializerActionMap =
            new Dictionary<Type, Action<object, Utf8JsonWriter, SerializationModeKind, bool>>
            {
                { typeof(SysML2.NET.PSM.DTO.Branch), BranchSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.BranchRequest), BranchRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.Commit), CommitSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.CommitRequest), CommitRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.CompositeConstraint), CompositeConstraintSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.CompositeConstraintRequest), CompositeConstraintRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.DataDifference), DataDifferenceSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.DataIdentity), DataIdentitySerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.DataIdentityRequest), DataIdentityRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.DataVersion), DataVersionSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.DataVersionRequest), DataVersionRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.Error), ErrorSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.ExternalData), ExternalDataSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.ExternalDataRequest), ExternalDataRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.ExternalRelationship), ExternalRelationshipSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.ExternalRelationshipRequest), ExternalRelationshipRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.PrimitiveConstraint), PrimitiveConstraintSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.PrimitiveConstraintRequest), PrimitiveConstraintRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.Project), ProjectSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.ProjectRequest), ProjectRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.ProjectUsage), ProjectUsageSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.ProjectUsageRequest), ProjectUsageRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.Query), QuerySerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.QueryRequest), QueryRequestSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.Tag), TagSerializer.Serialize },
                { typeof(SysML2.NET.PSM.DTO.TagRequest), TagRequestSerializer.Serialize },
            };

        /// <summary>
        /// Provides the delegate that serializes the specified <see cref="Type"/>
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// The delegate that serializes the <see cref="Type"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="Type"/> is not supported
        /// </exception>
        internal static Action<object, Utf8JsonWriter, SerializationModeKind, bool> Provide(Type type)
        {
            if (!SerializerActionMap.TryGetValue(type, out var action))
            {
                throw new NotSupportedException($"The {type.Name} is not supported by the SerializationProvider.");
            }

            return action;
        }

        /// <summary>
        /// Asserts whether the specified <see cref="Type"/> is supported by the provider
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> for which support is asserted
        /// </param>
        /// <returns>
        /// True when the <see cref="Type"/> is supported
        /// </returns>
        internal static bool IsTypeSupported(Type type)
        {
            return SerializerActionMap.ContainsKey(type);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
