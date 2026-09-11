// -------------------------------------------------------------------------------------------------
// <copyright file="PsmDataResolverGetFormatterHelper.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// Helper class that resolves a MessagePack formatter for the Systems Modeling API and Services types
    /// </summary>
    internal static class PsmDataResolverGetFormatterHelper
    {
        /// <summary>
        /// Maps a <see cref="Type"/> to the formatter that serializes it
        /// </summary>
        private static readonly Dictionary<Type, object> FormatterMap = new()
        {
            { typeof(RequestPayload), new RequestPayloadMessagePackFormatter() },
            { typeof(ResponsePayload), new ResponsePayloadMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.Branch), new BranchMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.BranchRequest), new BranchRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.Commit), new CommitMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.CommitRequest), new CommitRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.CompositeConstraint), new CompositeConstraintMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.CompositeConstraintRequest), new CompositeConstraintRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.DataDifference), new DataDifferenceMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.DataIdentity), new DataIdentityMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.DataIdentityRequest), new DataIdentityRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.DataVersion), new DataVersionMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.DataVersionRequest), new DataVersionRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.Error), new ErrorMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.ExternalData), new ExternalDataMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.ExternalDataRequest), new ExternalDataRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.ExternalRelationship), new ExternalRelationshipMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.ExternalRelationshipRequest), new ExternalRelationshipRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.PrimitiveConstraint), new PrimitiveConstraintMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.PrimitiveConstraintRequest), new PrimitiveConstraintRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.Project), new ProjectMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.ProjectRequest), new ProjectRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.ProjectUsage), new ProjectUsageMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.ProjectUsageRequest), new ProjectUsageRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.Query), new QueryMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.QueryRequest), new QueryRequestMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.Tag), new TagMessagePackFormatter() },
            { typeof(SysML2.NET.PSM.DTO.TagRequest), new TagRequestMessagePackFormatter() },
        };

        /// <summary>
        /// Resolves the formatter that serializes the specified <see cref="Type"/>
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// The formatter, or <c>null</c> when the <see cref="Type"/> is not a Systems Modeling API and Services type
        /// </returns>
        internal static object GetFormatter(Type type)
        {
            return FormatterMap.TryGetValue(type, out var formatter) ? formatter : null;
        }

        /// <summary>
        /// Asserts whether the specified <see cref="Type"/> is a Systems Modeling API and Services type
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> for which support is asserted
        /// </param>
        /// <returns>
        /// True when the <see cref="Type"/> is supported
        /// </returns>
        internal static bool IsTypeSupported(Type type)
        {
            return FormatterMap.ContainsKey(type);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
