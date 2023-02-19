// -------------------------------------------------------------------------------------------------
// <copyright file="IMetadataFeature.cs" company="RHEA System S.A.">
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
    /// A MetadataFeature is a Feature that is an AnnotatingElement used to annotate another Element with
    /// metadata. It is typed by a Metaclass. All its ownedFeatures must redefine features of its metaclass
    /// and any feature bindings must be model-level evaluable.A MetadataFeature must subset, directly or
    /// indirectly, the base MetadataFeature metadata from the Kernel
    /// Library.specializesFromLibrary("Metaobjects::metaobjects")isSemantic() implies    let annotatedTypes
    /// : Sequence(Type) =         annotatedElement->selectAsKind(Type) in    let baseTypes :
    /// Sequence(MetadataFeature) =         evaluateFeature(resolveGlobal(           
    /// 'Metaobjects::SemanticMetadata::baseType').            oclAsType(Feature))->       
    /// selectAsKind(MetadataFeature) in    annotatedTypes->notEmpty() and     baseTypes()->notEmpty() and  
    ///   baseTypes()->first().isSyntactic() implies        let annotatedType : Type =
    /// annotatedTypes->first() in        let baseType : Element = baseTypes->first().syntaxElement() in    
    ///    if annotatedType.oclIsKindOf(Classifier) and             baseType.oclIsKindOf(Feature) then      
    ///      baseType.oclAsType(Feature).type->                forAll(t | annotatedType.specializes(t))     
    ///   else if baseType.oclIsKindOf(Type) then           
    /// annotatedType.specializes(baseType.oclAsType(Type))        else            true        endif
    /// </summary>
    public partial interface IMetadataFeature : IFeature, IAnnotatingElement
    {
        /// <summary>
        /// Queries the derived property Metaclass
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Metaclass QueryMetaclass();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
