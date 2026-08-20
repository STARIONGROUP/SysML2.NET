// -------------------------------------------------------------------------------------------------
// <copyright file="NotationInvariants.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.CommonStructure;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// The OMG names the textual-notation generator depends on for rules that neither the KEBNF nor the
    /// metamodel states machine-readably.
    /// </summary>
    /// <remarks>
    /// Some notation rules cannot be derived. The grammar describes token order, and the metamodel describes
    /// structure, but neither records that a KEYWORD already conveys a property, nor that a member is always
    /// singular. The generator therefore has to know a small number of OMG names outright.
    /// <para>Holding them here rather than inline achieves the two things that matter on an OMG release:
    /// every dependency is auditable in ONE place with its justification, and a name that stops resolving is
    /// REPORTED (<see cref="QueryUnresolvedInvariants" />) instead of silently disabling the rule it backs —
    /// which is how the emission bugs these invariants fix would quietly return.</para>
    /// <para>Prefer derivation whenever it exists; add an entry only when it does not. Two derivations were
    /// tried and rejected for the direction rule below: the metamodel's own
    /// <c>ParameterMembership::parameterDirection()</c> is polymorphic but returns <c>in</c> on the base, so
    /// suppressing whenever the direction matches it would strip every <c>in</c> parameter; and the "must be
    /// out" statement on ReturnParameterMembership is class documentation, not one of its OCL constraints.</para>
    /// </remarks>
    public static class NotationInvariants
    {
        /// <summary>
        /// The metaclass of a member that the enclosing rule always consumes on its own.
        /// </summary>
        public const string ResultMemberMetaclass = "ResultMemberMetaclass";

        /// <summary>
        /// The Feature property whose value a result member's own keyword already conveys.
        /// </summary>
        public const string ImpliedDirectionProperty = "ImpliedDirectionProperty";

        /// <summary>
        /// The OMG names the generator depends on, keyed by the concept the generator refers to.
        /// </summary>
        private static readonly NotationInvariant[] Entries =
        [
            new(ResultMemberMetaclass, "ReturnParameterMembership",
                "The grammar never repeats a result member: it always gives one its OWN slot in the enclosing rule (EmptyResultMember, ConstructorResultMember, ReturnParameterMember), never a comma-separated repetition. A repeated '+=' member that shares the enclosing rule's cursor therefore has to exclude it, because the repetition's declared item type is one of its supertypes — ArgumentMember is a ParameterMembership and EmptyResultMember is a ReturnParameterMembership, so the loop consumed the result member, emitted a separator and then rendered nothing: 'f(a, )'."),
            new(ImpliedDirectionProperty, "direction",
                "The 'return' keyword of ReturnParameterMember already says the parameter is the result, and a result parameter always carries direction = out, so writing the direction as well emits 'return out verdict' where the notation is 'return verdict'. The exclusion cannot go through QuerySubclassesWithMatchingDefault, which suppresses a keyword whose metamodel DEFAULT already matches: Feature::direction is [0..1] and declares no default, so there is nothing to compare against and a null direction genuinely means undirected.")
        ];

        /// <summary>
        /// The invariants whose OMG name resolved against the metamodel during this generator run.
        /// </summary>
        private static readonly HashSet<string> ResolvedNames = [];

        /// <summary>
        /// Returns the OMG name an invariant depends on.
        /// </summary>
        /// <param name="invariantName">The invariant's stable key.</param>
        /// <returns>The OMG name, or <see langword="null" /> when no entry carries that key.</returns>
        public static string QueryMetamodelName(string invariantName)
        {
            return Entries.SingleOrDefault(invariant => string.Equals(invariant.Name, invariantName, StringComparison.Ordinal))?.MetamodelName;
        }

        /// <summary>
        /// Resolves the metaclass an invariant depends on, recording that its name still exists.
        /// </summary>
        /// <param name="invariantName">The invariant's stable key.</param>
        /// <param name="cacheSource">Any <see cref="IClass" /> from the loaded model, used to reach the cache.</param>
        /// <returns>The resolved <see cref="IClass" />, or <see langword="null" /> when it no longer exists.</returns>
        /// <remarks>
        /// A null return disables the rule the invariant backs, which is why the miss is recorded rather than
        /// swallowed: <see cref="QueryUnresolvedInvariants" /> turns it into a message on the next run.
        /// </remarks>
        public static IClass QueryMetaclass(string invariantName, IClass cacheSource)
        {
            var metamodelName = QueryMetamodelName(invariantName);

            if (metamodelName == null || cacheSource == null)
            {
                return null;
            }

            var metaclass = RuleQueryUtilities.FindClass(cacheSource.Cache, metamodelName);

            if (metaclass != null)
            {
                ResolvedNames.Add(invariantName);
            }

            return metaclass;
        }

        /// <summary>
        /// Records that an invariant's property name still matches a property the grammar assigns.
        /// </summary>
        /// <param name="invariantName">The invariant's stable key.</param>
        public static void MarkResolved(string invariantName)
        {
            if (!string.IsNullOrWhiteSpace(invariantName))
            {
                ResolvedNames.Add(invariantName);
            }
        }

        /// <summary>
        /// Returns the invariants whose OMG name resolved against nothing during this generator run.
        /// </summary>
        /// <returns>The unresolved entries, whose rules are consequently NOT being applied.</returns>
        /// <remarks>
        /// Only meaningful once generation has completed. An unresolved entry means OMG renamed or removed
        /// the name the invariant hangs on, so the emission rule it backs is silently off and the entry needs
        /// re-anchoring — not pruning, since the underlying notation rule still holds.
        /// </remarks>
        public static IReadOnlyList<NotationInvariant> QueryUnresolvedInvariants()
        {
            return [..Entries.Where(invariant => !ResolvedNames.Contains(invariant.Name))];
        }
    }
}
