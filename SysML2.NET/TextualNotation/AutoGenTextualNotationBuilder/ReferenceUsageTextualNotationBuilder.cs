// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// The <see cref="ReferenceUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> element
    /// </summary>
    public static partial class ReferenceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossFeature
        /// <para>OwnedCrossFeature:ReferenceUsage=BasicUsagePrefixUsageDeclaration</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedCrossFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, stringBuilder);
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefaultReferenceUsage
        /// <para>DefaultReferenceUsage:ReferenceUsage=RefPrefixUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefaultReferenceUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildRefPrefix(poco, stringBuilder);
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule VariantReference
        /// <para>VariantReference:ReferenceUsage=ownedRelationship+=OwnedReferenceSubsettingFeatureSpecialization*UsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildVariantReference(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfReferenceSubsettingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.ReferenceSubsetting>().GetEnumerator();
            ownedRelationshipOfReferenceSubsettingIterator.MoveNext();

            if (ownedRelationshipOfReferenceSubsettingIterator.Current != null)
            {
                ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(ownedRelationshipOfReferenceSubsettingIterator.Current, stringBuilder);
            }
            // Handle collection Non Terminal 
            BuildFeatureSpecializationInternal(poco, stringBuilder); FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, stringBuilder);
            UsageTextualNotationBuilder.BuildUsageBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SourceEnd
        /// <para>SourceEnd:ReferenceUsage=(ownedRelationship+=OwnedMultiplicity)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSourceEnd(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();

            if (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(ownedRelationshipOfOwningMembershipIterator.Current, 0, stringBuilder);
                }
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectorEnd
        /// <para>ConnectorEnd:ReferenceUsage=(ownedRelationship+=OwnedCrossMultiplicityMember)?(declaredName=NAMEREFERENCES)?ownedRelationship+=OwnedReferenceSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnectorEnd(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            using var ownedRelationshipOfReferenceSubsettingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.ReferenceSubsetting>().GetEnumerator();

            if (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedCrossMultiplicityMember(ownedRelationshipOfOwningMembershipIterator.Current, 0, stringBuilder);
                }
                stringBuilder.Append(' ');
            }


            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append(poco.DeclaredName);
                stringBuilder.Append(" ::> ");
                stringBuilder.Append(' ');
            }

            ownedRelationshipOfReferenceSubsettingIterator.MoveNext();

            if (ownedRelationshipOfReferenceSubsettingIterator.Current != null)
            {
                ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(ownedRelationshipOfReferenceSubsettingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowFeature
        /// <para>FlowFeature:ReferenceUsage=ownedRelationship+=FlowFeatureRedefinition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildFlowFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelationship.Count)
            {
                var elementForOwnedRelationship = poco.OwnedRelationship[elementIndex];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                {
                    RedefinitionTextualNotationBuilder.BuildFlowFeatureRedefinition(elementAsRedefinition, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadParameter
        /// <para>PayloadParameter:ReferenceUsage=PayloadFeature|IdentificationPayloadFeatureSpecializationPart?TriggerValuePart</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NodeParameter
        /// <para>NodeParameter:ReferenceUsage=ownedRelationship+=FeatureBinding</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildNodeParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelationship.Count)
            {
                var elementForOwnedRelationship = poco.OwnedRelationship[elementIndex];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildFeatureBinding(elementAsFeatureValue, 0, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyUsage
        /// <para>EmptyUsage:ReferenceUsage={}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentTargetParameter
        /// <para>AssignmentTargetParameter:ReferenceUsage=(ownedRelationship+=AssignmentTargetBinding'.')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssignmentTargetParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();

            if (ownedRelationshipOfFeatureValueIterator.MoveNext())
            {

                if (ownedRelationshipOfFeatureValueIterator.Current != null)
                {
                    FeatureValueTextualNotationBuilder.BuildAssignmentTargetBinding(ownedRelationshipOfFeatureValueIterator.Current, 0, stringBuilder);
                }
                stringBuilder.Append(".");
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ForVariableDeclaration
        /// <para>ForVariableDeclaration:ReferenceUsage=UsageDeclaration</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildForVariableDeclaration(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyFeature
        /// <para>EmptyFeature:ReferenceUsage={}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SubjectUsage
        /// <para>SubjectUsage:ReferenceUsage='subject'UsageExtensionKeyword*Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubjectUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("subject ");
            // Handle collection Non Terminal 
            for (var ownedRelationshipIndex = 0; ownedRelationshipIndex < poco.OwnedRelationship.Count; ownedRelationshipIndex++)
            {
                ownedRelationshipIndex = UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, ownedRelationshipIndex, stringBuilder);
            }
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SatisfactionParameter
        /// <para>SatisfactionParameter:ReferenceUsage=ownedRelationship+=SatisfactionFeatureValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildSatisfactionParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelationship.Count)
            {
                var elementForOwnedRelationship = poco.OwnedRelationship[elementIndex];

                if (elementForOwnedRelationship is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildSatisfactionFeatureValue(elementAsFeatureValue, 0, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyUsage
        /// <para>MetadataBodyUsage:ReferenceUsage='ref'?(':&gt;&gt;'|'redefines')?ownedRelationship+=OwnedRedefinitionFeatureSpecializationPart?ValuePart?MetadataBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfRedefinitionIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.Redefinition>().GetEnumerator();
            stringBuilder.Append("ref ");
            stringBuilder.Append(" :>> ");
            ownedRelationshipOfRedefinitionIterator.MoveNext();

            if (ownedRelationshipOfRedefinitionIterator.Current != null)
            {
                RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(ownedRelationshipOfRedefinitionIterator.Current, stringBuilder);
            }
            FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, stringBuilder);
            FeatureTextualNotationBuilder.BuildValuePart(poco, 0, stringBuilder);
            TypeTextualNotationBuilder.BuildMetadataBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ReferenceUsage
        /// <para>ReferenceUsage=(EndUsagePrefix|RefPrefix)'ref'Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReferenceUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
            stringBuilder.Append(' ');
            stringBuilder.Append("ref ");
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
