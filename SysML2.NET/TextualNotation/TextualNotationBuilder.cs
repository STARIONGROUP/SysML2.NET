// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// Base class that provides Textual Notation string builder for a specific <typeparamref name="TElement"/>
    /// </summary>
    /// <typeparam name="TElement">Any <see cref="TElement"/> concrete class</typeparam>
    public abstract class TextualNotationBuilder<TElement> where TElement : class, IElement
    {
        /// <summary>
        /// Gets the <see cref="ITextualNotationBuilderFacade"/> used to query textual notation of referenced <see cref="IElement"/>
        /// </summary>
        protected readonly ITextualNotationBuilderFacade Facade;

        /// <summary>
        /// Initializes a new instance of a <see cref="TextualNotationBuilder{TElement}"/>
        /// </summary>
        /// <param name="facade">The <see cref="ITextualNotationBuilderFacade"/> used to query textual notation of referenced <see cref="IElement"/></param>
        protected TextualNotationBuilder(ITextualNotationBuilderFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Builds the Textual Notation string for the provided <typeparamref name="TElement" />
        /// </summary>
        /// <param name="poco">The <typeparamref name="TElement" /> from which the textual notation should be build</param>
        /// <returns>The built textual notation string</returns>
        public abstract string BuildTextualNotation(TElement poco);
    }
}
