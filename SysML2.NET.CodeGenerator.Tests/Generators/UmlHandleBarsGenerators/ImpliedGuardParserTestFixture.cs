// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedGuardParserTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.Classification;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;

    [TestFixture]
    public class ImpliedGuardParserTestFixture
    {
        /// <summary>
        /// The metaclass names an owning-Type kind test over two alternatives yields.
        /// </summary>
        private static readonly string[] ExpectedPartTypeNames = ["PartDefinition", "PartUsage"];

        /// <summary>
        /// The metaclass name an owned-typing kind test yields.
        /// </summary>
        private static readonly string[] ExpectedDataTypeNames = ["DataType"];

        [Test]
        public void VerifyParse()
        {
            using (Assert.EnterMultipleScope())
            {
                var owningTypeKind = ImpliedGuardParser.Parse("isComposite and owningType <> null and (owningType.oclIsKindOf(PartDefinition) or owningType.oclIsKindOf(PartUsage))");
                Assert.That(owningTypeKind.Shape, Is.EqualTo(ImpliedGuardShape.OwningTypeKind));
                Assert.That(owningTypeKind.RequiresComposite, Is.True);
                Assert.That(owningTypeKind.TypeNames, Is.EqualTo(ExpectedPartTypeNames));

                var withoutComposite = ImpliedGuardParser.Parse("owningType <> null and (owningType.oclIsKindOf(ViewDefinition) or owningType.oclIsKindOf(ViewUsage))");
                Assert.That(withoutComposite.Shape, Is.EqualTo(ImpliedGuardShape.OwningTypeKind));
                Assert.That(withoutComposite.RequiresComposite, Is.False);

                var operationCall = ImpliedGuardParser.Parse("isSubactionUsage()");
                Assert.That(operationCall.Shape, Is.EqualTo(ImpliedGuardShape.OperationCall));
                Assert.That(operationCall.MemberName, Is.EqualTo("isSubactionUsage"));
                Assert.That(operationCall.IsNegated, Is.False);

                var negated = ImpliedGuardParser.Parse("not isTriggerAction()");
                Assert.That(negated.Shape, Is.EqualTo(ImpliedGuardShape.OperationCall));
                Assert.That(negated.IsNegated, Is.True);

                var withArgument = ImpliedGuardParser.Parse("isSubstateUsage(true)");
                Assert.That(withArgument.Shape, Is.EqualTo(ImpliedGuardShape.OperationCall));
                Assert.That(withArgument.Literal, Is.EqualTo("true"));

                var endCount = ImpliedGuardParser.Parse("ownedEndFeature->size() = 2");
                Assert.That(endCount.Shape, Is.EqualTo(ImpliedGuardShape.OwnedEndFeatureCount));
                Assert.That(endCount.Literal, Is.EqualTo("2"));

                var notEmpty = ImpliedGuardParser.Parse("ownedEndFeatures->notEmpty()");
                Assert.That(notEmpty.Shape, Is.EqualTo(ImpliedGuardShape.OwnedEndFeatureCount));
                Assert.That(notEmpty.Literal, Is.Null);

                var ownedTyping = ImpliedGuardParser.Parse("ownedTyping.type->exists(selectByKind(DataType))");
                Assert.That(ownedTyping.Shape, Is.EqualTo(ImpliedGuardShape.OwnedTypingKind));
                Assert.That(ownedTyping.TypeNames, Is.EqualTo(ExpectedDataTypeNames));

                var membership = ImpliedGuardParser.Parse("owningFeatureMembership <> null and owningFeatureMembership.oclIsKindOf(StakeholderMembership)");
                Assert.That(membership.Shape, Is.EqualTo(ImpliedGuardShape.OwningFeatureMembershipKind));

                var enumeration = ImpliedGuardParser.Parse("portionKind = PortionKind::timeslice");
                Assert.That(enumeration.Shape, Is.EqualTo(ImpliedGuardShape.EnumerationComparison));
                Assert.That(enumeration.MemberName, Is.EqualTo("portionKind"));
                Assert.That(enumeration.Literal, Is.EqualTo("timeslice"));

                var booleanProperty = ImpliedGuardParser.Parse("isIndividual");
                Assert.That(booleanProperty.Shape, Is.EqualTo(ImpliedGuardShape.BooleanProperty));

                // Multi-line OCL from the XMI must normalise before matching.
                var multiLine = ImpliedGuardParser.Parse("owningType <> null and\n (owningType.oclIsKindOf(Behavior) or\n owningType.oclIsKindOf(Step))");
                Assert.That(multiLine.Shape, Is.EqualTo(ImpliedGuardShape.OwningTypeKind));
            }
        }

        [Test]
        public void VerifyParseRejectsWhatItCannotTranslate()
        {
            using (Assert.EnterMultipleScope())
            {
                // A nested oclAsType navigation is beyond the recognised shapes and must NOT be approximated.
                var nested = ImpliedGuardParser.Parse("isComposite and owningType <> null and (owningType.oclIsKindOf(Structure) or owningType.oclIsKindOf(Feature) and owningType.oclAsType(Feature).type->exists(oclIsKindOf(Structure)))");
                Assert.That(nested.Shape, Is.EqualTo(ImpliedGuardShape.RequiresHandCoding));

                // An extra conjunct beyond the recognised owner-kind shape likewise falls back.
                var extraConjunct = ImpliedGuardParser.Parse("isComposite and owningType <> null and (owningType.oclIsKindOf(StateDefinition) or owningType.oclIsKindOf(StateUsage)) and source <> null and source.oclIsKindOf(StateUsage)");
                Assert.That(extraConjunct.Shape, Is.EqualTo(ImpliedGuardShape.RequiresHandCoding));

                Assert.That(ImpliedGuardParser.Parse(null).Shape, Is.EqualTo(ImpliedGuardShape.RequiresHandCoding));
                Assert.That(ImpliedGuardParser.Parse(string.Empty).Shape, Is.EqualTo(ImpliedGuardShape.RequiresHandCoding));
                Assert.That(ImpliedGuardParser.Parse("   ").Shape, Is.EqualTo(ImpliedGuardShape.RequiresHandCoding));
            }
        }

        /// <summary>
        /// Pins how much of the REAL constraint set the parser covers, so a regression in the patterns shows
        /// up as a coverage drop rather than silently shifting guards into hand-coding.
        /// </summary>
        [Test]
        public void VerifyCoverageOfTheActualConstraintSet()
        {
            var guarded = GeneratorSetupFixture.XmiReaderResult
                .QueryImpliedRelationshipRules()
                .Where(rule => rule.Form == ImpliedRuleForm.GuardedLibrarySpecialization)
                .ToList();

            var parsed = guarded
                .Select(rule => ImpliedGuardParser.Parse(rule.GuardExpression))
                .ToList();

            var translatable = parsed.Count(expression => expression.Shape != ImpliedGuardShape.RequiresHandCoding);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guarded, Has.Count.EqualTo(63), "The number of guarded constraints in the abstract syntax changed.");
                Assert.That(translatable, Is.EqualTo(46), "Guard-shape coverage changed; re-check the patterns against the OCL.");
            }
        }

        /// <summary>
        /// Emits a predicate for every translatable guard in the real constraint set, so a shape that parses
        /// but cannot be rendered — an unknown metaclass, say — is caught here rather than as a compile
        /// failure in the generated assembly.
        /// </summary>
        [Test]
        public void VerifyEveryTranslatableGuardEmitsAPredicate()
        {
            var interfaceFqnByName = QueryInterfaceFqnByName();
            var enumerationFqnByName = QueryEnumerationFqnByName();

            var unrenderable = GeneratorSetupFixture.XmiReaderResult
                .QueryImpliedRelationshipRules()
                .Where(rule => rule.Form == ImpliedRuleForm.GuardedLibrarySpecialization)
                .Select(rule => new
                {
                    rule.ConstraintName,
                    rule.MetaclassName,
                    Expression = ImpliedGuardParser.Parse(rule.GuardExpression)
                })
                .Where(candidate => candidate.Expression.Shape != ImpliedGuardShape.RequiresHandCoding)
                .Where(candidate => !interfaceFqnByName.TryGetValue(candidate.MetaclassName, out var declaringFqn)
                                    || ImpliedGuardEmitter.Emit(candidate.Expression, declaringFqn, interfaceFqnByName, enumerationFqnByName) == null)
                .Select(candidate => candidate.ConstraintName)
                .ToList();

            Assert.That(unrenderable, Is.Empty, $"These guards parse but emit no predicate: {string.Join(", ", unrenderable)}");
        }

        private static Dictionary<string, string> QueryInterfaceFqnByName()
        {
            return GeneratorSetupFixture.XmiReaderResult
                .QueryContainedAndImported("SysML")
                .SelectMany(package => package.PackagedElement.OfType<IClass>())
                .GroupBy(umlClass => umlClass.Name, StringComparer.Ordinal)
                .ToDictionary(group => group.Key, group => group.First().QueryFullyQualifiedTypeName(), StringComparer.Ordinal);
        }

        private static Dictionary<string, string> QueryEnumerationFqnByName()
        {
            return GeneratorSetupFixture.XmiReaderResult
                .QueryContainedAndImported("SysML")
                .SelectMany(package => package.PackagedElement.OfType<IEnumeration>())
                .GroupBy(enumeration => enumeration.Name, StringComparer.Ordinal)
                .ToDictionary(group => group.Key, group => group.First().QueryFullyQualifiedTypeName(), StringComparer.Ordinal);
        }
    }
}
