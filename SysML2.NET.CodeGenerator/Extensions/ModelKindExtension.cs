// -------------------------------------------------------------------------------------------------
// <copyright file="ModelKindExtension.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;

    using SysML2.NET.CodeGenerator.Enumeration;

    /// <summary>
    /// Extension class for the <see cref="ModelKind"/> enum
    /// </summary>
    public static class ModelKindExtension
    {
        /// <summary>
        /// Queries the root name of the UML model based on the provided <see cref="ModelKind"/>
        /// </summary>
        /// <param name="modelKind">The current <see cref="ModelKind"/></param>
        /// <returns>The UML model root name</returns>
        /// <exception cref="IndexOutOfRangeException">If the <paramref name="modelKind"/> is not supported</exception>
        public static string QueryRootNamePerModelKind(this ModelKind modelKind)
        {
            return modelKind switch
            {
                ModelKind.Core => "SysML",
                ModelKind.PIM => "Systems Modeling API and Services PIM",
                _ => throw new IndexOutOfRangeException($"Provided  modelKind: {modelKind} not supported")
            };
        }
    }
}
