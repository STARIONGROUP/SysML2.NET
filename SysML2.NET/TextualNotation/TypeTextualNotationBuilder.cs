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
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// Hand-coded part of the <see cref="TypeTextualNotationBuilder" />
    /// </summary>
    public static partial class TypeTextualNotationBuilder
    {
        /// <summary>
        /// Build the complex DefinitionBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=OccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import rule
        /// </summary>
        /// <param name="poco">The <see cref="IType" /></param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /></param>
        internal static void BuildDefinitionBodyItemInternal(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case IVariantMembership variantMembership:
                        VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case FeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember():
                    {
                        var nextElement = ownedRelationshipCursor.GetNext(1);

                        if (nextElement is FeatureMembership nextFeatureMembership && nextFeatureMembership.IsValidForOccurrenceUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, cursorCache, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildOccurrenceUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, cursorCache, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }
                        else
                        {
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case FeatureMembership featureMembershipForOccurrence when featureMembershipForOccurrence.IsValidForOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildOccurrenceUsageMember(featureMembershipForOccurrence, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case FeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(featureMembershipForNonOccurrence, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }
            }
        }

        /// <summary>
        /// Build the logic for the NonBehaviorBodyItem =ownedRelationship+=Import|ownedRelationship+=AliasMember|ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=StructureUsageMember rule
        /// </summary>
        /// <param name="poco">The <see cref="IType" /></param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /></param>
        internal static void BuildNonBehaviorBodyItemInternal(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IVariantMembership variantMembership:
                        VariantMembershipTextualNotationBuilder.BuildVariantUsageMember(variantMembership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case FeatureMembership featureMembershipForSuccession when featureMembershipForSuccession.IsValidForSourceSuccessionMember():
                    {
                        var nextElement = ownedRelationshipCursor.GetNext(1);

                        if (nextElement is FeatureMembership nextFeatureMembership && nextFeatureMembership.IsValidForStructureUsageMember())
                        {
                            FeatureMembershipTextualNotationBuilder.BuildSourceSuccessionMember(featureMembershipForSuccession, cursorCache, stringBuilder);
                            ownedRelationshipCursor.Move();
                            FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember((IFeatureMembership)ownedRelationshipCursor.Current, cursorCache, stringBuilder);
                            ownedRelationshipCursor.Move();
                        }
                        else
                        {
                            ownedRelationshipCursor.Move();
                        }

                        break;
                    }

                    case FeatureMembership featureMembershipForStructure when featureMembershipForStructure.IsValidForStructureUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildStructureUsageMember(featureMembershipForStructure, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case FeatureMembership featureMembershipForNonOccurrence when featureMembershipForNonOccurrence.IsValidForNonOccurrenceUsageMember():
                        FeatureMembershipTextualNotationBuilder.BuildNonOccurrenceUsageMember(featureMembershipForNonOccurrence, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildDefinitionMember(owningMembership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    case IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;

                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyItem
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildActionBodyItemHandCoded(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildActionBodyItemHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBody
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildCalculationBodyHandCoded(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildCalculationBodyHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionBodyItem
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildDefinitionBodyItemHandCoded(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildDefinitionBodyItemHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionBody
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFunctionBodyHandCoded(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildFunctionBodyHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBodyItem
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildInterfaceBodyItemHandCoded(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildInterfaceBodyItemHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateBodyItem
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildStateBodyItemHandCoded(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildStateBodyItemHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeRelationshipPart
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildTypeRelationshipPartHandCoded(IType poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildTypeRelationshipPartHandCoded requires manual implementation");
        }
    }
}
