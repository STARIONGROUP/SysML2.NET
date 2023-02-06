// -------------------------------------------------------------------------------------------------
// <copyright file="DtoGenerator.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using System.IO;

    using ECoreNetto;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    /// <summary>
    /// A Handlebars based DTO code generator
    /// </summary>
    public class DtoGenerator : HandleBarsGenerator
    {
        /// <summary>
        /// Generates the <see cref="EClass"/> instances
        /// that are in the provided <see cref="EPackage"/>
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> that contains the <see cref="EClass"/> to generate
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public override async Task Generate(EPackage package, DirectoryInfo outputDirectory)
        {
            await this.GenerateInterfaces(package, outputDirectory);
            await this.GenerateClasses(package, outputDirectory);
        }

		/// <summary>
		/// Generates all the interfaces for <see cref="EClass"/> instances n the <see cref="EPackage"/>
		/// </summary>
		/// <param name="package">
		/// The <see cref="EPackage"/> that contains the <see cref="EClass"/> instances
		/// </param>
		/// <param name="outputDirectory">
		/// The directory where the output is to be generated
		/// </param>
		/// <returns>
		/// an awaitable <see cref="Task"/>
		/// </returns>
		public async Task GenerateInterfaces(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["dto-interface-template"];

            foreach (var eClass in package.EClassifiers.OfType<EClass>())
            {
                var generatedInterface = template(eClass);

                generatedInterface = CodeCleanup(generatedInterface);

                var fileName = $"I{eClass.Name.CapitalizeFirstLetter()}.cs";

                await Write(generatedInterface, outputDirectory, fileName);
            }
        }

		/// <summary>
		/// Generates the interface C# class file using the name of the class
		/// </summary>
		/// <param name="package">
		/// The <see cref="EPackage"/> that contains the <see cref="EClass"/> instances
		/// </param>
		/// <param name="outputDirectory">
		/// The directory where the output is to be generated
		/// </param>
		/// <param name="className">
		/// The name of the class for which the interface is to be generated
		/// </param>
		/// <returns>
		/// an awaitable <see cref="Task"/>
		/// </returns>
		public async Task<string> GenerateInterface(EPackage package, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates["dto-interface-template"];

            var eClass = package.EClassifiers.OfType<EClass>().Single(x => x.Name == className);
            
            var generatedInterface = template(eClass);

            generatedInterface = CodeCleanup(generatedInterface);

            var fileName = $"I{eClass.Name.CapitalizeFirstLetter()}.cs";

            await Write(generatedInterface, outputDirectory, fileName);
            
            return generatedInterface;
        }

		/// <summary>
		/// Generates all the classes for <see cref="EClass"/> instances n the <see cref="EPackage"/>
		/// </summary>
		/// <param name="package">
		/// The <see cref="EPackage"/> that contains the <see cref="EClass"/> instances
		/// </param>
		/// <param name="outputDirectory">
		/// The directory where the output is to be generated
		/// </param>
		/// <returns>
		/// an awaitable <see cref="Task"/>
		/// </returns>
		public async Task GenerateClasses(EPackage package, DirectoryInfo outputDirectory)
        {
            var template = this.Templates["dto-class-template"];
            
            foreach (var eClass in package.EClassifiers.OfType<EClass>().Where(x => !x.Abstract))
            {
                var generatedClass = template(eClass);

                generatedClass = CodeCleanup(generatedClass);

                var fileName = $"{eClass.Name.CapitalizeFirstLetter()}.cs";

                await Write(generatedClass, outputDirectory, fileName);
            }
        }

		/// <summary>
		/// Generates the C# class file using the name of the <see cref="EClass"/>
		/// </summary>
		/// <param name="package">
		/// The <see cref="EPackage"/> that contains the <see cref="EClass"/> instances
		/// </param>
		/// <param name="outputDirectory">
		/// The directory where the output is to be generated
		/// </param>
		/// <param name="className">
		/// The name of the class for which the interface is to be generated
		/// </param>
		/// <returns>
		/// an awaitable <see cref="Task"/>
		/// </returns>
		public async Task<string> GenerateClass(EPackage package, DirectoryInfo outputDirectory, string className)
        {
            var template = this.Templates["dto-class-template"];

            var eClass = package.EClassifiers.OfType<EClass>().Single(x => x.Name == className);

            if (eClass.Abstract)
            {
                throw new InvalidOperationException("DTO should not be abstract");
            }

            var generatedClass = template(eClass);

            generatedClass = CodeCleanup(generatedClass);

            var fileName = $"{eClass.Name.CapitalizeFirstLetter()}.cs";

            await Write(generatedClass, outputDirectory, fileName);

            return generatedClass;
        }

        /// <summary>
        /// Register the custom helpers
        /// </summary>
        protected override void RegisterHelpers()
        {
            ECoreNetto.HandleBars.StringHelper.RegisterStringHelper(this.Handlebars);
            ECoreNetto.HandleBars.StructuralFeatureHelper.RegisterStructuralFeatureHelper(this.Handlebars);
            ECoreNetto.HandleBars.GeneralizationHelper.RegisterGeneralizationHelper(this.Handlebars);

            this.Handlebars.RegisteredDocumentationHelper();
            this.Handlebars.RegisterTypeNameHelper();
            this.Handlebars.RegisterStructuralFeatureHelper();
        }

        /// <summary>
        /// Register the code templates
        /// </summary>
        protected override void RegisterTemplates()
        {
            this.RegisterTemplate("dto-interface-template");
            this.RegisterTemplate("dto-class-template");
        }
    }
}
