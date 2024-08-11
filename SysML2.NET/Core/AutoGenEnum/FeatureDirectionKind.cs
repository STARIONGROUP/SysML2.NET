// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureDirectionKind.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2024 Starion Group S.A.
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

namespace SysML2.NET.Core
{
    /// <summary>
    /// FeatureDirectionKind enumerates the possible kinds of direction that a Feature may be given as a
    /// member of a Type.
    /// </summary>
    public enum FeatureDirectionKind
    {
        /// <summary>
        /// Values of the Feature on each instance of its domain are determined externally to that instance and
        /// used internally.
        /// </summary>
        In = 0,

        /// <summary>
        /// Values of the Feature on each instance are determined either as in or out directions, or both.
        /// </summary>
        Inout = 1,

        /// <summary>
        /// Values of the Feature on each instance of its domain are determined internally to that instance and
        /// used externally.
        /// </summary>
        Out = 2,

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
