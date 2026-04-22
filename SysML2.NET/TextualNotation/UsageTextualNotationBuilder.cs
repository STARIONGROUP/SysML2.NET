// -------------------------------------------------------------------------------------------------
// <copyright file="UsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Text;

    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// Hand-coded part of the <see cref="UsageTextualNotationBuilder" />
    /// </summary>
    public static partial class UsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ActionTargetSuccession
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildActionTargetSuccessionHandCoded(IUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildActionTargetSuccessionHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceOccurrenceUsageElement
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildInterfaceOccurrenceUsageElementHandCoded(IUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildInterfaceOccurrenceUsageElementHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsageElement
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildOccurrenceUsageElementHandCoded(IUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildOccurrenceUsageElementHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StructureUsageElement
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildStructureUsageElementHandCoded(IUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildStructureUsageElementHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule VariantUsageElement
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildVariantUsageElementHandCoded(IUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildVariantUsageElementHandCoded requires manual implementation");
        }
    }
}
