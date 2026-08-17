// -------------------------------------------------------------------------------------------------
// <copyright file="ArgumentResultSpecializationRule.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Base for a rule whose result parameter subsets the result of the Expression's FIRST argument.
    /// </summary>
    /// <remarks>
    /// The target is a <c>result</c> parameter, hence a Feature, so the Specialization is a Subsetting —
    /// Feature-to-Feature admits no other kind. This is why the family does NOT need the
    /// Classifier-or-Feature test that <see cref="ConstructorExpressionResultSpecializationRule" /> makes.
    /// <para>The OCL elects <c>arguments-&gt;first()</c> explicitly, so taking the first of many is the
    /// contract rather than an arbitrary pick.</para>
    /// </remarks>
    public abstract class ArgumentResultSpecializationRule : IImpliedRelationshipRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentResultSpecializationRule" /> class.
        /// </summary>
        /// <param name="factory">The factory creating the detached Subsetting.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory" /> is null.</exception>
        protected ArgumentResultSpecializationRule(IImpliedRelationshipFactory factory)
        {
            this.Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public abstract string ConstraintName { get; }

        /// <summary>
        /// Gets the factory creating the detached Subsetting.
        /// </summary>
        protected IImpliedRelationshipFactory Factory { get; }

        /// <summary>
        /// Computes the Subsetting binding the Expression's result to its first argument's result.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Subsetting, or empty when the constraint does not apply.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public IReadOnlyList<IRelationship> Apply(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (!this.IsInScope(element) || element is not IInstantiationExpression { result: not null } expression)
            {
                return [];
            }

            var firstArgumentResult = expression.argument.Count == 0 ? null : expression.argument[0].result;

            return firstArgumentResult == null || !this.AppliesTo(firstArgumentResult)
                ? []
                : [this.Factory.CreateImpliedSubsetting(expression.result, firstArgumentResult)];
        }

        /// <summary>
        /// Asserts whether the Element is the metaclass this rule constrains.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the rule applies to the Element's metaclass.</returns>
        protected abstract bool IsInScope(IElement element);

        /// <summary>
        /// Asserts any further condition the constraint places on the first argument's result.
        /// </summary>
        /// <param name="firstArgumentResult">The result parameter of the first argument Expression.</param>
        /// <returns>True when the Subsetting is required; the base implementation always agrees.</returns>
        protected virtual bool AppliesTo(SysML2.NET.Core.POCO.Core.Features.IFeature firstArgumentResult) => true;
    }
}
