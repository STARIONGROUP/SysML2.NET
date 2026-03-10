// -------------------------------------------------------------------------------------------------
// <copyright file="ElementTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ElementTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> element
    /// </summary>
    public static partial class ElementTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule Identification
        /// <para>Identification:Element=('&lt;'declaredShortName=NAME'&gt;')?(declaredName=NAME)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIdentification(SysML2.NET.Core.POCO.Root.Elements.IElement poco, StringBuilder stringBuilder)
        {

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName))
            {
                stringBuilder.Append("<");
                stringBuilder.Append(poco.DeclaredShortName);
                stringBuilder.Append(">");
                stringBuilder.Append(' ');
            }


            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append(poco.DeclaredName);
                stringBuilder.Append(' ');
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionElement
        /// <para>DefinitionElement:Element=Package|LibraryPackage|AnnotatingElement|Dependency|AttributeDefinition|EnumerationDefinition|OccurrenceDefinition|IndividualDefinition|ItemDefinition|PartDefinition|ConnectionDefinition|FlowDefinition|InterfaceDefinition|PortDefinition|ActionDefinition|CalculationDefinition|StateDefinition|ConstraintDefinition|RequirementDefinition|ConcernDefinition|CaseDefinition|AnalysisCaseDefinition|VerificationCaseDefinition|UseCaseDefinition|ViewDefinition|ViewpointDefinition|RenderingDefinition|MetadataDefinition|ExtendedDefinition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives with same referenced rule type not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedRelatedElement
        /// <para>OwnedRelatedElement:Element=NonFeatureElement|FeatureElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedRelatedElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Features.Feature pocoFeature:
                    FeatureTextualNotationBuilder.BuildFeatureElement(pocoFeature, stringBuilder);
                    break;
                default:
                    BuildNonFeatureElement(poco, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MemberElement
        /// <para>MemberElement:Element=AnnotatingElement|NonFeatureElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMemberElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Root.Annotations.AnnotatingElement pocoAnnotatingElement:
                    AnnotatingElementTextualNotationBuilder.BuildAnnotatingElement(pocoAnnotatingElement, stringBuilder);
                    break;
                default:
                    BuildNonFeatureElement(poco, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureElement
        /// <para>NonFeatureElement:Element=Dependency|Namespace|Type|Classifier|DataType|Class|Structure|Metaclass|Association|AssociationStructure|Interaction|Behavior|Function|Predicate|Multiplicity|Package|LibraryPackage|Specialization|Conjugation|Subclassification|Disjoining|FeatureInverting|FeatureTyping|Subsetting|Redefinition|TypeFeaturing</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Root.Dependencies.Dependency pocoDependency:
                    DependencyTextualNotationBuilder.BuildDependency(pocoDependency, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Subsetting pocoSubsetting:
                    SubsettingTextualNotationBuilder.BuildSubsetting(pocoSubsetting, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureTyping pocoFeatureTyping:
                    FeatureTypingTextualNotationBuilder.BuildFeatureTyping(pocoFeatureTyping, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.FeatureInverting pocoFeatureInverting:
                    FeatureInvertingTextualNotationBuilder.BuildFeatureInverting(pocoFeatureInverting, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Disjoining pocoDisjoining:
                    DisjoiningTextualNotationBuilder.BuildDisjoining(pocoDisjoining, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.Subclassification pocoSubclassification:
                    SubclassificationTextualNotationBuilder.BuildSubclassification(pocoSubclassification, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Conjugation pocoConjugation:
                    ConjugationTextualNotationBuilder.BuildConjugation(pocoConjugation, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Specialization pocoSpecialization:
                    SpecializationTextualNotationBuilder.BuildSpecialization(pocoSpecialization, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.LibraryPackage pocoLibraryPackage:
                    LibraryPackageTextualNotationBuilder.BuildLibraryPackage(pocoLibraryPackage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.Package pocoPackage:
                    PackageTextualNotationBuilder.BuildPackage(pocoPackage, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Multiplicity pocoMultiplicity:
                    MultiplicityTextualNotationBuilder.BuildMultiplicity(pocoMultiplicity, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Predicate pocoPredicate:
                    PredicateTextualNotationBuilder.BuildPredicate(pocoPredicate, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.Function pocoFunction:
                    FunctionTextualNotationBuilder.BuildFunction(pocoFunction, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.Interaction pocoInteraction:
                    InteractionTextualNotationBuilder.BuildInteraction(pocoInteraction, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.Behavior pocoBehavior:
                    BehaviorTextualNotationBuilder.BuildBehavior(pocoBehavior, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Associations.AssociationStructure pocoAssociationStructure:
                    AssociationStructureTextualNotationBuilder.BuildAssociationStructure(pocoAssociationStructure, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Associations.Association pocoAssociation:
                    AssociationTextualNotationBuilder.BuildAssociation(pocoAssociation, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Metadata.Metaclass pocoMetaclass:
                    MetaclassTextualNotationBuilder.BuildMetaclass(pocoMetaclass, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Structures.Structure pocoStructure:
                    StructureTextualNotationBuilder.BuildStructure(pocoStructure, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Classes.Class pocoClass:
                    ClassTextualNotationBuilder.BuildClass(pocoClass, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.DataTypes.DataType pocoDataType:
                    DataTypeTextualNotationBuilder.BuildDataType(pocoDataType, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.Classifier pocoClassifier:
                    ClassifierTextualNotationBuilder.BuildClassifier(pocoClassifier, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.Type pocoType:
                    TypeTextualNotationBuilder.BuildType(pocoType, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.Namespace pocoNamespace:
                    NamespaceTextualNotationBuilder.BuildNamespace(pocoNamespace, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.Redefinition pocoRedefinition:
                    RedefinitionTextualNotationBuilder.BuildRedefinition(pocoRedefinition, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.TypeFeaturing pocoTypeFeaturing:
                    TypeFeaturingTextualNotationBuilder.BuildTypeFeaturing(pocoTypeFeaturing, stringBuilder);
                    break;
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
