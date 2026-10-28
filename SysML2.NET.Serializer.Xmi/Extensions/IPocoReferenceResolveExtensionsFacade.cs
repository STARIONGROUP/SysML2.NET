// ------------------------------------------------------------------------------------------------
// <copyright file="IPocoReferenceResolveExtensionsFacade.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Extensions
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// The <see cref="IPocoReferenceResolveExtensionsFacade"/> provides access to extensions method for POCO <see cref="IData"/> to resolve reference
    /// </summary>
    public interface IPocoReferenceResolveExtensionsFacade
    {
        /// <summary>
        /// Resolve and assign single reference value for a specific property
        /// </summary>
        /// <param name="data">The <see cref="IData"/> that should have the value of a property to be set</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="reference">The identifier of the <see cref="IData"/> value to set</param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/></param>
        /// <param name="logger">The <see cref="ILogger" /> used to produce log statement</param>
        void ResolveAndAssignSingleValueReference(IData data, string propertyName, Guid reference, IXmiDataCache xmiDataCache, ILogger logger);
        
        /// <summary>
        /// Resolve and assign multiple references value for a specific property
        /// </summary>
        /// <param name="data">The <see cref="IData"/> that should have the value of a property to be set</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="references">The collection of identifier values to set</param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/></param>
        /// <param name="logger">The <see cref="ILogger" /> used to produce log statement</param>
        void ResolveAndAssignMultipleValueReferences(IData data, string propertyName, IReadOnlyCollection<Guid> references, IXmiDataCache xmiDataCache, ILogger logger);
    }
}
