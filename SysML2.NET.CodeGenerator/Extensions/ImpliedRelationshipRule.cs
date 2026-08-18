// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipRule.cs" company="Starion Group S.A.">
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
    /// <summary>
    /// The category of semantic constraint, per KerML 1.0 §8.4.2.
    /// </summary>
    public enum ImpliedConstraintCategory
    {
        /// <summary>
        /// Requires a Type to directly or indirectly specialize a base Type, normally from a model library.
        /// </summary>
        Specialization,

        /// <summary>
        /// Requires a Redefinition between two Features of a user model.
        /// </summary>
        Redefinition,

        /// <summary>
        /// Requires a TypeFeaturing between a Feature and a Type of a user model.
        /// </summary>
        TypeFeaturing,

        /// <summary>
        /// Requires a BindingConnector to exist between two Features of a user model.
        /// </summary>
        BindingConnector
    }

    /// <summary>
    /// How much of a semantic constraint can be turned into generated code.
    /// </summary>
    public enum ImpliedRuleForm
    {
        /// <summary>
        /// The whole OCL body is <c>specializesFromLibrary('X::y')</c> — fully generatable.
        /// </summary>
        UnconditionalLibrarySpecialization,

        /// <summary>
        /// The OCL body is <c>&lt;guard&gt; implies specializesFromLibrary('X::y')</c> — the target is
        /// generatable, the guard needs a hand-written predicate.
        /// </summary>
        GuardedLibrarySpecialization,

        /// <summary>
        /// The constraint relates user-model elements, or its OCL is not in a mechanically extractable
        /// shape; the whole rule needs hand-coding.
        /// </summary>
        RequiresHandCoding,

        /// <summary>
        /// The specification itself leaves the OCL body as <c>TBD</c>, so there is nothing to implement.
        /// </summary>
        SpecificationTbd
    }

    /// <summary>
    /// A single semantic constraint that may be satisfied by inserting an implied <c>Relationship</c>.
    /// </summary>
    public sealed class ImpliedRelationshipRule
    {
        /// <summary>
        /// Gets the name of the constraint as declared in the XMI, e.g. <c>checkPortUsageSpecialization</c>.
        /// </summary>
        public string ConstraintName { get; init; }

        /// <summary>
        /// Gets the name of the metaclass the constraint is declared on.
        /// </summary>
        public string MetaclassName { get; init; }

        /// <summary>
        /// Gets the §8.4.2 category of the constraint.
        /// </summary>
        public ImpliedConstraintCategory Category { get; init; }

        /// <summary>
        /// Gets how much of the constraint can be generated.
        /// </summary>
        public ImpliedRuleForm Form { get; init; }

        /// <summary>
        /// Gets the qualified name of the library Type that must be specialized, or null when the constraint
        /// does not target a library element.
        /// </summary>
        public string TargetLibraryName { get; init; }

        /// <summary>
        /// Gets the OCL guard that gates the specialization, or null when the constraint is unconditional.
        /// </summary>
        public string GuardExpression { get; init; }

        /// <summary>
        /// Gets the whitespace-normalised OCL body the rule was extracted from.
        /// </summary>
        public string Ocl { get; init; }
    }
}
