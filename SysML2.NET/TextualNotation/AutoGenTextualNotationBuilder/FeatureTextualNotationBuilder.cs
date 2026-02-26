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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Typings
        /// <para>Typings:Feature=TypedBy(','ownedRelationship+=FeatureTyping)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypings(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            BuildTypedBy(poco, stringBuilder);
            if (poco.OwnedRelationship.Count != 0)
            {
                stringBuilder.Append(",");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Subsettings
        /// <para>Subsettings:Feature=Subsets(','ownedRelationship+=OwnedSubsetting)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubsettings(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            BuildSubsets(poco, stringBuilder);
            if (poco.OwnedRelationship.Count != 0)
            {
                stringBuilder.Append(",");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule References
        /// <para>References:Feature=REFERENCESownedRelationship+=OwnedReferenceSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReferences(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Crosses
        /// <para>Crosses:Feature=CROSSESownedRelationship+=OwnedCrossSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCrosses(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Redefinitions
        /// <para>Redefinitions:Feature=Redefines(','ownedRelationship+=OwnedRedefinition)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRedefinitions(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            BuildRedefines(poco, stringBuilder);
            if (poco.OwnedRelationship.Count != 0)
            {
                stringBuilder.Append(",");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureChain
        /// <para>OwnedFeatureChain:Feature=ownedRelationship+=OwnedFeatureChaining('.'ownedRelationship+=OwnedFeatureChaining)+</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureChain(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append(".");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append(".");

            stringBuilder.Append(' ');
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Argument
        /// <para>Argument:Feature=ownedRelationship+=ArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentExpression
        /// <para>ArgumentExpression:Feature=ownedRelationship+=ArgumentExpressionValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentExpression(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureElement
        /// <para>FeatureElement:Feature=Feature|Step|Expression|BooleanExpression|Invariant|Connector|BindingConnector|Succession|Flow|SuccessionFlow</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureElement(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
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
                // Assignment Element : isVariable = true
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

            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ChainingPart
        /// <para>ChainingPart:Feature='chains'(ownedRelationship+=OwnedFeatureChaining|FeatureChain)</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildChainingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("chains ");
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
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
            stringBuilder.Append("inverse ");
            stringBuilder.Append("of ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeFeaturingPart
        /// <para>TypeFeaturingPart:Feature='featured''by'ownedRelationship+=OwnedTypeFeaturing(','ownedTypeFeaturing+=OwnedTypeFeaturing)*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeFeaturingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("featured ");
            stringBuilder.Append("by ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            if (poco.ownedTypeFeaturing.Count != 0)
            {
                stringBuilder.Append(",");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append(".");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeReference
        /// <para>TypeReference:Feature=ownedRelationship+=ReferenceTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeReference(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryArgument
        /// <para>PrimaryArgument:Feature=ownedRelationship+=PrimaryArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryArgument
        /// <para>NonFeatureChainPrimaryArgument:Feature=ownedRelationship+=NonFeatureChainPrimaryArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BodyArgument
        /// <para>BodyArgument:Feature=ownedRelationship+=BodyArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBodyArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceArgument
        /// <para>FunctionReferenceArgument:Feature=ownedRelationship+=FunctionReferenceArgumentValue</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureReference
        /// <para>FeatureReference:Feature=[QualifiedName]</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureReference(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            // Value Literal Element : [QualifiedName]

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
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            if (poco.OwnedRelationship.Count != 0)
            {
                stringBuilder.Append(",");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            if (poco.OwnedRelationship.Count != 0)
            {
                stringBuilder.Append(",");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append("=");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyFeature
        /// <para>MetadataBodyFeature:Feature='feature'?(':&gt;&gt;'|'redefines')?ownedRelationship+=OwnedRedefinitionFeatureSpecializationPart?ValuePart?MetadataBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyFeature(SysML2.NET.Core.POCO.Core.Features.IFeature poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("feature ");
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
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
