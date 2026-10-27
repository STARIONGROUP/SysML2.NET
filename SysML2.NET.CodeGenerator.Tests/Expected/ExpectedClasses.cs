// ------------------------------------------------------------------------------------------------
// <copyright file="ExpectedClasses.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Expected.Ecore.Core
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// provides a list of the expected Concrete classes
    /// </summary>
    public class ExpectedConcreteClasses : IEnumerable<string>
    {
        /// <summary>
        /// returns the list of class names
        /// </summary>
        /// <returns>
        /// list of class names
        /// </returns>
        public IEnumerator<string> GetEnumerator()
        {
            yield return "AnnotatingElement";
            yield return "Association";
            yield return "Dependency";
            yield return "EnumerationDefinition";
            yield return "Feature";
            yield return "FeatureTyping";
            yield return "Flow";
            yield return "FramedConcernMembership";
            yield return "LiteralInteger";
            yield return "LiteralRational";
            yield return "Membership";
            yield return "MultiplicityRange";
            yield return "OwningMembership";
            yield return "ReferenceSubsetting";
            yield return "RequirementUsage";
            yield return "SelectExpression";
            yield return "Subclassification";
            yield return "TextualRepresentation";
            yield return "Usage";
        }

        /// <summary>
        /// returns the list of class names
        /// </summary>
        /// <returns>
        /// list of class names
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// provides a list of the all expected Abstract and Concrete classes
    /// </summary>
    public class ExpectedAllClasses : IEnumerable<string>
    {
        /// <summary>
        /// returns the list of class names
        /// </summary>
        /// <returns>
        /// list of class names
        /// </returns>
        public IEnumerator<string> GetEnumerator()
        {
            yield return "AnnotatingElement";
            yield return "Association";
            yield return "Dependency";
            yield return "Element";
            yield return "EnumerationDefinition";
            yield return "Feature";
            yield return "FeatureTyping";
            yield return "Flow";
            yield return "FramedConcernMembership";
            yield return "LiteralInteger";
            yield return "LiteralRational";
            yield return "Membership";
            yield return "MultiplicityRange";
            yield return "OwningMembership";
            yield return "ReferenceSubsetting";
            yield return "Relationship";
            yield return "RequirementUsage";
            yield return "SelectExpression";
            yield return "Subclassification";
            yield return "TextualRepresentation";
            yield return "Usage";
        }

        /// <summary>
        /// returns the list of class names
        /// </summary>
        /// <returns>
        /// list of class names
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
