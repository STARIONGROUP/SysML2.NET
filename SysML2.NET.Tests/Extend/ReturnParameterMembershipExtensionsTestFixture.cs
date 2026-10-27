// ------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;

    [TestFixture]
    public class ReturnParameterMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedParameterDirectionOperation()
        {
            Assert.That(() => ((IReturnParameterMembership)null).ComputeRedefinedParameterDirectionOperation(), Throws.TypeOf<ArgumentNullException>());

            var returnParameterMembership = new ReturnParameterMembership();

            Assert.That(returnParameterMembership.ComputeRedefinedParameterDirectionOperation(), Is.EqualTo(FeatureDirectionKind.Out));
        }
    }
}
