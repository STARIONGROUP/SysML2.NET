// -------------------------------------------------------------------------------------------------
// <copyright file="OwningMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="OwningMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> element
    /// </summary>
    public static partial class OwningMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule AnnotatingMember
        /// <para>AnnotatingMember:OwningMembership=ownedRelatedElement+=AnnotatingElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildAnnotatingMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Root.Annotations.IAnnotatingElement elementAsAnnotatingElement)
                {
                    AnnotatingElementTextualNotationBuilder.BuildAnnotatingElement(elementAsAnnotatingElement, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PackageMember
        /// <para>PackageMember:OwningMembership=MemberPrefix(ownedRelatedElement+=DefinitionElement|ownedRelatedElement=UsageElement)</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="cursorCache"></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPackageMember(IOwningMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            var cursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            switch (cursor.Current)
            {
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage usage:
                    UsageTextualNotationBuilder.BuildUsageElement(usage, cursorCache, stringBuilder);
                    break;
                case { } element:
                    ElementTextualNotationBuilder.BuildDefinitionElement(element, cursorCache, stringBuilder);
                    cursor.Move();
                    break;            
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionMember
        /// <para>DefinitionMember:OwningMembership=MemberPrefixownedRelatedElement+=DefinitionElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementIterator = poco.OwnedRelatedElement.GetEnumerator();
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            ownedRelatedElementIterator.MoveNext();

            if (ownedRelatedElementIterator.Current != null)
            {
                ElementTextualNotationBuilder.BuildDefinitionElement(ownedRelatedElementIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossFeatureMember
        /// <para>OwnedCrossFeatureMember:OwningMembership=ownedRelatedElement+=OwnedCrossFeature</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildOwnedCrossFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildOwnedCrossFeature(elementAsReferenceUsage, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedMultiplicity
        /// <para>OwnedMultiplicity:OwningMembership=ownedRelatedElement+=MultiplicityRange</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildOwnedMultiplicity(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Kernel.Multiplicities.IMultiplicityRange elementAsMultiplicityRange)
                {
                    MultiplicityRangeTextualNotationBuilder.BuildMultiplicityRange(elementAsMultiplicityRange, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MultiplicityExpressionMember
        /// <para>MultiplicityExpressionMember:OwningMembership=ownedRelatedElement+=(LiteralExpression|FeatureReferenceExpression)</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildMultiplicityExpressionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];
            switch (elementForOwnedRelatedElement)
            {
                case SysML2.NET.Core.POCO.Kernel.Expressions.LiteralExpression pocoLiteralExpression:

                    if (pocoLiteralExpression is SysML2.NET.Core.POCO.Kernel.Expressions.ILiteralExpression elementAsLiteralExpression)
                    {
                        LiteralExpressionTextualNotationBuilder.BuildLiteralExpression(elementAsLiteralExpression, stringBuilder);
                    }
                    break;
                case SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression pocoFeatureReferenceExpression:

                    if (pocoFeatureReferenceExpression is SysML2.NET.Core.POCO.Kernel.Expressions.IFeatureReferenceExpression elementAsFeatureReferenceExpression)
                    {
                        FeatureReferenceExpressionTextualNotationBuilder.BuildFeatureReferenceExpression(elementAsFeatureReferenceExpression, stringBuilder);
                    }
                    break;
            }


            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyMultiplicityMember
        /// <para>EmptyMultiplicityMember:OwningMembership=ownedRelatedElement+=EmptyMultiplicity</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildEmptyMultiplicityMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Core.Types.IMultiplicity elementAsMultiplicity)
                {
                    MultiplicityTextualNotationBuilder.BuildEmptyMultiplicity(elementAsMultiplicity, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConjugatedPortDefinitionMember
        /// <para>ConjugatedPortDefinitionMember:OwningMembership=ownedRelatedElement+=ConjugatedPortDefinition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildConjugatedPortDefinitionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Systems.Ports.IConjugatedPortDefinition elementAsConjugatedPortDefinition)
                {
                    ConjugatedPortDefinitionTextualNotationBuilder.BuildConjugatedPortDefinition(elementAsConjugatedPortDefinition, 0, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossMultiplicityMember
        /// <para>OwnedCrossMultiplicityMember:OwningMembership=ownedRelatedElement+=OwnedCrossMultiplicity</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildOwnedCrossMultiplicityMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildOwnedCrossMultiplicity(elementAsFeature, 0, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureChainMember
        /// <para>OwnedFeatureChainMember:OwningMembership=ownedRelatedElement+=OwnedFeatureChain</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildOwnedFeatureChainMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildOwnedFeatureChain(elementAsFeature, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionSuccessionMember
        /// <para>TransitionSuccessionMember:OwningMembership=ownedRelatedElement+=TransitionSuccession</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildTransitionSuccessionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession elementAsSuccession)
                {
                    SuccessionTextualNotationBuilder.BuildTransitionSuccession(elementAsSuccession, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrefixMetadataMember
        /// <para>PrefixMetadataMember:OwningMembership='#'ownedRelatedElement=PrefixMetadataUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrefixMetadataMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfMetadataUsageIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.Metadata.MetadataUsage>().GetEnumerator();
            stringBuilder.Append("#");
            ownedRelatedElementOfMetadataUsageIterator.MoveNext();

            if (ownedRelatedElementOfMetadataUsageIterator.Current != null)
            {
                MetadataUsageTextualNotationBuilder.BuildPrefixMetadataUsage(ownedRelatedElementOfMetadataUsageIterator.Current, 0, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceMember
        /// <para>NamespaceMember:OwningMembership=NonFeatureMember|NamespaceFeatureMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureMember
        /// <para>NonFeatureMember:OwningMembership=MemberPrefixownedRelatedElement+=MemberElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementIterator = poco.OwnedRelatedElement.GetEnumerator();
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            ownedRelatedElementIterator.MoveNext();

            if (ownedRelatedElementIterator.Current != null)
            {
                ElementTextualNotationBuilder.BuildMemberElement(ownedRelatedElementIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceFeatureMember
        /// <para>NamespaceFeatureMember:OwningMembership=MemberPrefixownedRelatedElement+=FeatureElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfFeatureIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.Feature>().GetEnumerator();
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            ownedRelatedElementOfFeatureIterator.MoveNext();

            if (ownedRelatedElementOfFeatureIterator.Current != null)
            {
                FeatureTextualNotationBuilder.BuildFeatureElement(ownedRelatedElementOfFeatureIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureMember
        /// <para>FeatureMember:OwningMembership=TypeFeatureMember|OwnedFeatureMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Types.FeatureMembership pocoFeatureMembership:
                    FeatureMembershipTextualNotationBuilder.BuildOwnedFeatureMember(pocoFeatureMembership, stringBuilder);
                    break;
                default:
                    BuildTypeFeatureMember(poco, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeFeatureMember
        /// <para>TypeFeatureMember:OwningMembership=MemberPrefix'member'ownedRelatedElement+=FeatureElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfFeatureIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.Feature>().GetEnumerator();
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            stringBuilder.Append("member ");
            ownedRelatedElementOfFeatureIterator.MoveNext();

            if (ownedRelatedElementOfFeatureIterator.Current != null)
            {
                FeatureTextualNotationBuilder.BuildFeatureElement(ownedRelatedElementOfFeatureIterator.Current, stringBuilder);
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
