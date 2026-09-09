// -------------------------------------------------------------------------------------------------
// <copyright file="GrammarUnreachableAttribute.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System;

    /// <summary>
    /// Marks a generated <c>Build{Rule}</c> method whose grammar rule has no incoming reference,
    /// directly or transitively, from any other production in the effective SysML v2 textual
    /// grammar rooted at <c>RootNamespace</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class GrammarUnreachableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GrammarUnreachableAttribute"/> class.
        /// </summary>
        /// <param name="reason">Why the grammar rule this method implements cannot be reached.</param>
        public GrammarUnreachableAttribute(string reason)
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Gets why the grammar rule this method implements cannot be reached.
        /// </summary>
        public string Reason { get; }
    }
}
