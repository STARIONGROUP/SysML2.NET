// -------------------------------------------------------------------------------------------------
// <copyright file="IfActionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.Actions;

    /// <summary>
    /// Hand-coded part of the <see cref="IfActionUsageTextualNotationBuilder"/>
    /// </summary>
    public static partial class IfActionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the conditional part for the IfNode rule
        /// </summary>
        /// <param name="poco">The <see cref="IIfActionUsage"/></param>
        /// <returns>The assertion of the condition</returns>
        private static bool BuildGroupConditionForIfNode(IIfActionUsage poco)
        {
            return poco.OwnedRelationship.OfType<IParameterMembership>().Count() != 0;
        }
    }
}
