// -------------------------------------------------------------------------------------------------
// <copyright file="DataModelLoaderTestFixture.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
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

using System;

namespace SysML2.NET.CodeGenerator.Tests
{
    using System.Linq;

    using ECoreNetto;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator;

    /// <summary>
    /// Suite of tests for the <see cref="DataModelLoader"/> class.
    /// </summary>
    [TestFixture]
    public class DataModelLoaderTestFixture
    {
        [Test]
        public void Verify_that_the_expected_SysML2_model_is_returned()
        {
            var ePacakge = DataModelLoader.Load();
            
            Assert.That(ePacakge, Is.Not.Null);
            
            var classes = ePacakge.EClassifiers.OfType<EClass>();
            
            var rootClass = classes.Single(x => !x.ESuperTypes.Any());

            Assert.That(rootClass.Name, Is.EqualTo("Element"));

            foreach (var eClass in classes)
            {
                Console.WriteLine(eClass.Name);
            }
        }
    }
}