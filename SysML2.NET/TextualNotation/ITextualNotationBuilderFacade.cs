// -------------------------------------------------------------------------------------------------
// <copyright file="ITextualNotationBuilderFacade.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ITextualNotationBuilderFacade"/> provides access to built textual notation for <see cref="IElement" /> via <see cref="TextualNotationBuilder{TElement}" />
    /// </summary>
    public interface ITextualNotationBuilderFacade
    {
        /// <summary>
        /// Queries the Textual Notation of an <see cref="IElement"/> 
        /// </summary>
        /// <param name="element">The <see cref="IElement"/> to built textual notation from</param>
        /// <returns>The built textual notation string</returns>
        string QueryTextualNotationOfElement(IElement element);
    }
}
