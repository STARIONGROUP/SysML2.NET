// -------------------------------------------------------------------------------------------------
// <copyright file="ILibraryPackage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A LibraryPackage is a Package that is the container for a model library. A LibraryPackage is itself
    /// a library Element as are all Elements that are directly or indirectly contained in it.
    /// </summary>
    public partial interface ILibraryPackage : IPackage
    {
        /// <summary>
        /// Whether this LibraryPackage contains a standard library model. This should only be set to true for
        /// LibraryPackages in the standard Kernel Model Libraries or in normative model libraries for a
        /// language built on KerML.
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: false, isTransient: false, isUnsettable: false, isDerived: false, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        bool IsStandard { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
