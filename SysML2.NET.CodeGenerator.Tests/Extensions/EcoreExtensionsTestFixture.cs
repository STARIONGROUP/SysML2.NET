// -------------------------------------------------------------------------------------------------
// <copyright file="EcoreExtensionsTestFixture.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2024 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ECoreNetto;
    using NUnit.Framework;
    using SysML2.NET.CodeGenerator.Extensions;

    [TestFixture]
    public class EcoreExtensionsTestFixture
    {
        private EPackage ePackage;

        [SetUp]
        public void SetUp()
        {
            this.ePackage = DataModelLoader.Load();
        }

        [Test]
        public void Verify_that_QueryDocumentation_gracefully_returns()
        {
            var classifiers = this.ePackage.EClassifiers;

            Assert.That(() =>
            {
                foreach (var eEnum in classifiers)
                {
                    var docs = eEnum.QueryDocumentation();
                }
            }, Throws.Nothing);
        }
    }
}
