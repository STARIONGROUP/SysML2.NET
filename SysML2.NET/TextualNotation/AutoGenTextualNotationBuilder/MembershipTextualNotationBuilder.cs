// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="MembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> element
    /// </summary>
    public static partial class MembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule MemberPrefix
        /// <para>MemberPrefix:Membership=(visibility=VisibilityIndicator)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMemberPrefix(SysML2.NET.Core.POCO.Root.Namespaces.IMembership poco, StringBuilder stringBuilder)
        {
            if (poco.Visibility != SysML2.NET.Core.Root.Namespaces.VisibilityKind.Public)
            {
                stringBuilder.Append(poco.Visibility.ToString().ToLower());
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AliasMember
        /// <para>AliasMember:Membership=MemberPrefix'alias'('&lt;'memberShortName=NAME'&gt;')?(memberName=NAME)?'for'memberElement=[QualifiedName]RelationshipBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAliasMember(SysML2.NET.Core.POCO.Root.Namespaces.IMembership poco, StringBuilder stringBuilder)
        {
            BuildMemberPrefix(poco, stringBuilder);
            stringBuilder.Append("alias ");
            if (!string.IsNullOrWhiteSpace(poco.MemberShortName))
            {
                stringBuilder.Append("<");
                stringBuilder.Append(poco.MemberShortName);
                stringBuilder.Append(">");
                stringBuilder.Append(' ');
            }

            if (!string.IsNullOrWhiteSpace(poco.MemberName))
            {
                stringBuilder.Append(poco.MemberName);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("for ");
            throw new System.NotSupportedException("Assigment of non-string value not yet supported");
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureChainMember
        /// <para>FeatureChainMember:Membership=memberElement=[QualifiedName]|OwnedFeatureChainMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureChainMember(SysML2.NET.Core.POCO.Root.Namespaces.IMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureReferenceMember
        /// <para>FeatureReferenceMember:Membership=memberElement=FeatureReference</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureReferenceMember(SysML2.NET.Core.POCO.Root.Namespaces.IMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of non-string value not yet supported");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ElementReferenceMember
        /// <para>ElementReferenceMember:Membership=memberElement=[QualifiedName]</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildElementReferenceMember(SysML2.NET.Core.POCO.Root.Namespaces.IMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of non-string value not yet supported");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InstantiatedTypeMember
        /// <para>InstantiatedTypeMember:Membership=memberElement=InstantiatedTypeReference|OwnedFeatureChainMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInstantiatedTypeMember(SysML2.NET.Core.POCO.Root.Namespaces.IMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyElement
        /// <para>MetadataBodyElement:Membership=NonFeatureMember|MetadataBodyFeatureMember|AliasMember|Import</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyElement(SysML2.NET.Core.POCO.Root.Namespaces.IMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
