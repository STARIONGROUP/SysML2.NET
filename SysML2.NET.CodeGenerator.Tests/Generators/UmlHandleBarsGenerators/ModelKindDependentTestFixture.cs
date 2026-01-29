// -------------------------------------------------------------------------------------------------
// <copyright file="ModelKindDependentTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Enumeration;
    using SysML2.NET.CodeGenerator.Tests.TestFixtureSourceConfiguration;

    using uml4net.xmi.Readers;

    public abstract class ModelKindDependentTestFixture<TModelKindConfiguration> where TModelKindConfiguration: IModelKindConfiguration, new()
    {
        protected readonly ModelKind ModelKind = new TModelKindConfiguration().ModelKind;

        protected XmiReaderResult XmiReaderResult { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.XmiReaderResult = GeneratorSetupFixture.Models[this.ModelKind];
        }
        
        protected static IEnumerable<string> GetConcreteClasses()
        {
            return new TModelKindConfiguration().ConcreteClassesName;
        }
        
        protected static IEnumerable<string> GetAllClasses()
        {
            return new TModelKindConfiguration().AllClassesName;
        }

        protected static IEnumerable<string> GetAllEnums()
        {
            return new TModelKindConfiguration().Enumerations;
        }
    }
}
