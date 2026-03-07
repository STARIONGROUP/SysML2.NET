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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="TypeTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> element
    /// </summary>
    public static partial class TypeTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionBody
        /// <para>DefinitionBody:Type=';'|'{'DefinitionBodyItem*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionBodyItem
        /// <para>DefinitionBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=OccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBody
        /// <para>InterfaceBody:Type=';'|'{'InterfaceBodyItem*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceBodyItem
        /// <para>InterfaceBodyItem:Type=ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=InterfaceNonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=InterfaceOccurrenceUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBody
        /// <para>ActionBody:Type=';'|'{'ActionBodyItem*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ActionBodyItem
        /// <para>ActionBodyItem:Type=NonBehaviorBodyItem|ownedRelationship+=InitialNodeMember(ownedRelationship+=ActionTargetSuccessionMember)*|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=ActionBehaviorMember(ownedRelationship+=ActionTargetSuccessionMember)*|ownedRelationship+=GuardedSuccessionMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActionBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StateBodyItem
        /// <para>StateBodyItem:Type=NonBehaviorBodyItem|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=BehaviorUsageMember(ownedRelationship+=TargetTransitionUsageMember)*|ownedRelationship+=TransitionUsageMember|ownedRelationship+=EntryActionMember(ownedRelationship+=EntryTransitionMember)*|ownedRelationship+=DoActionMember|ownedRelationship+=ExitActionMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStateBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBody
        /// <para>CalculationBody:Type=';'|'{'CalculationBodyPart'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCalculationBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBodyPart
        /// <para>CalculationBodyPart:Type=CalculationBodyItem*(ownedRelationship+=ResultExpressionMember)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCalculationBodyPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfResultExpressionMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Functions.ResultExpressionMembership>().GetEnumerator();
            BuildCalculationBodyItem(poco, stringBuilder);

            if (ownedRelationshipOfResultExpressionMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfResultExpressionMembershipIterator.Current != null)
                {
                    ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(ownedRelationshipOfResultExpressionMembershipIterator.Current, stringBuilder);
                }
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CalculationBodyItem
        /// <para>CalculationBodyItem:Type=ActionBodyItem|ownedRelationship+=ReturnParameterMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCalculationBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementBody
        /// <para>RequirementBody:Type=';'|'{'RequirementBodyItem*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementBodyItem
        /// <para>RequirementBodyItem:Type=DefinitionBodyItem|ownedRelationship+=SubjectMember|ownedRelationship+=RequirementConstraintMember|ownedRelationship+=FramedConcernMember|ownedRelationship+=RequirementVerificationMember|ownedRelationship+=ActorMember|ownedRelationship+=StakeholderMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CaseBody
        /// <para>CaseBody:Type=';'|'{'CaseBodyItem*(ownedRelationship+=ResultExpressionMember)?'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCaseBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule CaseBodyItem
        /// <para>CaseBodyItem:Type=ActionBodyItem|ownedRelationship+=SubjectMember|ownedRelationship+=ActorMember|ownedRelationship+=ObjectiveMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCaseBodyItem(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBody
        /// <para>MetadataBody:Type=';'|'{'(ownedRelationship+=DefinitionMember|ownedRelationship+=MetadataBodyUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import)*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypePrefix
        /// <para>TypePrefix:Type=(isAbstract?='abstract')?(ownedRelationship+=PrefixMetadataMember)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypePrefix(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();

            if (poco.IsAbstract)
            {
                stringBuilder.Append("abstract");
                stringBuilder.Append(' ');
            }


            while (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeDeclaration
        /// <para>TypeDeclaration:Type=(isSufficient?='all')?Identification(ownedRelationship+=OwnedMultiplicity)?(SpecializationPart|ConjugationPart)+TypeRelationshipPart*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeDeclaration(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();

            if (poco.IsSufficient)
            {
                stringBuilder.Append("all");
                stringBuilder.Append(' ');
            }

            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);

            if (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
                }
                stringBuilder.Append(' ');
            }

            {
                throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            }
            stringBuilder.Append(' ');
            BuildTypeRelationshipPart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SpecializationPart
        /// <para>SpecializationPart:Type=SPECIALIZESownedRelationship+=OwnedSpecialization(','ownedRelationship+=OwnedSpecialization)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSpecializationPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfSpecializationIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.Specialization>().GetEnumerator();
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            ownedRelationshipOfSpecializationIterator.MoveNext();

            if (ownedRelationshipOfSpecializationIterator.Current != null)
            {
                SpecializationTextualNotationBuilder.BuildOwnedSpecialization(ownedRelationshipOfSpecializationIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfSpecializationIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfSpecializationIterator.Current != null)
                {
                    SpecializationTextualNotationBuilder.BuildOwnedSpecialization(ownedRelationshipOfSpecializationIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConjugationPart
        /// <para>ConjugationPart:Type=CONJUGATESownedRelationship+=OwnedConjugation</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConjugationPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfConjugationIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.Conjugation>().GetEnumerator();
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            ownedRelationshipOfConjugationIterator.MoveNext();

            if (ownedRelationshipOfConjugationIterator.Current != null)
            {
                ConjugationTextualNotationBuilder.BuildOwnedConjugation(ownedRelationshipOfConjugationIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeRelationshipPart
        /// <para>TypeRelationshipPart:Type=DisjoiningPart|UnioningPart|IntersectingPart|DifferencingPart</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeRelationshipPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DisjoiningPart
        /// <para>DisjoiningPart:Type='disjoint''from'ownedRelationship+=OwnedDisjoining(','ownedRelationship+=OwnedDisjoining)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDisjoiningPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfDisjoiningIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.Disjoining>().GetEnumerator();
            stringBuilder.Append("disjoint ");
            stringBuilder.Append("from ");
            ownedRelationshipOfDisjoiningIterator.MoveNext();

            if (ownedRelationshipOfDisjoiningIterator.Current != null)
            {
                DisjoiningTextualNotationBuilder.BuildOwnedDisjoining(ownedRelationshipOfDisjoiningIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfDisjoiningIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfDisjoiningIterator.Current != null)
                {
                    DisjoiningTextualNotationBuilder.BuildOwnedDisjoining(ownedRelationshipOfDisjoiningIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule UnioningPart
        /// <para>UnioningPart:Type='unions'ownedRelationship+=Unioning(','ownedRelationship+=Unioning)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildUnioningPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfUnioningIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.Unioning>().GetEnumerator();
            stringBuilder.Append("unions ");
            ownedRelationshipOfUnioningIterator.MoveNext();

            if (ownedRelationshipOfUnioningIterator.Current != null)
            {
                UnioningTextualNotationBuilder.BuildUnioning(ownedRelationshipOfUnioningIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfUnioningIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfUnioningIterator.Current != null)
                {
                    UnioningTextualNotationBuilder.BuildUnioning(ownedRelationshipOfUnioningIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule IntersectingPart
        /// <para>IntersectingPart:Type='intersects'ownedRelationship+=Intersecting(','ownedRelationship+=Intersecting)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIntersectingPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfIntersectingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.Intersecting>().GetEnumerator();
            stringBuilder.Append("intersects ");
            ownedRelationshipOfIntersectingIterator.MoveNext();

            if (ownedRelationshipOfIntersectingIterator.Current != null)
            {
                IntersectingTextualNotationBuilder.BuildIntersecting(ownedRelationshipOfIntersectingIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfIntersectingIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfIntersectingIterator.Current != null)
                {
                    IntersectingTextualNotationBuilder.BuildIntersecting(ownedRelationshipOfIntersectingIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DifferencingPart
        /// <para>DifferencingPart:Type='differences'ownedRelationship+=Differencing(','ownedRelationship+=Differencing)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDifferencingPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfDifferencingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.Differencing>().GetEnumerator();
            stringBuilder.Append("differences ");
            ownedRelationshipOfDifferencingIterator.MoveNext();

            if (ownedRelationshipOfDifferencingIterator.Current != null)
            {
                DifferencingTextualNotationBuilder.BuildDifferencing(ownedRelationshipOfDifferencingIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfDifferencingIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfDifferencingIterator.Current != null)
                {
                    DifferencingTextualNotationBuilder.BuildDifferencing(ownedRelationshipOfDifferencingIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeBody
        /// <para>TypeBody:Type=';'|'{'TypeBodyElement*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeBodyElement
        /// <para>TypeBodyElement:Type=ownedRelationship+=NonFeatureMember|ownedRelationship+=FeatureMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeBodyElement(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionBody
        /// <para>FunctionBody:Type=';'|'{'FunctionBodyPart'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionBody(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionBodyPart
        /// <para>FunctionBodyPart:Type=(TypeBodyElement|ownedRelationship+=ReturnFeatureMember)*(ownedRelationship+=ResultExpressionMember)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionBodyPart(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfReturnParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Functions.ReturnParameterMembership>().GetEnumerator();
            using var ownedRelationshipOfResultExpressionMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Functions.ResultExpressionMembership>().GetEnumerator();

            while (ownedRelationshipOfReturnParameterMembershipIterator.MoveNext())
            {
                throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            }

            if (ownedRelationshipOfResultExpressionMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfResultExpressionMembershipIterator.Current != null)
                {
                    ResultExpressionMembershipTextualNotationBuilder.BuildResultExpressionMember(ownedRelationshipOfResultExpressionMembershipIterator.Current, stringBuilder);
                }
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InstantiatedTypeReference
        /// <para>InstantiatedTypeReference:Type=[QualifiedName]</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInstantiatedTypeReference(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append(poco.qualifiedName);
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Type
        /// <para>Type=TypePrefix'type'TypeDeclarationTypeBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IType" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildType(SysML2.NET.Core.POCO.Core.Types.IType poco, StringBuilder stringBuilder)
        {
            BuildTypePrefix(poco, stringBuilder);
            stringBuilder.Append("type ");
            BuildTypeDeclaration(poco, stringBuilder);
            BuildTypeBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
