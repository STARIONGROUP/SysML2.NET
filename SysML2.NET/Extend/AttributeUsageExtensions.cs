// -------------------------------------------------------------------------------------------------
// <copyright file="AttributeUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.DataTypes;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// The <see cref="AttributeUsageExtensions"/> class provides extensions methods for
    /// the <see cref="IAttributeUsage"/> interface
    /// </summary>
    internal static class AttributeUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// Walks <c>OwnedRelationship</c> → <c>IFeatureTyping</c> → <c>Type</c> directly,
        /// filtering to <c>IDataType</c>. The AttributeUsage POCO's explicit-interface
        /// <c>IUsage.definition</c> impl delegates to <c>this.attributeDefinition</c>,
        /// which would route back into this method → stack overflow. Bypassing the
        /// instance property mirrors the technique in
        /// <see cref="UsageExtensions.ComputeDefinition" />.
        /// </remarks>
        /// <param name="attributeUsageSubject">
        /// The subject <see cref="IAttributeUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IDataType> ComputeAttributeDefinition(this IAttributeUsage attributeUsageSubject)
        {
            return attributeUsageSubject == null
                ? throw new ArgumentNullException(nameof(attributeUsageSubject))
                : [.. FeatureExtensions.ComputeType(attributeUsageSubject).OfType<IDataType>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="attributeUsageSubject">
        /// The subject <see cref="IAttributeUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static bool ComputeIsReference(this IAttributeUsage attributeUsageSubject)
        {
            return attributeUsageSubject == null
                ? throw new ArgumentNullException(nameof(attributeUsageSubject))
                : true;
        }

    }
}
