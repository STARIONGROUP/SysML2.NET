// -------------------------------------------------------------------------------------------------
// <copyright file="UsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="UsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> element
    /// </summary>
    public static partial class UsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule UsageElement
        /// <para>UsageElement:Usage=NonOccurrenceUsageElement|OccurrenceUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RefPrefix
        /// <para>RefPrefix:Usage=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract'|isVariation?='variation')?(isConstant?='constant')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRefPrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {

            if (poco.Direction.HasValue)
            {
                stringBuilder.Append(poco.Direction.ToString().ToLower());
                stringBuilder.Append(' ');
            }


            if (poco.IsDerived)
            {
                stringBuilder.Append(" derived ");
                stringBuilder.Append(' ');
            }

            if (poco.IsAbstract)
            {
                stringBuilder.Append(" abstract ");
            }
            else if (poco.IsVariation)
            {
                stringBuilder.Append(" variation ");
            }


            if (poco.IsConstant)
            {
                stringBuilder.Append(" constant ");
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BasicUsagePrefix
        /// <para>BasicUsagePrefix:Usage=RefPrefix(isReference?='ref')?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBasicUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            BuildRefPrefix(poco, stringBuilder);

            if (poco.isReference)
            {
                stringBuilder.Append(" ref ");
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EndUsagePrefix
        /// <para>EndUsagePrefix:Usage=isEnd?='end'(ownedRelationship+=OwnedCrossFeatureMember)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEndUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            if (poco.IsEnd)
            {
                stringBuilder.Append(" end ");
            }

            if (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedCrossFeatureMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
                }
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageExtensionKeyword
        /// <para>UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageExtensionKeyword(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            ownedRelationshipOfOwningMembershipIterator.MoveNext();

            if (ownedRelationshipOfOwningMembershipIterator.Current != null)
            {
                OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UnextendedUsagePrefix
        /// <para>UnextendedUsagePrefix:Usage=EndUsagePrefix|BasicUsagePrefix</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUnextendedUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsagePrefix
        /// <para>UsagePrefix:Usage=UnextendedUsagePrefixUsageExtensionKeyword*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsagePrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            BuildUnextendedUsagePrefix(poco, stringBuilder);
            BuildUsageExtensionKeyword(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageDeclaration
        /// <para>UsageDeclaration:Usage=IdentificationFeatureSpecializationPart?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageDeclaration(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
            FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageCompletion
        /// <para>UsageCompletion:Usage=ValuePart?UsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageCompletion(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);
            BuildUsageBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UsageBody
        /// <para>UsageBody:Usage=DefinitionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsageBody(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            TypeTextualNotationBuilder.BuildDefinitionBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonOccurrenceUsageElement
        /// <para>NonOccurrenceUsageElement:Usage=DefaultReferenceUsage|ReferenceUsage|AttributeUsage|EnumerationUsage|BindingConnectorAsUsage|SuccessionAsUsage|ExtendedUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsageElement
        /// <para>OccurrenceUsageElement:Usage=StructureUsageElement|BehaviorUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StructureUsageElement
        /// <para>StructureUsageElement:Usage=OccurrenceUsage|IndividualUsage|PortionUsage|EventOccurrenceUsage|ItemUsage|PartUsage|ViewUsage|RenderingUsage|PortUsage|ConnectionUsage|InterfaceUsage|AllocationUsage|Message|FlowUsage|SuccessionFlowUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStructureUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BehaviorUsageElement
        /// <para>BehaviorUsageElement:Usage=ActionUsage|CalculationUsage|StateUsage|ConstraintUsage|RequirementUsage|ConcernUsage|CaseUsage|AnalysisCaseUsage|VerificationCaseUsage|UseCaseUsage|ViewpointUsage|PerformActionUsage|ExhibitStateUsage|IncludeUseCaseUsage|AssertConstraintUsage|SatisfyRequirementUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBehaviorUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.Calculations.CalculationUsage pocoCalculationUsage:
                    CalculationUsageTextualNotationBuilder.BuildCalculationUsage(pocoCalculationUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.StateUsage pocoStateUsage:
                    StateUsageTextualNotationBuilder.BuildStateUsage(pocoStateUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.ActionUsage pocoActionUsage:
                    ActionUsageTextualNotationBuilder.BuildActionUsage(pocoActionUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.ConcernUsage pocoConcernUsage:
                    ConcernUsageTextualNotationBuilder.BuildConcernUsage(pocoConcernUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.RequirementUsage pocoRequirementUsage:
                    RequirementUsageTextualNotationBuilder.BuildRequirementUsage(pocoRequirementUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.ConstraintUsage pocoConstraintUsage:
                    ConstraintUsageTextualNotationBuilder.BuildConstraintUsage(pocoConstraintUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.AnalysisCases.AnalysisCaseUsage pocoAnalysisCaseUsage:
                    AnalysisCaseUsageTextualNotationBuilder.BuildAnalysisCaseUsage(pocoAnalysisCaseUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.VerificationCases.VerificationCaseUsage pocoVerificationCaseUsage:
                    VerificationCaseUsageTextualNotationBuilder.BuildVerificationCaseUsage(pocoVerificationCaseUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.UseCaseUsage pocoUseCaseUsage:
                    UseCaseUsageTextualNotationBuilder.BuildUseCaseUsage(pocoUseCaseUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Cases.CaseUsage pocoCaseUsage:
                    CaseUsageTextualNotationBuilder.BuildCaseUsage(pocoCaseUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewpointUsage pocoViewpointUsage:
                    ViewpointUsageTextualNotationBuilder.BuildViewpointUsage(pocoViewpointUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.States.ExhibitStateUsage pocoExhibitStateUsage:
                    ExhibitStateUsageTextualNotationBuilder.BuildExhibitStateUsage(pocoExhibitStateUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.UseCases.IncludeUseCaseUsage pocoIncludeUseCaseUsage:
                    IncludeUseCaseUsageTextualNotationBuilder.BuildIncludeUseCaseUsage(pocoIncludeUseCaseUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Actions.PerformActionUsage pocoPerformActionUsage:
                    PerformActionUsageTextualNotationBuilder.BuildPerformActionUsage(pocoPerformActionUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Requirements.SatisfyRequirementUsage pocoSatisfyRequirementUsage:
                    SatisfyRequirementUsageTextualNotationBuilder.BuildSatisfyRequirementUsage(pocoSatisfyRequirementUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Constraints.AssertConstraintUsage pocoAssertConstraintUsage:
                    AssertConstraintUsageTextualNotationBuilder.BuildAssertConstraintUsage(pocoAssertConstraintUsage, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule VariantUsageElement
        /// <para>VariantUsageElement:Usage=VariantReference|ReferenceUsage|AttributeUsage|BindingConnectorAsUsage|SuccessionAsUsage|OccurrenceUsage|IndividualUsage|PortionUsage|EventOccurrenceUsage|ItemUsage|PartUsage|ViewUsage|RenderingUsage|PortUsage|ConnectionUsage|InterfaceUsage|AllocationUsage|Message|FlowUsage|SuccessionFlowUsage|BehaviorUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildVariantUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceNonOccurrenceUsageElement
        /// <para>InterfaceNonOccurrenceUsageElement:Usage=ReferenceUsage|AttributeUsage|EnumerationUsage|BindingConnectorAsUsage|SuccessionAsUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceNonOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.ReferenceUsage pocoReferenceUsage:
                    ReferenceUsageTextualNotationBuilder.BuildReferenceUsage(pocoReferenceUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Enumerations.EnumerationUsage pocoEnumerationUsage:
                    EnumerationUsageTextualNotationBuilder.BuildEnumerationUsage(pocoEnumerationUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Attributes.AttributeUsage pocoAttributeUsage:
                    AttributeUsageTextualNotationBuilder.BuildAttributeUsage(pocoAttributeUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.BindingConnectorAsUsage pocoBindingConnectorAsUsage:
                    BindingConnectorAsUsageTextualNotationBuilder.BuildBindingConnectorAsUsage(pocoBindingConnectorAsUsage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Systems.Connections.SuccessionAsUsage pocoSuccessionAsUsage:
                    SuccessionAsUsageTextualNotationBuilder.BuildSuccessionAsUsage(pocoSuccessionAsUsage, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceOccurrenceUsageElement
        /// <para>InterfaceOccurrenceUsageElement:Usage=DefaultInterfaceEnd|StructureUsageElement|BehaviorUsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceOccurrenceUsageElement(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionTargetSuccession
        /// <para>ActionTargetSuccession:Usage=(TargetSuccession|GuardedTargetSuccession|DefaultTargetSuccession)UsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionTargetSuccession(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
            stringBuilder.Append(' ');
            BuildUsageBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExtendedUsage
        /// <para>ExtendedUsage:Usage=UnextendedUsagePrefixUsageExtensionKeyword+Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExtendedUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            BuildUnextendedUsagePrefix(poco, stringBuilder);
            BuildUsageExtensionKeyword(poco, stringBuilder);
            BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Usage
        /// <para>Usage=UsageDeclarationUsageCompletion</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUsage(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage poco, StringBuilder stringBuilder)
        {
            BuildUsageDeclaration(poco, stringBuilder);
            BuildUsageCompletion(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
