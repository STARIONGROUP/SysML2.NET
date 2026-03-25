// -------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using uml4net.Classification;
    using uml4net.CommonStructure;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Extension methods for the <see cref="IType"/> interface
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Queries the C# Type name of the <see cref="IType"/>
        /// </summary>
        /// <param name="type">
        /// The <see cref="IType"/> for which the C# Type name is to be queried
        /// </param>
        /// <param name="shouldTargetInterface">Asserts that the type name should target the interface name in case of an <see cref="IClassifier"/></param>
        /// <returns>
        /// a string representation of the C# Type name
        /// </returns>
        public static string QueryCSharpTypeName(this IType type, bool shouldTargetInterface)
        {
            var typeName = uml4net.Extensions.TypeExtensions.QueryCSharpTypeName(type);

            if (shouldTargetInterface && type is IClass)
            {
                typeName = $"I{typeName}";
            }

            return typeName;
        }
    }
}
