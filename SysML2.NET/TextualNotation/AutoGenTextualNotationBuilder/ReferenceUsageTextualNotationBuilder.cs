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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedCrossFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, cursorCache, stringBuilder);
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefaultReferenceUsage
        /// <para>DefaultReferenceUsage:ReferenceUsage=RefPrefixUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefaultReferenceUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildRefPrefix(poco, cursorCache, stringBuilder);
            UsageTextualNotationBuilder.BuildUsage(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule VariantReference
        /// <para>VariantReference:ReferenceUsage=ownedRelationship+=OwnedReferenceSubsettingFeatureSpecialization*UsageBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildVariantReference(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting elementAsReferenceSubsetting)
                {
                    ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(elementAsReferenceSubsetting, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            while (ownedRelationshipCursor.Current != null)
            {
                FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
            }

            UsageTextualNotationBuilder.BuildUsageBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SourceEnd
        /// <para>SourceEnd:ReferenceUsage=(ownedRelationship+=OwnedMultiplicity)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSourceEnd(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectorEnd
        /// <para>ConnectorEnd:ReferenceUsage=(ownedRelationship+=OwnedCrossMultiplicityMember)?(declaredName=NAMEREFERENCES)?ownedRelationship+=OwnedReferenceSubsetting</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnectorEnd(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildOwnedCrossMultiplicityMember(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
                stringBuilder.Append(' ');
            }


            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append(poco.DeclaredName);
                stringBuilder.Append(" ::> ");
                stringBuilder.Append(' ');
            }


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting elementAsReferenceSubsetting)
                {
                    ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(elementAsReferenceSubsetting, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowFeature
        /// <para>FlowFeature:ReferenceUsage=ownedRelationship+=FlowFeatureRedefinition</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                {
                    RedefinitionTextualNotationBuilder.BuildFlowFeatureRedefinition(elementAsRedefinition, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadParameter
        /// <para>PayloadParameter:ReferenceUsage=PayloadFeature|IdentificationPayloadFeatureSpecializationPart?TriggerValuePart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildPayloadParameterHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NodeParameter
        /// <para>NodeParameter:ReferenceUsage=ownedRelationship+=FeatureBinding</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNodeParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildFeatureBinding(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyUsage
        /// <para>EmptyUsage:ReferenceUsage={}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule AssignmentTargetParameter
        /// <para>AssignmentTargetParameter:ReferenceUsage=(ownedRelationship+=AssignmentTargetBinding'.')?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildAssignmentTargetParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                    {
                        FeatureValueTextualNotationBuilder.BuildAssignmentTargetBinding(elementAsFeatureValue, cursorCache, stringBuilder);
                    }
                }
                stringBuilder.Append(".");
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ForVariableDeclaration
        /// <para>ForVariableDeclaration:ReferenceUsage=UsageDeclaration</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildForVariableDeclaration(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyFeature
        /// <para>EmptyFeature:ReferenceUsage={}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEmptyFeature(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SubjectUsage
        /// <para>SubjectUsage:ReferenceUsage='subject'UsageExtensionKeyword*Usage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubjectUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            stringBuilder.Append("subject ");
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current != null)
            {
                UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, cursorCache, stringBuilder);
            }

            UsageTextualNotationBuilder.BuildUsage(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SatisfactionParameter
        /// <para>SatisfactionParameter:ReferenceUsage=ownedRelationship+=SatisfactionFeatureValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSatisfactionParameter(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildSatisfactionFeatureValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyUsage
        /// <para>MetadataBodyUsage:ReferenceUsage='ref'?(':&gt;&gt;'|'redefines')?ownedRelationship+=OwnedRedefinitionFeatureSpecializationPart?ValuePart?MetadataBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("ref ");
            stringBuilder.Append(" :>> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                {
                    RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(elementAsRedefinition, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, cursorCache, stringBuilder);
            }

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, cursorCache, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildMetadataBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ReferenceUsage
        /// <para>ReferenceUsage=(EndUsagePrefix|RefPrefix)'ref'Usage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReferenceUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage pocoUsageRefPrefix when pocoUsageRefPrefix.IsDerived:
                    UsageTextualNotationBuilder.BuildRefPrefix(pocoUsageRefPrefix, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage pocoUsage:
                    UsageTextualNotationBuilder.BuildEndUsagePrefix(pocoUsage, cursorCache, stringBuilder);
                    break;
            }

            stringBuilder.Append(' ');
            stringBuilder.Append("ref ");
            UsageTextualNotationBuilder.BuildUsage(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
