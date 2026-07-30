// -------------------------------------------------------------------------------------------------
// <copyright file="OperatorExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class OperatorExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedInstantiatedTypeOperation()
        {
            // Branch 1: null subject -> ArgumentNullException.
            Assert.That(
                () => ((IOperatorExpression)null).ComputeRedefinedInstantiatedTypeOperation(),
                Throws.TypeOf<ArgumentNullException>());

            // Branch 2: populated with an Operator symbol but NO loaded Kernel Function Library
            // (BaseFunctions / DataFunctions / ControlFunctions). resolveGlobal(...) returns null for each
            // of the three namespaces, so the collected sequence is empty -> the method returns null.
            var operatorSubject = new OperatorExpression { Operator = "+" };

            // Branch 3: Operator unset (null) -> the built qualified names still resolve to nothing -> null,
            // and, critically, no crash while composing the "ns::'<operator>'" lookup key.
            var operatorlessSubject = new OperatorExpression();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(operatorSubject.ComputeRedefinedInstantiatedTypeOperation(), Is.Null);
                Assert.That(operatorlessSubject.ComputeRedefinedInstantiatedTypeOperation(), Is.Null);
            }

            // Branch 4 (positive resolveGlobal path): wire a resolvable global scope so
            // ResolveGlobal("BaseFunctions::'+'") returns a real Membership whose MemberElement is an IType,
            // exercising the "resolveGlobal -> ?.MemberElement as IType -> return the Type" bulk of the method.
            // The quoted operator segment "'+'" is unescaped by UnqualifiedNameOf to the raw name "+", so the
            // resolved member's DeclaredName must be "+".
            var root = new Namespace();

            var baseFunctions = new Namespace { DeclaredName = "BaseFunctions" };
            root.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, baseFunctions);

            var basePlusFunction = new Behavior { DeclaredName = "+" };
            baseFunctions.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, basePlusFunction);

            // DataFunctions also carries a "+" function; because the method probes BaseFunctions first and
            // takes the first non-null resolution, the BaseFunctions one must win (namespace precedence).
            var dataFunctions = new Namespace { DeclaredName = "DataFunctions" };
            root.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, dataFunctions);

            var dataPlusFunction = new Behavior { DeclaredName = "+" };
            dataFunctions.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, dataPlusFunction);

            // The subject must sit under the same root so its owningNamespace chain reaches the global scope.
            var resolvableOperator = new OperatorExpression { Operator = "+" };
            root.AssignOwnership(new OwningMembership { Visibility = VisibilityKind.Public }, resolvableOperator);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(resolvableOperator.ComputeRedefinedInstantiatedTypeOperation(), Is.SameAs(basePlusFunction));
                Assert.That(resolvableOperator.ComputeRedefinedInstantiatedTypeOperation(), Is.Not.SameAs(dataPlusFunction));
            }
        }
    }
}
