// -------------------------------------------------------------------------------------------------
// <copyright file="ModelLevelEvaluableFunctions.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.KernelFunctions
{
    using System;
    using System.Collections.Frozen;
    using System.Collections.Generic;

    /// <summary>
    /// Provides the Kernel Function Library membership set that decides
    /// <c>Function::isModelLevelEvaluable</c>, derived from the operator terminals of the KerML
    /// textual-notation KEBNF grammar, the defaulted <c>operator</c> attributes of the UML model, and
    /// the Kernel Function Library itself.
    /// </summary>
    public static class ModelLevelEvaluableFunctions
    {
        /// <summary>
        /// Provides the model-level evaluable library functions as raw <c>Package::Function</c> names.
        /// </summary>
        /// <remarks>
        /// The segments are declared names, NOT KerML-escaped names — <c>BaseFunctions::==</c>, not
        /// <c>BaseFunctions::'=='</c> — so that membership can be tested without reproducing the
        /// escaping rules of the textual notation.
        /// </remarks>
        public static readonly FrozenSet<string> QualifiedNames = new List<string>
        {
            "BaseFunctions::!=",
            "BaseFunctions::!==",
            "BaseFunctions::#",
            "DataFunctions::%",
            "DataFunctions::&",
            "DataFunctions::*",
            "DataFunctions::**",
            "DataFunctions::+",
            "BaseFunctions::,",
            "DataFunctions::-",
            "ControlFunctions::.",
            "DataFunctions::..",
            "DataFunctions::/",
            "DataFunctions::<",
            "DataFunctions::<=",
            "BaseFunctions::==",
            "BaseFunctions::===",
            "DataFunctions::>",
            "DataFunctions::>=",
            "ControlFunctions::??",
            "BaseFunctions::@",
            "BaseFunctions::@@",
            "DataFunctions::^",
            "ControlFunctions::and",
            "BaseFunctions::as",
            "ControlFunctions::collect",
            "BaseFunctions::hastype",
            "ControlFunctions::if",
            "ControlFunctions::implies",
            "BaseFunctions::istype",
            "BaseFunctions::meta",
            "DataFunctions::not",
            "ControlFunctions::or",
            "ControlFunctions::select",
            "DataFunctions::xor",
            "DataFunctions::|"
        }.ToFrozenSet(StringComparer.Ordinal);

        /// <summary>
        /// Asserts that the named function of the named library package is model-level evaluable.
        /// </summary>
        /// <param name="packageName">The declared name of the library package owning the function</param>
        /// <param name="functionName">The declared name of the function</param>
        /// <returns><c>true</c> when the function is model-level evaluable, <c>false</c> otherwise</returns>
        public static bool Contains(string packageName, string functionName)
        {
            return !string.IsNullOrWhiteSpace(packageName)
                   && !string.IsNullOrWhiteSpace(functionName)
                   && QualifiedNames.Contains($"{packageName}::{functionName}");
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
