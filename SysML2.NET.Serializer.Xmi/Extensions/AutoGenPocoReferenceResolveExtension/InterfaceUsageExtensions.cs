// -------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.Serializer.Xmi.Extensions
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// Provides extensions methods for the <see cref="{SysML2.NET.Core.POCO.Systems.Interfaces.{this.Name}}"/> to help resolve reference for properties
    /// </summary>
    public static class InterfaceUsageExtensions
    {
        /// <summary>
        /// Resolve and assign single reference value for a specific property
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceUsage"/> that should have the value of a property to be set</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="reference">The identifier of the <see cref="IData"/> value to set</param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/></param>
        /// <param name="logger">The <see cref="ILogger" /> used to produce log statement</param>
        public static void ResolveAndAssignSingleValueReference(this SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceUsage poco, string propertyName, Guid reference, IXmiDataCache xmiDataCache, ILogger logger)
        {
            if (poco == null)
            {
                throw new ArgumentNullException(nameof(poco));
            }

            if (xmiDataCache == null)
            {
                throw new ArgumentNullException(nameof(xmiDataCache));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            switch (propertyName)
            {
                case "owningRelatedElement":
                    {
                        if (!xmiDataCache.TryGetData(reference, out var referencedData))
                        {
                            logger.LogWarning("The reference to [{Reference}] for property [owningRelatedElement] on element type [InterfaceUsage] with id [{Id}] was not found in the cache, probably because its type is not supported.",
                                reference, poco.Id);

                            return;
                        }

                        if (referencedData is not SysML2.NET.Core.POCO.Root.Elements.IElement owningRelatedElementReference)
                        {
                            throw new InvalidOperationException($"The referenced element with the id [{reference}] is a {referencedData.GetType().Name}, expected type: an IElement");
                        }

                        poco.OwningRelatedElement = owningRelatedElementReference;
                        return;
                    }

                case "owningRelationship":
                    {
                        if (!xmiDataCache.TryGetData(reference, out var referencedData))
                        {
                            logger.LogWarning("The reference to [{Reference}] for property [owningRelationship] on element type [InterfaceUsage] with id [{Id}] was not found in the cache, probably because its type is not supported.",
                                reference, poco.Id);

                            return;
                        }

                        if (referencedData is not SysML2.NET.Core.POCO.Root.Elements.IRelationship owningRelationshipReference)
                        {
                            throw new InvalidOperationException($"The referenced element with the id [{reference}] is a {referencedData.GetType().Name}, expected type: an IRelationship");
                        }

                        poco.OwningRelationship = owningRelationshipReference;
                        return;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyName), propertyName, "Unknown property name");
            }
        }

        /// <summary>
        /// Resolve and assign multiple references value for a specific property
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceUsage"/> that should have the value of a property to be set</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="references">The collection of identifier values to set</param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/></param>
        /// <param name="logger">The <see cref="ILogger" /> used to produce log statement</param>
        public static void ResolveAndAssignMultipleValueReferences(this SysML2.NET.Core.POCO.Systems.Interfaces.InterfaceUsage poco, string propertyName, IReadOnlyCollection<Guid> references, IXmiDataCache xmiDataCache, ILogger logger)
        {
            if (poco == null)
            {
                throw new ArgumentNullException(nameof(poco));
            }

            if (references == null)
            {
                throw new ArgumentNullException(nameof(references));
            }

            if (xmiDataCache == null)
            {
                throw new ArgumentNullException(nameof(xmiDataCache));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            switch (propertyName)
            {
                case "ownedRelatedElement":
                    {
                        foreach (var reference in references)
                        {
                            if (!xmiDataCache.TryGetData(reference, out var referencedData))
                            {
                                logger.LogWarning("The reference to [{Reference}] for property [ownedRelatedElement] on element type [InterfaceUsage] with id [{Id}] was not found in the cache, probably because its type is not supported.",
                                reference, poco.Id);

                                continue;
                            }

                            if (referencedData is not SysML2.NET.Core.POCO.Root.Elements.IElement ownedRelatedElementReference)
                            {
                                throw new InvalidOperationException($"The referenced element with the id [{reference}] is a {referencedData.GetType().Name}, expected type: an IElement");
                            }

                            poco.OwnedRelatedElement.Add(ownedRelatedElementReference);
                        }

                        return;
                    }

                case "ownedRelationship":
                    {
                        foreach (var reference in references)
                        {
                            if (!xmiDataCache.TryGetData(reference, out var referencedData))
                            {
                                logger.LogWarning("The reference to [{Reference}] for property [ownedRelationship] on element type [InterfaceUsage] with id [{Id}] was not found in the cache, probably because its type is not supported.",
                                reference, poco.Id);

                                continue;
                            }

                            if (referencedData is not SysML2.NET.Core.POCO.Root.Elements.IRelationship ownedRelationshipReference)
                            {
                                throw new InvalidOperationException($"The referenced element with the id [{reference}] is a {referencedData.GetType().Name}, expected type: an IRelationship");
                            }

                            poco.OwnedRelationship.Add(ownedRelationshipReference);
                        }

                        return;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyName), propertyName, "Unknown property name");
            }
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
