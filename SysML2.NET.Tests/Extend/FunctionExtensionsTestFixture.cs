// -------------------------------------------------------------------------------------------------
// <copyright file="FunctionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class FunctionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeResult()
        {
            // Null subject:
            Assert.That(() => ((IFunction)null).ComputeResult(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no ReturnParameterMembership → null.
            var emptySubject = new Function();
            Assert.That(emptySubject.ComputeResult(), Is.Null);

            // Negative: a FeatureMembership that is NOT a ReturnParameterMembership → null.
            var negativeSubject = new Function();
            var plainMembership = new FeatureMembership();
            var plainFeature = new Feature();
            negativeSubject.AssignOwnership(plainMembership, plainFeature);
            Assert.That(negativeSubject.ComputeResult(), Is.Null);

            // Positive: one ReturnParameterMembership whose ownedMemberParameter is a Feature → that feature.
            var subject = new Function();
            var resultFeature = new Feature();
            var returnParameterMembership = new ReturnParameterMembership();
            subject.AssignOwnership(returnParameterMembership, resultFeature);
            Assert.That(subject.ComputeResult(), Is.SameAs(resultFeature));

            // Two ReturnParameterMemberships → the FIRST is returned (OCL ->first()).
            var secondResultFeature = new Feature();
            var secondReturnParameterMembership = new ReturnParameterMembership();
            subject.AssignOwnership(secondReturnParameterMembership, secondResultFeature);
            Assert.That(subject.ComputeResult(), Is.SameAs(resultFeature));
        }

        [Test]
        public void VerifyComputeExpression()
        {
            // Null subject:
            Assert.That(() => ((IFunction)null).ComputeExpression(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no features → empty.
            var emptySubject = new Function();
            Assert.That(emptySubject.ComputeExpression(), Is.Empty);

            // Kind filter: an Expression feature plus a non-Expression feature → only the Expression.
            var subject = new Function();
            var expressionFeature = new Expression();
            var plainFeature = new Feature();
            subject.AssignOwnership(new FeatureMembership(), expressionFeature);
            subject.AssignOwnership(new FeatureMembership(), plainFeature);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeExpression(), Does.Contain(expressionFeature));
                Assert.That(subject.ComputeExpression(), Does.Not.Contain(plainFeature));
                Assert.That(subject.ComputeExpression(), Has.Count.EqualTo(1));
            }
        }

        [Test]
        public void VerifyComputeIsModelLevelEvaluable()
        {
            // Null subject:
            Assert.That(() => ((IFunction)null).ComputeIsModelLevelEvaluable(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no owning namespace and no name → not a library function.
            Assert.That(new Function().ComputeIsModelLevelEvaluable(), Is.False);

            // Positive: the operators KerML Table 5 and Table 7 mark as model-level evaluable.
            using (Assert.EnterMultipleScope())
            {
                Assert.That(LibraryFunction("BaseFunctions", "==").ComputeIsModelLevelEvaluable(), Is.True);
                Assert.That(LibraryFunction("BaseFunctions", "as").ComputeIsModelLevelEvaluable(), Is.True);
                Assert.That(LibraryFunction("BaseFunctions", "#").ComputeIsModelLevelEvaluable(), Is.True);
                Assert.That(LibraryFunction("DataFunctions", "^").ComputeIsModelLevelEvaluable(), Is.True);
                Assert.That(LibraryFunction("DataFunctions", "**").ComputeIsModelLevelEvaluable(), Is.True);
                Assert.That(LibraryFunction("ControlFunctions", "select").ComputeIsModelLevelEvaluable(), Is.True);
                Assert.That(LibraryFunction("ControlFunctions", ".").ComputeIsModelLevelEvaluable(), Is.True);
            }

            // Negative: the three operators the tables mark as NOT model-level evaluable.
            using (Assert.EnterMultipleScope())
            {
                Assert.That(LibraryFunction("BaseFunctions", "all").ComputeIsModelLevelEvaluable(), Is.False);
                Assert.That(LibraryFunction("BaseFunctions", "[").ComputeIsModelLevelEvaluable(), Is.False);
                Assert.That(LibraryFunction("DataFunctions", "~").ComputeIsModelLevelEvaluable(), Is.False);
            }

            // Negative: library functions that no operator maps to, and a function outside the library.
            using (Assert.EnterMultipleScope())
            {
                Assert.That(LibraryFunction("DataFunctions", "max").ComputeIsModelLevelEvaluable(), Is.False);
                Assert.That(LibraryFunction("ControlFunctions", "reduce").ComputeIsModelLevelEvaluable(), Is.False);
                Assert.That(LibraryFunction("BaseFunctions", "ToString").ComputeIsModelLevelEvaluable(), Is.False);
                Assert.That(LibraryFunction("SomePackage", "==").ComputeIsModelLevelEvaluable(), Is.False);
                Assert.That(LibraryFunction("BaseFunctions", null).ComputeIsModelLevelEvaluable(), Is.False);
            }

            // '==' and '===' are declared by BaseFunctions AND DataFunctions; the probe order elects BaseFunctions.
            using (Assert.EnterMultipleScope())
            {
                Assert.That(LibraryFunction("DataFunctions", "==").ComputeIsModelLevelEvaluable(), Is.False);
                Assert.That(LibraryFunction("DataFunctions", "===").ComputeIsModelLevelEvaluable(), Is.False);
            }

            static IFunction LibraryFunction(string packageName, string functionName)
            {
                var libraryPackage = new Namespace { DeclaredName = packageName };
                var function = new Function { DeclaredName = functionName };
                libraryPackage.AssignOwnership(new OwningMembership(), function);

                return function;
            }
        }
    }
}
