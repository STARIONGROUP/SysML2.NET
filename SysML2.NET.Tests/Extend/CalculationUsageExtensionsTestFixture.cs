// -------------------------------------------------------------------------------------------------
// <copyright file="CalculationUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extend
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class CalculationUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeCalculationDefinition()
        {
            Assert.That(() => ((ICalculationUsage)null).ComputeCalculationDefinition(), Throws.TypeOf<ArgumentNullException>());

            var emptyCalculationUsage = new CalculationUsage();

            // No FeatureTyping entries → no IFunction type → null (property is [0..1]).
            Assert.That(emptyCalculationUsage.ComputeCalculationDefinition(), Is.Null);

            // A FeatureTyping pointing at a non-Function Type must not satisfy the IFunction filter.
            var nonFunctionSubject = new CalculationUsage();
            var nonFunctionType = new Type();
            nonFunctionSubject.AssignOwnership(new FeatureTyping { Type = nonFunctionType });

            Assert.That(nonFunctionSubject.ComputeCalculationDefinition(), Is.Null);

            // A FeatureTyping pointing at a CalculationDefinition (which implements IFunction) must be returned.
            var subject = new CalculationUsage();
            var calculationDefinition = new CalculationDefinition();
            subject.AssignOwnership(new FeatureTyping { Type = calculationDefinition });

            Assert.That(subject.ComputeCalculationDefinition(), Is.SameAs(calculationDefinition));

            // A FeatureTyping pointing at a kernel Function also satisfies the IFunction filter.
            var functionSubject = new CalculationUsage();
            var kernelFunction = new Function();
            functionSubject.AssignOwnership(new FeatureTyping { Type = kernelFunction });

            Assert.That(functionSubject.ComputeCalculationDefinition(), Is.SameAs(kernelFunction));

            // Two FeatureTypings whose Type satisfies the IFunction filter → MultiplicityViolationException
            // (upper-bound violation of the derived [0..1] property).
            var twoTypingSubject = new CalculationUsage();
            twoTypingSubject.AssignOwnership(new FeatureTyping { Type = new Function() });
            twoTypingSubject.AssignOwnership(new FeatureTyping { Type = new CalculationDefinition() });

            Assert.That(() => twoTypingSubject.ComputeCalculationDefinition(), Throws.TypeOf<MultiplicityViolationException>());
        }

        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            Assert.That(
                () => ((ICalculationUsage)null).ComputeRedefinedModelLevelEvaluableOperation(null),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new CalculationUsage();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation(null), Is.False);
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation(new List<IFeature>()), Is.False);
            }
        }
    }
}
