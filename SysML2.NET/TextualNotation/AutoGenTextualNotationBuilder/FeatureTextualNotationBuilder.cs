// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> element
    /// </summary>
    public static partial class FeatureTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ValuePart
        /// <para>ValuePart:Feature=ownedRelationship+=FeatureValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildValuePart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildFeatureValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureSpecializationPart
        /// <para>FeatureSpecializationPart:Feature=FeatureSpecialization+MultiplicityPart?FeatureSpecialization*|MultiplicityPartFeatureSpecialization*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureSpecializationPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureSpecialization
        /// <para>FeatureSpecialization:Feature=Typings|Subsettings|References|Crosses|Redefinitions</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureSpecialization(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with only NonTerminalElement not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Typings
        /// <para>Typings:Feature=TypedBy(','ownedRelationship+=FeatureTyping)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypings(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureTypingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureTyping>().GetEnumerator();
            BuildTypedBy(poco, stringBuilder);

            while (ownedRelationshipOfFeatureTypingIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfFeatureTypingIterator.Current != null)
                {
                    FeatureTypingTextualNotationBuilder.BuildFeatureTyping(ownedRelationshipOfFeatureTypingIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypedBy
        /// <para>TypedBy:Feature=DEFINED_BYownedRelationship+=FeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypedBy(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureTypingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureTyping>().GetEnumerator();
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            ownedRelationshipOfFeatureTypingIterator.MoveNext();

            if (ownedRelationshipOfFeatureTypingIterator.Current != null)
            {
                FeatureTypingTextualNotationBuilder.BuildFeatureTyping(ownedRelationshipOfFeatureTypingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Subsettings
        /// <para>Subsettings:Feature=Subsets(','ownedRelationship+=OwnedSubsetting)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubsettings(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfSubsettingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.Subsetting>().GetEnumerator();
            BuildSubsets(poco, stringBuilder);

            while (ownedRelationshipOfSubsettingIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfSubsettingIterator.Current != null)
                {
                    SubsettingTextualNotationBuilder.BuildOwnedSubsetting(ownedRelationshipOfSubsettingIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Subsets
        /// <para>Subsets:Feature=SUBSETSownedRelationship+=OwnedSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubsets(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfSubsettingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.Subsetting>().GetEnumerator();
            stringBuilder.Append(" :> ");
            ownedRelationshipOfSubsettingIterator.MoveNext();

            if (ownedRelationshipOfSubsettingIterator.Current != null)
            {
                SubsettingTextualNotationBuilder.BuildOwnedSubsetting(ownedRelationshipOfSubsettingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule References
        /// <para>References:Feature=REFERENCESownedRelationship+=OwnedReferenceSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReferences(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfReferenceSubsettingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.ReferenceSubsetting>().GetEnumerator();
            stringBuilder.Append(" ::> ");
            ownedRelationshipOfReferenceSubsettingIterator.MoveNext();

            if (ownedRelationshipOfReferenceSubsettingIterator.Current != null)
            {
                ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(ownedRelationshipOfReferenceSubsettingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Crosses
        /// <para>Crosses:Feature=CROSSESownedRelationship+=OwnedCrossSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCrosses(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfCrossSubsettingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.CrossSubsetting>().GetEnumerator();
            stringBuilder.Append(" => ");
            ownedRelationshipOfCrossSubsettingIterator.MoveNext();

            if (ownedRelationshipOfCrossSubsettingIterator.Current != null)
            {
                CrossSubsettingTextualNotationBuilder.BuildOwnedCrossSubsetting(ownedRelationshipOfCrossSubsettingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Redefinitions
        /// <para>Redefinitions:Feature=Redefines(','ownedRelationship+=OwnedRedefinition)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRedefinitions(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfRedefinitionIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.Redefinition>().GetEnumerator();
            BuildRedefines(poco, stringBuilder);

            while (ownedRelationshipOfRedefinitionIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfRedefinitionIterator.Current != null)
                {
                    RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(ownedRelationshipOfRedefinitionIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Redefines
        /// <para>Redefines:Feature=REDEFINESownedRelationship+=OwnedRedefinition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRedefines(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfRedefinitionIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.Redefinition>().GetEnumerator();
            stringBuilder.Append(" :>> ");
            ownedRelationshipOfRedefinitionIterator.MoveNext();

            if (ownedRelationshipOfRedefinitionIterator.Current != null)
            {
                RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(ownedRelationshipOfRedefinitionIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureChain
        /// <para>OwnedFeatureChain:Feature=ownedRelationship+=OwnedFeatureChaining('.'ownedRelationship+=OwnedFeatureChaining)+</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureChain(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureChainingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureChaining>().GetEnumerator();
            ownedRelationshipOfFeatureChainingIterator.MoveNext();

            if (ownedRelationshipOfFeatureChainingIterator.Current != null)
            {
                FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(ownedRelationshipOfFeatureChainingIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfFeatureChainingIterator.MoveNext())
            {
                stringBuilder.Append(".");

                if (ownedRelationshipOfFeatureChainingIterator.Current != null)
                {
                    FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(ownedRelationshipOfFeatureChainingIterator.Current, stringBuilder);
                }

            }
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MultiplicityPart
        /// <para>MultiplicityPart:Feature=ownedRelationship+=OwnedMultiplicity|(ownedRelationship+=OwnedMultiplicity)?(isOrdered?='ordered'({isUnique=false}'nonunique')?|{isUnique=false}'nonunique'(isOrdered?='ordered')?)</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMultiplicityPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossMultiplicity
        /// <para>OwnedCrossMultiplicity:Feature=ownedRelationship+=OwnedMultiplicity</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedCrossMultiplicity(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            ownedRelationshipOfOwningMembershipIterator.MoveNext();

            if (ownedRelationshipOfOwningMembershipIterator.Current != null)
            {
                OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeature
        /// <para>PayloadFeature:Feature=Identification?PayloadFeatureSpecializationPartValuePart?|ownedRelationship+=OwnedFeatureTyping(ownedRelationship+=OwnedMultiplicity)?|ownedRelationship+=OwnedMultiplicityownedRelationship+=OwnedFeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadFeature(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeatureSpecializationPart
        /// <para>PayloadFeatureSpecializationPart:Feature=(FeatureSpecialization)+MultiplicityPart?FeatureSpecialization*|MultiplicityPartFeatureSpecialization+</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadFeatureSpecializationPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureChainPrefix
        /// <para>FeatureChainPrefix:Feature=(ownedRelationship+=OwnedFeatureChaining'.')+ownedRelationship+=OwnedFeatureChaining'.'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureChainPrefix(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureChainingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureChaining>().GetEnumerator();

            while (ownedRelationshipOfFeatureChainingIterator.MoveNext())
            {

                if (ownedRelationshipOfFeatureChainingIterator.Current != null)
                {
                    FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(ownedRelationshipOfFeatureChainingIterator.Current, stringBuilder);
                }
                stringBuilder.Append(".");

            }
            stringBuilder.Append(' ');
            ownedRelationshipOfFeatureChainingIterator.MoveNext();

            if (ownedRelationshipOfFeatureChainingIterator.Current != null)
            {
                FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(ownedRelationshipOfFeatureChainingIterator.Current, stringBuilder);
            }
            stringBuilder.Append(".");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TriggerValuePart
        /// <para>TriggerValuePart:Feature=ownedRelationship+=TriggerFeatureValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTriggerValuePart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildTriggerFeatureValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Argument
        /// <para>Argument:Feature=ownedRelationship+=ArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildArgumentValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentExpression
        /// <para>ArgumentExpression:Feature=ownedRelationship+=ArgumentExpressionValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentExpression(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildArgumentExpressionValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureElement
        /// <para>FeatureElement:Feature=Feature|Step|Expression|BooleanExpression|Invariant|Connector|BindingConnector|Succession|Flow|SuccessionFlow</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureElement(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with only NonTerminalElement not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EndFeaturePrefix
        /// <para>EndFeaturePrefix:Feature=(isConstant?='const'{isVariable=true})?isEnd?='end'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEndFeaturePrefix(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {

            if (poco.IsConstant)
            {
                stringBuilder.Append("const");
                // NonParsing Assignment Element : isVariable = true => Does not have to be process
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("end");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BasicFeaturePrefix
        /// <para>BasicFeaturePrefix:Feature=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract')?(isComposite?='composite'|isPortion?='portion')?(isVariable?='var'|isConstant?='const'{isVariable=true})?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBasicFeaturePrefix(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {

            if (poco.Direction.HasValue)
            {
                stringBuilder.Append(poco.Direction.ToString().ToLower());
                stringBuilder.Append(' ');
            }


            if (poco.IsDerived)
            {
                stringBuilder.Append("derived");
                stringBuilder.Append(' ');
            }


            if (poco.IsAbstract)
            {
                stringBuilder.Append("abstract");
                stringBuilder.Append(' ');
            }

            throw new System.NotSupportedException("Multiple alternatives with only AssignmentElement not implemented yet");
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureDeclaration
        /// <para>FeatureDeclaration:Feature=(isSufficient?='all')?(FeatureIdentification(FeatureSpecializationPart|ConjugationPart)?|FeatureSpecializationPart|ConjugationPart)FeatureRelationshipPart*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureDeclaration(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {

            if (poco.IsSufficient)
            {
                stringBuilder.Append("all");
                stringBuilder.Append(' ');
            }

            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');
            BuildFeatureRelationshipPart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureIdentification
        /// <para>FeatureIdentification:Feature='&lt;'declaredShortName=NAME'&gt;'(declaredName=NAME)?|declaredName=NAME</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureIdentification(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureRelationshipPart
        /// <para>FeatureRelationshipPart:Feature=TypeRelationshipPart|ChainingPart|InvertingPart|TypeFeaturingPart</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureRelationshipPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with only NonTerminalElement not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ChainingPart
        /// <para>ChainingPart:Feature='chains'(ownedRelationship+=OwnedFeatureChaining|FeatureChain)</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildChainingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureChainingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureChaining>().GetEnumerator();
            stringBuilder.Append("chains ");
            throw new System.NotSupportedException("Multiple alternatives with only one of the different type not implemented yet - AssignmentElement,NonTerminalElement");
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InvertingPart
        /// <para>InvertingPart:Feature='inverse''of'ownedRelationship+=OwnedFeatureInverting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInvertingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureInvertingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureInverting>().GetEnumerator();
            stringBuilder.Append("inverse ");
            stringBuilder.Append("of ");
            ownedRelationshipOfFeatureInvertingIterator.MoveNext();

            if (ownedRelationshipOfFeatureInvertingIterator.Current != null)
            {
                FeatureInvertingTextualNotationBuilder.BuildOwnedFeatureInverting(ownedRelationshipOfFeatureInvertingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeFeaturingPart
        /// <para>TypeFeaturingPart:Feature='featured''by'ownedRelationship+=OwnedTypeFeaturing(','ownedTypeFeaturing+=OwnedTypeFeaturing)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeFeaturingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfTypeFeaturingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.TypeFeaturing>().GetEnumerator();
            using var ownedTypeFeaturingIterator = poco.ownedTypeFeaturing.GetEnumerator();
            stringBuilder.Append("featured ");
            stringBuilder.Append("by ");
            ownedRelationshipOfTypeFeaturingIterator.MoveNext();

            if (ownedRelationshipOfTypeFeaturingIterator.Current != null)
            {
                TypeFeaturingTextualNotationBuilder.BuildOwnedTypeFeaturing(ownedRelationshipOfTypeFeaturingIterator.Current, stringBuilder);
            }

            while (ownedTypeFeaturingIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedTypeFeaturingIterator.Current != null)
                {
                    TypeFeaturingTextualNotationBuilder.BuildOwnedTypeFeaturing(ownedTypeFeaturingIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureChain
        /// <para>FeatureChain:Feature=ownedRelationship+=OwnedFeatureChaining('.'ownedRelationship+=OwnedFeatureChaining)+</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureChain(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureChainingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureChaining>().GetEnumerator();
            ownedRelationshipOfFeatureChainingIterator.MoveNext();

            if (ownedRelationshipOfFeatureChainingIterator.Current != null)
            {
                FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(ownedRelationshipOfFeatureChainingIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfFeatureChainingIterator.MoveNext())
            {
                stringBuilder.Append(".");

                if (ownedRelationshipOfFeatureChainingIterator.Current != null)
                {
                    FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(ownedRelationshipOfFeatureChainingIterator.Current, stringBuilder);
                }

            }
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataArgument
        /// <para>MetadataArgument:Feature=ownedRelationship+=MetadataValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildMetadataValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeReference
        /// <para>TypeReference:Feature=ownedRelationship+=ReferenceTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeReference(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureTypingIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.FeatureTyping>().GetEnumerator();
            ownedRelationshipOfFeatureTypingIterator.MoveNext();

            if (ownedRelationshipOfFeatureTypingIterator.Current != null)
            {
                FeatureTypingTextualNotationBuilder.BuildReferenceTyping(ownedRelationshipOfFeatureTypingIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryArgument
        /// <para>PrimaryArgument:Feature=ownedRelationship+=PrimaryArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildPrimaryArgumentValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryArgument
        /// <para>NonFeatureChainPrimaryArgument:Feature=ownedRelationship+=NonFeatureChainPrimaryArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildNonFeatureChainPrimaryArgumentValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BodyArgument
        /// <para>BodyArgument:Feature=ownedRelationship+=BodyArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBodyArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildBodyArgumentValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceArgument
        /// <para>FunctionReferenceArgument:Feature=ownedRelationship+=FunctionReferenceArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildFunctionReferenceArgumentValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureReference
        /// <para>FeatureReference:Feature=[QualifiedName]</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureReference(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append(poco.qualifiedName);
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConstructorResult
        /// <para>ConstructorResult:Feature=ArgumentList</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConstructorResult(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            BuildArgumentList(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentList
        /// <para>ArgumentList:Feature='('(PositionalArgumentList|NamedArgumentList)?')'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentList(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("(");
            throw new System.NotSupportedException("Multiple alternatives with only NonTerminalElement not implemented yet");
            stringBuilder.Append(")");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PositionalArgumentList
        /// <para>PositionalArgumentList:Feature=e.ownedRelationship+=ArgumentMember(','e.ownedRelationship+=ArgumentMember)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPositionalArgumentList(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Behaviors.ParameterMembership>().GetEnumerator();
            ownedRelationshipOfParameterMembershipIterator.MoveNext();

            if (ownedRelationshipOfParameterMembershipIterator.Current != null)
            {
                ParameterMembershipTextualNotationBuilder.BuildArgumentMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfParameterMembershipIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfParameterMembershipIterator.Current != null)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamedArgumentList
        /// <para>NamedArgumentList:Feature=ownedRelationship+=NamedArgumentMember(','ownedRelationship+=NamedArgumentMember)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamedArgumentList(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Types.FeatureMembership>().GetEnumerator();
            ownedRelationshipOfFeatureMembershipIterator.MoveNext();

            if (ownedRelationshipOfFeatureMembershipIterator.Current != null)
            {
                FeatureMembershipTextualNotationBuilder.BuildNamedArgumentMember(ownedRelationshipOfFeatureMembershipIterator.Current, stringBuilder);
            }

            while (ownedRelationshipOfFeatureMembershipIterator.MoveNext())
            {
                stringBuilder.Append(",");

                if (ownedRelationshipOfFeatureMembershipIterator.Current != null)
                {
                    FeatureMembershipTextualNotationBuilder.BuildNamedArgumentMember(ownedRelationshipOfFeatureMembershipIterator.Current, stringBuilder);
                }

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamedArgument
        /// <para>NamedArgument:Feature=ownedRelationship+=ParameterRedefinition'='ownedRelationship+=ArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamedArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfRedefinitionIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.Redefinition>().GetEnumerator();
            using var ownedRelationshipOfFeatureValueIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue>().GetEnumerator();
            ownedRelationshipOfRedefinitionIterator.MoveNext();

            if (ownedRelationshipOfRedefinitionIterator.Current != null)
            {
                RedefinitionTextualNotationBuilder.BuildParameterRedefinition(ownedRelationshipOfRedefinitionIterator.Current, stringBuilder);
            }
            stringBuilder.Append("=");
            ownedRelationshipOfFeatureValueIterator.MoveNext();

            if (ownedRelationshipOfFeatureValueIterator.Current != null)
            {
                FeatureValueTextualNotationBuilder.BuildArgumentValue(ownedRelationshipOfFeatureValueIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyFeature
        /// <para>MetadataBodyFeature:Feature='feature'?(':&gt;&gt;'|'redefines')?ownedRelationship+=OwnedRedefinitionFeatureSpecializationPart?ValuePart?MetadataBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyFeature(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfRedefinitionIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Core.Features.Redefinition>().GetEnumerator();
            stringBuilder.Append("feature ");
            stringBuilder.Append(" :>> ");
            ownedRelationshipOfRedefinitionIterator.MoveNext();

            if (ownedRelationshipOfRedefinitionIterator.Current != null)
            {
                RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(ownedRelationshipOfRedefinitionIterator.Current, stringBuilder);
            }
            BuildFeatureSpecializationPart(poco, stringBuilder);
            BuildValuePart(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildMetadataBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Feature
        /// <para>Feature=(FeaturePrefix('feature'|ownedRelationship+=PrefixMetadataMember)FeatureDeclaration?|(EndFeaturePrefix|BasicFeaturePrefix)FeatureDeclaration)ValuePart?TypeBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeature(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');
            BuildValuePart(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildTypeBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
