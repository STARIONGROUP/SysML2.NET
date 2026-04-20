// -------------------------------------------------------------------------------------------------
// <copyright file="PackageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class PackageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFilterCondition()
        {
            Assert.That(() => ((IPackage)null).ComputeFilterCondition(), Throws.TypeOf<ArgumentNullException>());

            var package = new Package();

            Assert.That(package.ComputeFilterCondition(), Is.Empty);
            var membership = new ElementFilterMembership();
            var expression = new BooleanExpression();

            var annotation = new Annotation();
            var comment = new Comment();
            
            package.AssignOwnership(membership, expression);
            package.AssignOwnership(annotation, comment);
            
            Assert.That(package.ComputeFilterCondition, Throws.InstanceOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRedefinedImportedMembershipsOperation()
        {
            Assert.That(() => ((IPackage)null).ComputeRedefinedImportedMembershipsOperation([]), Throws.TypeOf<ArgumentNullException>());

            var package = new Package();

            Assert.That(package.ComputeRedefinedImportedMembershipsOperation([]), Is.Empty);

            var importMember = new MembershipImport();
            var type = new Type();
            
            package.AssignOwnership(importMember, type);
            Assert.That(()=> package.ComputeRedefinedImportedMembershipsOperation([]), Throws.InstanceOf<NotSupportedException>());
            
            var membership = new ElementFilterMembership();
            var expression = new BooleanExpression();
            package.AssignOwnership(membership, expression);
            Assert.That(()=> package.ComputeRedefinedImportedMembershipsOperation([]), Throws.InstanceOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeIncludeAsMemberOperation()
        {
            Assert.That(() => ((IPackage)null).ComputeIncludeAsMemberOperation(null), Throws.TypeOf<ArgumentNullException>());

            var package = new Package();
            Assert.That(package.ComputeIncludeAsMemberOperation(null), Is.False);

            var element = new Type();
            Assert.That(package.ComputeIncludeAsMemberOperation(element), Is.True);
            var membership = new ElementFilterMembership();
            var expression = new BooleanExpression();
            
            package.AssignOwnership(membership, expression);
            
            Assert.That(() => package.ComputeIncludeAsMemberOperation(element), Throws.TypeOf<NotSupportedException>());
        }
    }
}
