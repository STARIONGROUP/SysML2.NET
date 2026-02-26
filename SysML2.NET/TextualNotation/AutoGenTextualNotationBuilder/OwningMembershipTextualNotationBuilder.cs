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
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

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
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAnnotatingMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PackageMember
        /// <para>PackageMember:OwningMembership=MemberPrefix(ownedRelatedElement+=DefinitionElement|ownedRelatedElement=UsageElement)</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPackageMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionMember
        /// <para>DefinitionMember:OwningMembership=MemberPrefixownedRelatedElement+=DefinitionElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossFeatureMember
        /// <para>OwnedCrossFeatureMember:OwningMembership=ownedRelatedElement+=OwnedCrossFeature</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedCrossFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedMultiplicity
        /// <para>OwnedMultiplicity:OwningMembership=ownedRelatedElement+=MultiplicityRange</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedMultiplicity(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MultiplicityExpressionMember
        /// <para>MultiplicityExpressionMember:OwningMembership=ownedRelatedElement+=(LiteralExpression|FeatureReferenceExpression)</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMultiplicityExpressionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyMultiplicityMember
        /// <para>EmptyMultiplicityMember:OwningMembership=ownedRelatedElement+=EmptyMultiplicity</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyMultiplicityMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConjugatedPortDefinitionMember
        /// <para>ConjugatedPortDefinitionMember:OwningMembership=ownedRelatedElement+=ConjugatedPortDefinition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConjugatedPortDefinitionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossMultiplicityMember
        /// <para>OwnedCrossMultiplicityMember:OwningMembership=ownedRelatedElement+=OwnedCrossMultiplicity</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedCrossMultiplicityMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureChainMember
        /// <para>OwnedFeatureChainMember:OwningMembership=ownedRelatedElement+=OwnedFeatureChain</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureChainMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionSuccessionMember
        /// <para>TransitionSuccessionMember:OwningMembership=ownedRelatedElement+=TransitionSuccession</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionSuccessionMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrefixMetadataMember
        /// <para>PrefixMetadataMember:OwningMembership='#'ownedRelatedElement=PrefixMetadataUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrefixMetadataMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("#");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceMember
        /// <para>NamespaceMember:OwningMembership=NonFeatureMember|NamespaceFeatureMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureMember
        /// <para>NonFeatureMember:OwningMembership=MemberPrefixownedRelatedElement+=MemberElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceFeatureMember
        /// <para>NamespaceFeatureMember:OwningMembership=MemberPrefixownedRelatedElement+=FeatureElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureMember
        /// <para>FeatureMember:OwningMembership=TypeFeatureMember|OwnedFeatureMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeFeatureMember
        /// <para>TypeFeatureMember:OwningMembership=MemberPrefix'member'ownedRelatedElement+=FeatureElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeFeatureMember(SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership poco, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            stringBuilder.Append("member ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
