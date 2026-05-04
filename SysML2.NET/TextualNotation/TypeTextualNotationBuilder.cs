// -------------------------------------------------------------------------------------------------
// <copyright file="TypeTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Hand-coded part of the <see cref="TypeTextualNotationBuilder" />
    /// </summary>
    public static partial class TypeTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyItem
        /// <remarks>ActionBodyItem:Type=NonBehaviorBodyItem|ownedRelationship+=InitialNodeMember(ownedRelationship+=ActionTargetSuccessionMember)*|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=ActionBehaviorMember(ownedRelationship+=ActionTargetSuccessionMember)*|ownedRelationship+=GuardedSuccessionMember</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildActionBodyItemHandCoded(IType poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    // Action-specific cases
                    case IFeatureMembership featureMembershipForInitialNode when featureMembershipForInitialNode.IsValidForInitialNodeMember():
                    {
                        FeatureMembershipTextualNotationBuilder.BuildInitialNodeMember(featureMembershipForInitialNode, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                        while (ownedRelationshipCursor.Current is IFeatureMembership targetSuccession && targetSuccession.IsValidForActionTargetSuccessionMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildActionTargetSuccessionMember(targetSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case IFeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember():
                    {
                        var nextElement = ownedRelationshipCursor.GetNext(1);

                        if (nextElement is IFeatureMembership nextForActionBehavior && nextForActionBehavior.IsValidForActionBehaviorMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildActionBehaviorMember((IFeatureMembership)ownedRelationshipCursor.Current, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();

                            while (ownedRelationshipCursor.Current is IFeatureMembership targetSuccession && targetSuccession.IsValidForActionTargetSuccessionMember())
                            {
                                FeatureMembershipTextualNotationBuilder.BuildActionTargetSuccessionMember(targetSuccession, writerContext, stringBuilder);
                                ownedRelationshipCursor.Move();
                            }
                        }
                        else if (nextElement is IFeatureMembership nextForStructure && nextForStructure.IsValidForStructureUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }
                        else
                        {
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case IFeatureMembership featureMembershipForActionBehavior when featureMembershipForActionBehavior.IsValidForActionBehaviorMember():
                    {
                        FeatureMembershipTextualNotationBuilder.BuildActionBehaviorMember(featureMembershipForActionBehavior, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                        while (ownedRelationshipCursor.Current is IFeatureMembership targetSuccession && targetSuccession.IsValidForActionTargetSuccessionMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildActionTargetSuccessionMember(targetSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case IFeatureMembership featureMembershipForGuarded when featureMembershipForGuarded.IsValidForGuardedSuccessionMember():
                        FeatureMembershipTextualNotationBuilder.BuildGuardedSuccessionMember(featureMembershipForGuarded, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    // NonBehaviorBodyItem cases
                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IVariantMembership variantMembership:
                        VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForStructure when featureMembershipForStructure.IsValidForStructureUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember(featureMembershipForStructure, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(featureMembershipForNonOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionBodyItem
        /// <remarks>DefinitionBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=OccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildDefinitionBodyItemHandCoded(IType poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case IVariantMembership variantMembership:
                        VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember():
                    {
                        var nextElement = ownedRelationshipCursor.GetNext(1);

                        if (nextElement is IFeatureMembership nextFeatureMembership && nextFeatureMembership.IsValidForOccurrenceUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildOccurrenceUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }
                        else
                        {
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case IFeatureMembership featureMembershipForOccurrence when featureMembershipForOccurrence.IsValidForOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildOccurrenceUsageMember(featureMembershipForOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(featureMembershipForNonOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeDeclaration
        /// <remarks>TypeDeclaration:Type=(isSufficient?='all')?Identification(ownedRelationship+=OwnedMultiplicity)?(SpecializationPart|ConjugationPart)+TypeRelationshipPart*</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildTypeDeclarationHandCoded(IType poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            if (poco.IsSufficient)
            {
                stringBuilder.Append("all ");
            }

            ElementTextualNotationBuilder.BuildIdentification(poco, writerContext, stringBuilder);

            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            // Optional OwnedMultiplicity: single += consumption if the current ownedRelationship element
            // is an OwningMembership containing an IMultiplicity (OwnedMultiplicity:OwningMembership).
            if (ownedRelationshipCursor.Current is IOwningMembership multiplicityMember
                && multiplicityMember.OwnedRelatedElement.OfType<IMultiplicity>().Any())
            {
                OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(multiplicityMember, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }

            // (SpecializationPart | ConjugationPart)+ — delegate to the existing Part builders.
            // OwnedSpecialization:Specialization and OwnedConjugation:Conjugation — cursor elements ARE
            // ISpecialization / IConjugation directly (not wrapped in IOwningMembership).
            // The Part builders now include a type-guarded trailing while loop, so interleaved
            // Specializations/Conjugations are safely handled by the outer while here.
            while (ownedRelationshipCursor.Current is ISpecialization || ownedRelationshipCursor.Current is IConjugation)
            {
                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildSpecializationPart(poco, writerContext, stringBuilder);
                }
                else
                {
                    BuildConjugationPart(poco, writerContext, stringBuilder);
                }
            }

            // TypeRelationshipPart* — zero or more. Dispatch directly on concrete cursor types because
            // BuildTypeRelationshipPart uses IsValidFor* guards that currently throw NotSupportedException.
            while (ownedRelationshipCursor.Current is IDisjoining
                   || ownedRelationshipCursor.Current is IUnioning
                   || ownedRelationshipCursor.Current is IIntersecting
                   || ownedRelationshipCursor.Current is IDifferencing)
            {
                if (ownedRelationshipCursor.Current is IDisjoining)
                {
                    BuildDisjoiningPart(poco, writerContext, stringBuilder);
                }
                else if (ownedRelationshipCursor.Current is IUnioning)
                {
                    BuildUnioningPart(poco, writerContext, stringBuilder);
                }
                else if (ownedRelationshipCursor.Current is IIntersecting)
                {
                    BuildIntersectingPart(poco, writerContext, stringBuilder);
                }
                else
                {
                    BuildDifferencingPart(poco, writerContext, stringBuilder);
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBodyItem
        /// <remarks>InterfaceBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=InterfaceNonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=InterfaceOccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildInterfaceBodyItemHandCoded(IType poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case IVariantMembership variantMembership:
                        VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember():
                    {
                        var nextElement = ownedRelationshipCursor.GetNext(1);

                        if (nextElement is IFeatureMembership nextFeatureMembership && nextFeatureMembership.IsValidForOccurrenceUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildInterfaceOccurrenceUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }
                        else
                        {
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case IFeatureMembership featureMembershipForOccurrence when featureMembershipForOccurrence.IsValidForOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildInterfaceOccurrenceUsageMember(featureMembershipForOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildInterfaceNonOccurrenceUsageMember(featureMembershipForNonOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateBodyItem
        /// <remarks>StateBodyItem:Type=NonBehaviorBodyItem|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=BehaviorUsageMember(ownedRelationship+=TargetTransitionUsageMember)*|ownedRelationship+=TransitionUsageMember|ownedRelationship+=EntryActionMember(ownedRelationship+=EntryTransitionMember)*|ownedRelationship+=DoActionMember|ownedRelationship+=ExitActionMember</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// StateBodyItem : Type =
        ///     NonBehaviorBodyItem
        ///   | ( ownedRelationship += SourceSuccessionMember )?
        ///     ownedRelationship += BehaviorUsageMember
        ///     ( ownedRelationship += TargetTransitionUsageMember )*
        ///   | ownedRelationship += TransitionUsageMember
        ///   | ownedRelationship += EntryActionMember
        ///     ( ownedRelationship += EntryTransitionMember )*
        ///   | ownedRelationship += DoActionMember
        ///   | ownedRelationship += ExitActionMember
        /// </remarks>
        private static void BuildStateBodyItemHandCoded(IType poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    // State-specific cases: Entry/Do/Exit action members
                    case IStateSubactionMembership { Kind: SysML2.NET.Core.Systems.States.StateSubactionKind.Entry } entryActionMember:
                    {
                        StateSubactionMembershipTextualNotationBuilder.BuildEntryActionMember(entryActionMember, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                        while (ownedRelationshipCursor.Current is IFeatureMembership entryTransition && entryTransition.IsValidForEntryTransitionMemberRule())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildEntryTransitionMember(entryTransition, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case IStateSubactionMembership { Kind: SysML2.NET.Core.Systems.States.StateSubactionKind.Do } doActionMember:
                        StateSubactionMembershipTextualNotationBuilder.BuildDoActionMember(doActionMember, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IStateSubactionMembership { Kind: SysML2.NET.Core.Systems.States.StateSubactionKind.Exit } exitActionMember:
                        StateSubactionMembershipTextualNotationBuilder.BuildExitActionMember(exitActionMember, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    // SourceSuccessionMember? + BehaviorUsageMember + TargetTransitionUsageMember*
                    case IFeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember():
                    {
                        var nextElement = ownedRelationshipCursor.GetNext(1);

                        if (nextElement is IFeatureMembership nextForBehavior && nextForBehavior.IsValidForBehaviorUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildBehaviorUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();

                            while (ownedRelationshipCursor.Current is IFeatureMembership targetTransition && targetTransition.IsValidForTargetTransitionUsageMember())
                            {
                                FeatureMembershipTextualNotationBuilder.BuildTargetTransitionUsageMember(targetTransition, writerContext, stringBuilder);
                                ownedRelationshipCursor.Move();
                            }
                        }
                        else if (nextElement is IFeatureMembership nextForStructure && nextForStructure.IsValidForStructureUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }
                        else
                        {
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    // BehaviorUsageMember without preceding SourceSuccessionMember
                    case IFeatureMembership featureMembershipForBehavior when featureMembershipForBehavior.IsValidForBehaviorUsageMember():
                    {
                        FeatureMembershipTextualNotationBuilder.BuildBehaviorUsageMember(featureMembershipForBehavior, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();

                        while (ownedRelationshipCursor.Current is IFeatureMembership targetTransition && targetTransition.IsValidForTargetTransitionUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildTargetTransitionUsageMember(targetTransition, writerContext, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    // TransitionUsageMember
                    case IFeatureMembership featureMembershipForTransition when featureMembershipForTransition.IsValidForTransitionUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildTransitionUsageMember(featureMembershipForTransition, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    // NonBehaviorBodyItem cases
                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IVariantMembership variantMembership:
                        VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForStructure when featureMembershipForStructure.IsValidForStructureUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember(featureMembershipForStructure, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IFeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(featureMembershipForNonOccurrence, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, writerContext, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }
            }
        }

    }
}
