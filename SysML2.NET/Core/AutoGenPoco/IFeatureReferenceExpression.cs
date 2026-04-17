// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureReferenceExpression.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A FeatureReferenceExpression is an Expression whose result is bound to a referent Feature.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651680_105632_42151", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureReferenceExpression : IExpression
    {
        /// <summary>
        /// The Feature that is referenced by this FeatureReferenceExpression, which is its first non-parameter
        /// member.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674962_848357_43185", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674979_644335_43267")]
        IFeature referent { get; }

        /// <summary>
        /// A FeatureReferenceExpression is model-level evaluable if it&#39;s referent                          
        ///  <ul>                            <li>conforms to the self-reference feature Anything::self;</li>    
        ///                        <li>is an Expression that is model-level evaluable;</li>                     
        ///       <li>has an owningType that is a Metaclass or MetadataFeature; or</li>                         
        ///   <li>has no featuringTypes and, if it has a FeatureValue, the value Expression is model-level
        /// evaluable.</li>                            </ul>
        /// </summary>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        new bool ModelLevelEvaluable(List<IFeature> visited);

        /// <summary>
        /// First, determine a value Expression for the referent:                            <ul>               
        ///             <li>If the target Element is a Type that has a feature that is the referent or (directly
        /// or indirectly) redefines it, then the value Expression of the FeatureValue for that feature (if
        /// any).</li>                            <li>Else, if the referent has no featuringTypes, the value
        /// Expression of the FeatureValue for the referent (if any).</li>                            </ul>     
        ///                       Then:                            <ul>                            <li>If such a
        /// value Expression exists, return the result of evaluating that Expression on the target.</li>        
        ///                    <li>Else, if the referent is not an Expression, return the referent.</li>        
        /// <li>Else return the empty sequence.</li>                            </ul>
        /// </summary>
        /// <param name="target">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IElement" />
        /// </returns>
        new List<IElement> Evaluate(IElement target);
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
