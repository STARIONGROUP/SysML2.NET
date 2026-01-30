// -------------------------------------------------------------------------------------------------
// <copyright file="IMultiplicity.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Core.Types
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Multiplicity is a Feature whose co-domain is a set of natural numbers giving the allowed
    /// cardinalities of each typeWithMultiplicity. The cardinality of a Type is defined as follows,
    /// depending on whether the Type is a Classifier or Feature. <ul> <li>Classifier – The number of basic
    /// instances of the Classifier, that is, those instances representing things, which are not instances
    /// of any subtypes of the Classifier that are Features. <li>Features – The number of instances with the
    /// same featuring instances. In the case of a Feature with a Classifier as its featuringType, this is
    /// the number of values of Feature for each basic instance of the Classifier. Note that, for non-unique
    /// Features, all duplicate values are included in this count.</li> </ul>  Multiplicity co-domains (in
    /// models) can be specified by Expression that might vary in their results. If the typeWithMultiplicity
    /// is a Classifier, the domain of the Multiplicity shall be Base::Anything.  If the
    /// typeWithMultiplicity is a Feature,  the Multiplicity shall have the same domain as the
    /// typeWithMultiplicity.
    /// </summary>
    [Class(xmiId: "Core-Types-Multiplicity", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IMultiplicity : IFeature
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
