// -------------------------------------------------------------------------------------------------
// <copyright file="HandlebarsPayload.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using uml4net.Packages;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// represents the payload for the generators that require all <see cref="IPackage"/>,
    /// <see cref="IDataType"/> and <see cref="IClass"/>
    /// </summary>
    public class HandlebarsPayload
    {
        /// <summary>
        /// initializes an instance of the <see cref="HandlebarsPayload"/> class.
        /// </summary>
        /// <param name="rootPackage">
        /// The Root <see cref="IPackage"/> which is that <see cref="IPackage"/> that was
        /// initially loaded
        /// </param>
        /// <param name="packages">
        /// The top level <see cref="IPackage"/>s of the UML models
        /// </param>
        /// <param name="enumerations">
        /// the <see cref="IEnumeration"/>s in the UML models
        /// </param>
        /// <param name="primitiveTypes">
        /// the <see cref="IPrimitiveType"/>s in the UML models
        /// </param>
        /// <param name="dataTypes">
        /// the <see cref="IDataType"/>s in the UML models
        /// </param>
        /// <param name="classes">
        /// the <see cref="IClass"/>es in the UML models
        /// </param>
        /// <param name="interfaces">
        /// the <see cref="IInterface"/>es in the UML models
        /// </param>
        public HandlebarsPayload(IPackage rootPackage, IEnumerable<IPackage> packages, IEnumerable<IEnumeration> enumerations,
            IEnumerable<IPrimitiveType> primitiveTypes, IEnumerable<IDataType> dataTypes, IEnumerable<IClass> classes,
            IEnumerable<IInterface> interfaces)
        {
            this.RootPackage = rootPackage;
            this.Packages = packages.ToArray();
            this.Enumerations = enumerations.ToArray();
            this.PrimitiveTypes = primitiveTypes.ToArray();
            this.DataTypes = dataTypes.ToArray();
            this.Classes = classes.ToArray();
            this.Interfaces = interfaces.ToArray();
        }

        /// <summary>
        /// Gets the Root <see cref="IPackage"/> which is that <see cref="IPackage"/> that was
        /// initially loaded
        /// </summary>
        public IPackage RootPackage { get; private set; }

        /// <summary>
        /// Gets the top level <see cref="IPackage"/>s, the <see cref="RootPackage"/> shall be contained in the
        /// <see cref="Packages"/> array
        /// </summary>
        public IPackage[] Packages { get; private set; }

        /// <summary>
        /// Gets the array of <see cref="IEnumeration"/>
        /// </summary>
        public IEnumeration[] Enumerations { get; private set; }

        /// <summary>
        /// Gets the array of <see cref="IPrimitiveType"/>
        /// </summary>
        public IPrimitiveType[] PrimitiveTypes { get; private set; }

        /// <summary>
        /// Gets the array of <see cref="IDataType"/>
        /// </summary>
        public IDataType[] DataTypes { get; private set; }

        /// <summary>
        /// Gets the array of <see cref="IClass"/>
        /// </summary>
        public IClass[] Classes { get; private set; }

        /// <summary>
        /// Gets the array of <see cref="IInterface"/>
        /// </summary>
        public IInterface[] Interfaces { get; private set; }

        /// <summary>
        /// Gets the version of the reporting library
        /// </summary>
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    }
}
