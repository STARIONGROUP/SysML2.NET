// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Buffers;

    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// Hand-coded part of the <see cref="FeatureInvertingTextualNotationBuilder"/>
    /// </summary>
    public static partial class FeatureInvertingTextualNotationBuilder
    {
        /// <summary>
        /// Builds the conditional part for the FeatureInverting rule
        /// </summary>
        /// <param name="poco">The <see cref="IFeatureInverting"/></param>
        /// <returns>The assertion of the condition</returns>
        private static bool BuildGroupConditionForFeatureInverting(IFeatureInverting poco)
        {
            return CommonTextualNotationBuilder.DoesDefinesIdentificationProperties(poco);
        }
    }
}
