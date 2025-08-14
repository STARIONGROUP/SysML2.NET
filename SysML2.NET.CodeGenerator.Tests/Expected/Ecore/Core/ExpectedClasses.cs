// -------------------------------------------------------------------------------------------------
// <copyright file="ExpectedClasses.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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
            return GetEnumerator();
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
            return GetEnumerator();
        }
    }
}
