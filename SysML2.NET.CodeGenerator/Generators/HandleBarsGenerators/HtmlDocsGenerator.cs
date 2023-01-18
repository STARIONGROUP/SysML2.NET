// -------------------------------------------------------------------------------------------------
// <copyright file="HtmlDocsGenerator.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.CodeGenerator.Generators.HandleBarsGenerators
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using System.IO;

	using ECoreNetto;

	using SysML2.NET.CodeGenerator.HandleBarHelpers;

	/// <summary>
	/// A Handlebars based ecore to html docs generator
	/// </summary>
	public class HtmlDocsGenerator : HandleBarsGenerator
	{
		/// <summary>
		/// Generates the HTML docs of the datatypes, enums and classes
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
			await this.GenerateHtmlDocs(package, outputDirectory);
		}

		/// <summary>
		/// Generates the DeSerializationProvider class
		/// </summary>
		/// <param name="package">
		/// the <see cref="EPackage"/> that contains the classes that need to be generated
		/// </param>
		/// <param name="outputDirectory">
		/// The target <see cref="DirectoryInfo"/>
		/// </param>
		/// <returns>
		/// an awaitable <see cref="Task"/>
		/// </returns>
		public async Task GenerateHtmlDocs(EPackage package, DirectoryInfo outputDirectory)
		{
			var template = this.Templates["ecore-to-html-docs"];

			var enums = package.EClassifiers.OfType<EEnum>().OrderBy(x => x.Name);
			var dataTypes = package.EClassifiers.OfType<EDataType>().OrderBy(x => x.Name);
			var eClasses = package.EClassifiers.OfType<EClass>().OrderBy(x => x.Name);

			var payload = new Payload(enums, dataTypes, eClasses);

			var generatedHtml = template(payload);

			var fileName = "index.html";

			await Write(generatedHtml, outputDirectory, fileName);
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
			this.RegisterTemplate("ecore-to-html-docs");
		}
	}

	/// <summary>
	/// represents the payload for the HTML generatior
	/// </summary>
	public class Payload
	{
		/// <summary>
		/// initializes an instance of the <see cref="Payload"/> class.
		/// </summary>
		/// <param name="enums">
		/// the <see cref="EEnum"/>s in the data model
		/// </param>
		/// <param name="dataTypes">
		/// the <see cref="EDataType"/>s in the data model
		/// </param>
		/// <param name="classes">
		/// the <see cref="EClass"/>es in the data model
		/// </param>
		public Payload(IEnumerable<EEnum> enums, IEnumerable<EDataType> dataTypes, IEnumerable<EClass> classes)
		{
			this.Enums = enums.ToArray();
			this.DataTypes = dataTypes.ToArray();
			this.Classes = classes.ToArray();

			var f = this.Classes.First().AllEStructuralFeaturesOrderByName.First();
			
		}

		public EEnum[] Enums { get; private set; }

		public EDataType[] DataTypes { get; private set; }

		public EClass[] Classes {get; private set; }
	}
}
