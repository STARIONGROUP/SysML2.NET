// -------------------------------------------------------------------------------------------------
// <copyright file="SelfBinding.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The binding a reference site declares itself, which does not exist while that declaration is
    /// being written.
    /// </summary>
    internal sealed class SelfBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfBinding" /> class.
        /// </summary>
        /// <param name="scope">The scope the membership binds in.</param>
        /// <param name="membership">The Membership that IS the reference.</param>
        internal SelfBinding(INamespace scope, IMembership membership)
        {
            this.Scope = scope;
            this.Membership = membership;
        }

        /// <summary>
        /// Gets the scope the membership binds in.
        /// </summary>
        internal INamespace Scope { get; }

        /// <summary>
        /// Gets the Membership that IS the reference, and whose binding therefore does not exist yet
        /// at parse time.
        /// </summary>
        internal IMembership Membership { get; }
    }
}
