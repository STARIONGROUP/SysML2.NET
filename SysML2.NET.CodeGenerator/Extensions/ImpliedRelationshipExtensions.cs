// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using uml4net.CommonStructure;
    using uml4net.StructuredClassifiers;
    using uml4net.Values;
    using uml4net.xmi.Readers;

    /// <summary>
    /// Extracts the KerML/SysML <i>semantic constraints</i> that a tool may satisfy by inserting implied
    /// <c>Relationships</c>, as described in KerML 1.0 §8.4.2.
    /// </summary>
    /// <remarks>
    /// §8.4.2 names four categories of semantic constraint and fixes a naming convention for each — the
    /// constraint name always contains the word <c>Specialization</c>, <c>Redefinition</c>,
    /// <c>TypeFeaturing</c> or <c>BindingConnector</c>. Only the <c>check</c>-prefixed rules are semantic
    /// constraints; <c>derive</c> rules are derivations and <c>validate</c> rules are validation constraints
    /// (§8.3.1), and neither implies a Relationship.
    /// <para>
    /// The normative catalogue of what to insert for each constraint is the set of tables in KerML §8.4.3.1.1
    /// (Tables 8, 9), §8.4.4.1 (Tables 10, 11) and SysML §Tables 31-33. This extractor reads the machine-readable
    /// XMI as a proxy for those tables. Where the two disagree, the tables win — reconciling them is a
    /// deliberate follow-up, and <see cref="ImpliedRuleForm.RequiresHandCoding"/> exists so that no constraint
    /// is silently dropped in the meantime.
    /// </para>
    /// </remarks>
    public static class ImpliedRelationshipExtensions
    {
        /// <summary>
        /// Matches an OCL body that is nothing but a library specialization, e.g.
        /// <c>specializesFromLibrary('Ports::ports')</c>.
        /// </summary>
        private static readonly Regex UnconditionalLibraryPattern =
            new(@"^specializesFromLibrary\('(?<target>[^']+)'\)$", RegexOptions.Compiled);

        /// <summary>
        /// Matches an OCL body of the form <c>&lt;guard&gt; implies specializesFromLibrary('X::y')</c>. The
        /// guard is captured verbatim; translating it into a C# predicate is hand-work, but the TARGET is
        /// still extracted mechanically.
        /// </summary>
        private static readonly Regex GuardedLibraryPattern =
            new(@"^(?<guard>.+?)\bimplies\b\s*specializesFromLibrary\('(?<target>[^']+)'\)$", RegexOptions.Compiled);

        /// <summary>
        /// The four category keywords of KerML §8.4.2, in the order the specification lists them.
        /// </summary>
        private static readonly (string Keyword, ImpliedConstraintCategory Category)[] CategoryKeywords =
        [
            ("Specialization", ImpliedConstraintCategory.Specialization),
            ("Redefinition", ImpliedConstraintCategory.Redefinition),
            ("TypeFeaturing", ImpliedConstraintCategory.TypeFeaturing),
            ("BindingConnector", ImpliedConstraintCategory.BindingConnector)
        ];

        /// <summary>
        /// Extracts every semantic constraint that may be satisfied by an implied <c>Relationship</c>, from
        /// every <see cref="IClass"/> reachable from the merged model.
        /// </summary>
        /// <param name="xmiReaderResult">
        /// The <see cref="XmiReaderResult"/> holding the merged KerML + SysML model
        /// </param>
        /// <returns>
        /// The extracted rules, ordered by metaclass then constraint name
        /// </returns>
        public static IReadOnlyList<ImpliedRelationshipRule> QueryImpliedRelationshipRules(this XmiReaderResult xmiReaderResult)
        {
            ArgumentNullException.ThrowIfNull(xmiReaderResult);

            var rules = new List<ImpliedRelationshipRule>();

            foreach (var umlClass in xmiReaderResult.QueryContainedAndImported("SysML").SelectMany(package => package.PackagedElement.OfType<IClass>()))
            {
                rules.AddRange(umlClass.OwnedRule
                    .Where(IsSemanticConstraint)
                    .Select(rule => CreateRule(umlClass, rule))
                    .Where(rule => rule != null));
            }

            return
            [
                ..rules
                    .OrderBy(rule => rule.MetaclassName, StringComparer.Ordinal)
                    .ThenBy(rule => rule.ConstraintName, StringComparer.Ordinal)
            ];
        }

        /// <summary>
        /// Determines whether a constraint is one of the §8.4.2 semantic constraints — a <c>check</c> rule
        /// whose name carries one of the four category keywords.
        /// </summary>
        /// <param name="constraint">
        /// The <see cref="IConstraint"/> to test
        /// </param>
        /// <returns>
        /// True when the constraint may imply a <c>Relationship</c>
        /// </returns>
        private static bool IsSemanticConstraint(IConstraint constraint)
        {
            return !string.IsNullOrWhiteSpace(constraint.Name)
                   && constraint.Name.StartsWith("check", StringComparison.Ordinal)
                   && CategoryKeywords.Any(candidate => constraint.Name.Contains(candidate.Keyword, StringComparison.Ordinal));
        }

        /// <summary>
        /// Projects a single constraint into an <see cref="ImpliedRelationshipRule"/>, classifying how far the
        /// OCL body can be turned into generated code.
        /// </summary>
        /// <param name="umlClass">
        /// The <see cref="IClass"/> the constraint is declared on
        /// </param>
        /// <param name="constraint">
        /// The <see cref="IConstraint"/> to project
        /// </param>
        /// <returns>
        /// The rule, or null when the constraint carries no OCL body at all
        /// </returns>
        private static ImpliedRelationshipRule CreateRule(IClass umlClass, IConstraint constraint)
        {
            var ocl = QueryOclBody(constraint);

            if (ocl == null)
            {
                return null;
            }

            var category = CategoryKeywords.First(candidate => constraint.Name.Contains(candidate.Keyword, StringComparison.Ordinal)).Category;

            var (form, target, guard) = ClassifyOcl(ocl, category);

            return new ImpliedRelationshipRule
            {
                ConstraintName = constraint.Name,
                MetaclassName = umlClass.Name,
                Category = category,
                Form = form,
                TargetLibraryName = target,
                GuardExpression = guard,
                Ocl = ocl
            };
        }

        /// <summary>
        /// Classifies an OCL body into the form that decides how much of the rule can be generated.
        /// </summary>
        /// <param name="ocl">
        /// The normalised OCL body
        /// </param>
        /// <param name="category">
        /// The §8.4.2 category the constraint belongs to
        /// </param>
        /// <returns>
        /// The form, the library target when there is one, and the guard when there is one
        /// </returns>
        private static (ImpliedRuleForm Form, string Target, string Guard) ClassifyOcl(string ocl, ImpliedConstraintCategory category)
        {
            if (string.Equals(ocl, "TBD", StringComparison.OrdinalIgnoreCase))
            {
                return (ImpliedRuleForm.SpecificationTbd, null, null);
            }

            // Only specialization constraints target the model libraries by qualified name; redefinition,
            // type-featuring and binding-connector constraints relate user-model elements to each other and
            // have no mechanically extractable target (§8.4.2 categories 2-4).
            if (category != ImpliedConstraintCategory.Specialization)
            {
                return (ImpliedRuleForm.RequiresHandCoding, null, null);
            }

            var unconditional = UnconditionalLibraryPattern.Match(ocl);

            if (unconditional.Success)
            {
                return (ImpliedRuleForm.UnconditionalLibrarySpecialization, unconditional.Groups["target"].Value, null);
            }

            var guarded = GuardedLibraryPattern.Match(ocl);

            return guarded.Success
                ? (ImpliedRuleForm.GuardedLibrarySpecialization, guarded.Groups["target"].Value, guarded.Groups["guard"].Value.Trim())
                : (ImpliedRuleForm.RequiresHandCoding, null, null);
        }

        /// <summary>
        /// Returns the constraint's OCL body as a single whitespace-normalised line, or null when the
        /// constraint carries no non-blank body.
        /// </summary>
        /// <param name="constraint">
        /// The <see cref="IConstraint"/> to read
        /// </param>
        /// <returns>
        /// The normalised OCL, or null
        /// </returns>
        private static string QueryOclBody(IConstraint constraint)
        {
            var opaqueExpression = constraint.Specification?.OfType<IOpaqueExpression>().FirstOrDefault();

            var body = opaqueExpression?.Body?.FirstOrDefault(candidate => !string.IsNullOrWhiteSpace(candidate));

            // Corrected before classification, so the target, the guard and the recorded OCL all agree.
            return body == null
                ? null
                : OclErrata.Apply(string.Join(' ', body.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)));
        }
    }
}
