// -------------------------------------------------------------------------------------------------
// <copyright file="ExpectedClasses.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.CodeGenerator.Tests.Expected
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
            yield return "Annotation";
            yield return "Comment";
            yield return "Connector";
            yield return "Dependency";
            yield return "Element";
            yield return "Feature";
            yield return "NamespaceImport";
            yield return "LiteralInteger";
            yield return "LiteralRational";
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
            yield return "Annotation";
            yield return "Comment";
            yield return "Connector";
            yield return "Dependency";
            yield return "Element";
            yield return "Feature";
            yield return "Import";
            yield return "LiteralInteger";
            yield return "LiteralRational";
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
