// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions
{
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Auto-generated companion to the hand-coded <see cref="ElementExtensions"/> partial class.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Determines whether <paramref name="target"/> is a structurally valid element to be added as an
        /// <see cref="IRelationship.OwnedRelatedElement"/> of <paramref name="bridgeRelationship"/>, based on
        /// the typed composite property declared by the bridge's metaclass in the KerML/SysML2 metamodel.
        /// </summary>
        /// <param name="bridgeRelationship">The bridge <see cref="IRelationship"/>.</param>
        /// <param name="target">The candidate <see cref="IElement"/>.</param>
        /// <returns><c>true</c> if <paramref name="target"/>'s runtime type satisfies the constraint;
        /// otherwise <c>false</c>.</returns>
        public static bool QueryIsValidForContainment(this IRelationship bridgeRelationship, IElement target)
        {
            return bridgeRelationship switch
            {
                SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership => target is SysML2.NET.Core.POCO.Systems.Parts.IPartUsage,
                SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership => target is SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage,
                SysML2.NET.Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership => target is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage,
                SysML2.NET.Core.POCO.Systems.Requirements.IStakeholderMembership => target is SysML2.NET.Core.POCO.Systems.Parts.IPartUsage,
                SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership => target is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage,
                SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership => target is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Systems.Cases.IObjectiveMembership => target is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage,
                SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership => target is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership => target is SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage,
                SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership => target is SysML2.NET.Core.POCO.Kernel.Functions.IExpression,
                SysML2.NET.Core.POCO.Systems.States.IStateSubactionMembership => target is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage,
                SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership => target is SysML2.NET.Core.POCO.Kernel.Behaviors.IStep,
                SysML2.NET.Core.POCO.Systems.Views.IViewRenderingMembership => target is SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage,
                SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership => target is SysML2.NET.Core.POCO.Kernel.Functions.IExpression,
                SysML2.NET.Core.POCO.Core.Types.IFeatureMembership => target is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue => target is SysML2.NET.Core.POCO.Kernel.Functions.IExpression,
                SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IVariantMembership => target is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage,
                SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership => target is not null,
                SysML2.NET.Core.POCO.Root.Annotations.IAnnotation => target is SysML2.NET.Core.POCO.Root.Annotations.IAnnotatingElement,
                SysML2.NET.Core.POCO.Root.Elements.IRelationship => target is not null,
                _ => true,
            };
        }

        /// <summary>
        /// Returns the simple C# interface name of the expected target type for the supplied
        /// <paramref name="bridgeRelationship"/>, used to compose the error message thrown by
        /// <c>AssignOwnership</c> when the target is not a valid containment target.
        /// </summary>
        /// <param name="bridgeRelationship">The bridge <see cref="IRelationship"/>.</param>
        /// <returns>The expected target type's simple interface name.</returns>
        public static string ExpectedContainmentTypeName(this IRelationship bridgeRelationship)
        {
            return bridgeRelationship switch
            {
                SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership => nameof(SysML2.NET.Core.POCO.Systems.Parts.IPartUsage),
                SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership => nameof(SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage),
                SysML2.NET.Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership => nameof(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage),
                SysML2.NET.Core.POCO.Systems.Requirements.IStakeholderMembership => nameof(SysML2.NET.Core.POCO.Systems.Parts.IPartUsage),
                SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership => nameof(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage),
                SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Systems.Cases.IObjectiveMembership => nameof(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage),
                SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership => nameof(SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage),
                SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership => nameof(SysML2.NET.Core.POCO.Kernel.Functions.IExpression),
                SysML2.NET.Core.POCO.Systems.States.IStateSubactionMembership => nameof(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage),
                SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership => nameof(SysML2.NET.Core.POCO.Kernel.Behaviors.IStep),
                SysML2.NET.Core.POCO.Systems.Views.IViewRenderingMembership => nameof(SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage),
                SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership => nameof(SysML2.NET.Core.POCO.Kernel.Functions.IExpression),
                SysML2.NET.Core.POCO.Core.Types.IFeatureMembership => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue => nameof(SysML2.NET.Core.POCO.Kernel.Functions.IExpression),
                SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IVariantMembership => nameof(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage),
                SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership => nameof(SysML2.NET.Core.POCO.Root.Elements.IElement),
                SysML2.NET.Core.POCO.Root.Annotations.IAnnotation => nameof(SysML2.NET.Core.POCO.Root.Annotations.IAnnotatingElement),
                SysML2.NET.Core.POCO.Root.Elements.IRelationship => nameof(SysML2.NET.Core.POCO.Root.Elements.IElement),
                _ => nameof(IElement),
            };
        }

        /// <summary>
        /// Determines whether <paramref name="source"/> is a structurally valid containment owner for
        /// <paramref name="bridgeRelationship"/>, based on the typed property declared by the bridge's
        /// metaclass that subsets <see cref="IRelationship.OwningRelatedElement"/> in the KerML/SysML2
        /// metamodel (e.g. an <c>IFeatureMembership</c> requires an <c>IType</c> owner).
        /// </summary>
        /// <param name="bridgeRelationship">The bridge <see cref="IRelationship"/>.</param>
        /// <param name="source">The candidate owner <see cref="IElement"/>.</param>
        /// <returns><c>true</c> if <paramref name="source"/>'s runtime type satisfies the owner-side
        /// constraint; otherwise <c>false</c>.</returns>
        public static bool QueryIsValidContainmentOwner(this IRelationship bridgeRelationship, IElement source)
        {
            return bridgeRelationship switch
            {
                SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership => source is SysML2.NET.Core.POCO.Systems.Parts.IPartUsage,
                SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership => source is SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage,
                SysML2.NET.Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership => source is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage,
                SysML2.NET.Core.POCO.Systems.Requirements.IStakeholderMembership => source is SysML2.NET.Core.POCO.Systems.Parts.IPartUsage,
                SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership => source is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage,
                SysML2.NET.Core.POCO.Kernel.Associations.IAssociation => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Kernel.Connectors.IConnector => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Systems.Cases.IObjectiveMembership => source is SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage,
                SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership => source is SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage,
                SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership => source is SysML2.NET.Core.POCO.Kernel.Functions.IExpression,
                SysML2.NET.Core.POCO.Systems.States.IStateSubactionMembership => source is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage,
                SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership => source is SysML2.NET.Core.POCO.Kernel.Behaviors.IStep,
                SysML2.NET.Core.POCO.Systems.Views.IViewRenderingMembership => source is SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage,
                SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortTyping => source is SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortDefinition,
                SysML2.NET.Core.POCO.Core.Features.ICrossSubsetting => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership => source is SysML2.NET.Core.POCO.Kernel.Functions.IExpression,
                SysML2.NET.Core.POCO.Core.Types.IFeatureMembership => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue => source is SysML2.NET.Core.POCO.Kernel.Functions.IExpression,
                SysML2.NET.Core.POCO.Core.Features.IRedefinition => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IVariantMembership => source is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage,
                SysML2.NET.Core.POCO.Core.Features.IFeatureTyping => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Root.Namespaces.IMembershipImport => source is SysML2.NET.Core.POCO.Root.Namespaces.IMembership,
                SysML2.NET.Core.POCO.Root.Namespaces.INamespaceImport => source is SysML2.NET.Core.POCO.Root.Namespaces.INamespace,
                SysML2.NET.Core.POCO.Systems.Ports.IPortConjugation => source is SysML2.NET.Core.POCO.Systems.Ports.IPortDefinition,
                SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification => source is SysML2.NET.Core.POCO.Core.Classifiers.IClassifier,
                SysML2.NET.Core.POCO.Core.Features.ISubsetting => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Core.Types.IConjugation => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Core.Types.IDifferencing => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Core.Types.IDisjoining => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Core.Features.IFeatureChaining => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Core.Features.IFeatureInverting => source is SysML2.NET.Core.POCO.Core.Features.IFeature,
                SysML2.NET.Core.POCO.Core.Types.IIntersecting => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Core.Types.ISpecialization => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing => source is SysML2.NET.Core.POCO.Core.Types.IType,
                SysML2.NET.Core.POCO.Core.Types.IUnioning => source is SysML2.NET.Core.POCO.Core.Types.IType,
                _ => true,
            };
        }

        /// <summary>
        /// Returns the simple C# interface name of the expected owner type for the supplied
        /// <paramref name="bridgeRelationship"/>, used to compose the error message thrown by
        /// <c>AssignOwnership</c> when the source is not a valid containment owner.
        /// </summary>
        /// <param name="bridgeRelationship">The bridge <see cref="IRelationship"/>.</param>
        /// <returns>The expected owner type's simple interface name.</returns>
        public static string ExpectedContainmentOwnerTypeName(this IRelationship bridgeRelationship)
        {
            return bridgeRelationship switch
            {
                SysML2.NET.Core.POCO.Systems.Requirements.IActorMembership => nameof(SysML2.NET.Core.POCO.Systems.Parts.IPartUsage),
                SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership => nameof(SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage),
                SysML2.NET.Core.POCO.Systems.VerificationCases.IRequirementVerificationMembership => nameof(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage),
                SysML2.NET.Core.POCO.Systems.Requirements.IStakeholderMembership => nameof(SysML2.NET.Core.POCO.Systems.Parts.IPartUsage),
                SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership => nameof(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage),
                SysML2.NET.Core.POCO.Kernel.Associations.IAssociation => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Kernel.Connectors.IConnector => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Core.Features.IEndFeatureMembership => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Systems.Cases.IObjectiveMembership => nameof(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementUsage),
                SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership => nameof(SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage),
                SysML2.NET.Core.POCO.Kernel.Functions.IResultExpressionMembership => nameof(SysML2.NET.Core.POCO.Kernel.Functions.IExpression),
                SysML2.NET.Core.POCO.Systems.States.IStateSubactionMembership => nameof(SysML2.NET.Core.POCO.Systems.Actions.IActionUsage),
                SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership => nameof(SysML2.NET.Core.POCO.Kernel.Behaviors.IStep),
                SysML2.NET.Core.POCO.Systems.Views.IViewRenderingMembership => nameof(SysML2.NET.Core.POCO.Systems.Views.IRenderingUsage),
                SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortTyping => nameof(SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortDefinition),
                SysML2.NET.Core.POCO.Core.Features.ICrossSubsetting => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership => nameof(SysML2.NET.Core.POCO.Kernel.Functions.IExpression),
                SysML2.NET.Core.POCO.Core.Types.IFeatureMembership => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue => nameof(SysML2.NET.Core.POCO.Kernel.Functions.IExpression),
                SysML2.NET.Core.POCO.Core.Features.IRedefinition => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IVariantMembership => nameof(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage),
                SysML2.NET.Core.POCO.Core.Features.IFeatureTyping => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Root.Namespaces.IMembershipImport => nameof(SysML2.NET.Core.POCO.Root.Namespaces.IMembership),
                SysML2.NET.Core.POCO.Root.Namespaces.INamespaceImport => nameof(SysML2.NET.Core.POCO.Root.Namespaces.INamespace),
                SysML2.NET.Core.POCO.Systems.Ports.IPortConjugation => nameof(SysML2.NET.Core.POCO.Systems.Ports.IPortDefinition),
                SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification => nameof(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier),
                SysML2.NET.Core.POCO.Core.Features.ISubsetting => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Core.Types.IConjugation => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Core.Types.IDifferencing => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Core.Types.IDisjoining => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Core.Features.IFeatureChaining => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Core.Features.IFeatureInverting => nameof(SysML2.NET.Core.POCO.Core.Features.IFeature),
                SysML2.NET.Core.POCO.Core.Types.IIntersecting => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Core.Types.ISpecialization => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                SysML2.NET.Core.POCO.Core.Types.IUnioning => nameof(SysML2.NET.Core.POCO.Core.Types.IType),
                _ => nameof(IElement),
            };
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
