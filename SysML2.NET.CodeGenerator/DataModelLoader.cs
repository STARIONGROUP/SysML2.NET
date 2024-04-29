// -------------------------------------------------------------------------------------------------
// <copyright file="DataModelLoader.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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

namespace SysML2.NET.CodeGenerator
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;

    using ECoreNetto;
    using ECoreNetto.Resource;

    using OpenApi.Model;

    /// <summary>
    /// The purpose of the <see cref="DataModelLoader"/> is to load the SysML data-model
    /// from the ecore file
    /// </summary>
    public static class DataModelLoader
    {
        /// <summary>
        /// Load the data model and return the root <see cref="EPackage"/>
        /// </summary>
        /// <returns>
        /// the root <see cref="EPackage"/>
        /// </returns>
        public static EPackage Load()
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var ecoreFileName = Path.Combine(assemblyFolder, "datamodel", "SysML.ecore");

            var uri = new Uri(ecoreFileName);

            var resourceSet = new ResourceSet();
            var resource = resourceSet.CreateResource(uri);
            
            var root = resource.Load(null);
         
            SortElementsByName(root);

            return root;
        }

        /// <summary>
        /// Sort the contained classes, structural features and operations in alphabetical order.
        /// </summary>
        /// <param name="package">
        /// the <see cref="EPackage"/> who's content needs to be sorted
        /// </param>
        public static void SortElementsByName(EPackage package)
        {
            var classifiers = package.EClassifiers.OrderBy(x => x.Name).ToList();
            package.EClassifiers.Clear();
            package.EClassifiers.AddRange(classifiers);

            foreach (var eClassifier in classifiers.OfType<EClass>())
            {
                var eStructuralFeatures = eClassifier.EStructuralFeatures.OrderBy(x => x.Name).ToList();
                eClassifier.EStructuralFeatures.Clear();
                eClassifier.EStructuralFeatures.AddRange(eStructuralFeatures);

                var eOperations = eClassifier.EOperations.OrderBy(x => x.Name).ToList();
                eClassifier.EOperations.Clear();
                eClassifier.EOperations.AddRange(eOperations);
            }
        }

        /// <summary>
        /// Loads the SysML v2 Open API.json specification
        /// </summary>
        /// <returns>
        /// an instance of <see cref="OpenApiDocument"/>
        /// </returns>
        public static Document LoadOpenApi()
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var ecoreFileName = Path.Combine(assemblyFolder, "datamodel", "openapi.json");

            using var stream = new FileStream(ecoreFileName,  FileMode.Open, FileAccess.Read);

            var deserializer = new OpenApi.DeSerializer();
            var document = deserializer.DeSerialize(stream);

            return document;
        }
    }
}
