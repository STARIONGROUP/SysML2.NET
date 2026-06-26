// -------------------------------------------------------------------------------------------------
// <copyright file="BooleanExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class BooleanExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputePredicate()
        {
            // Null subject:
            Assert.That(() => ((IBooleanExpression)null).ComputePredicate(), Throws.TypeOf<ArgumentNullException>());

            // Empty BooleanExpression:
            var emptyBooleanExpression = new BooleanExpression();
            Assert.That(emptyBooleanExpression.ComputePredicate(), Is.Null);

            // Typed by Function, but not Predicate (predicate is a specialization of function):
            var nonPredicateBooleanExpression = new BooleanExpression();
            var function = new Function();
            var functionTyping = new FeatureTyping { Type = function };
            nonPredicateBooleanExpression.AssignOwnership(functionTyping);
            Assert.That(nonPredicateBooleanExpression.ComputePredicate(), Is.Null);

            // Typed by Predicate:
            var predicateBooleanExpression = new BooleanExpression();
            var predicate = new Predicate();
            var predicateTyping = new FeatureTyping { Type = predicate };
            predicateBooleanExpression.AssignOwnership(predicateTyping);
            Assert.That(predicateBooleanExpression.ComputePredicate(), Is.SameAs(predicate));

            // Two FeatureTypings whose Type is a Predicate → MultiplicityViolationException (upper-bound
            // violation of the derived [0..1] property).
            var twoPredicateBooleanExpression = new BooleanExpression();
            twoPredicateBooleanExpression.AssignOwnership(new FeatureTyping { Type = new Predicate() });
            twoPredicateBooleanExpression.AssignOwnership(new FeatureTyping { Type = new Predicate() });
            Assert.That(() => twoPredicateBooleanExpression.ComputePredicate(), Throws.TypeOf<MultiplicityViolationException>());
        }
    }
}
