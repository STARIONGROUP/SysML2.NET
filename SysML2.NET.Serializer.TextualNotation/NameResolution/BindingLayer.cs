// -------------------------------------------------------------------------------------------------
// <copyright file="BindingLayer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    /// <summary>
    /// The source a Membership enters a Namespace through. KerML §8.2.3.5.3: the local resolution of a
    /// Namespace covers its owned, imported and (for a Type) inherited Memberships.
    /// </summary>
    internal enum BindingLayer
    {
        /// <summary>Bound by an owned Membership of the scope.</summary>
        Owned,

        /// <summary>Bound by an inherited Membership (including through implied Specializations).</summary>
        Inherited,

        /// <summary>Bound through an owned Import of the scope.</summary>
        Imported,
    }
}
