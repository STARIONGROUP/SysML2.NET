// -------------------------------------------------------------------------------------------------
// <copyright file="VisibilityKind.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core
{
    /// <summary>
    /// VisibilityKind is an enumeration whose literals specify the visibility of a Membership of an Element
    /// in a Namespace outside of that Namespace. Note that &quot;visibility&quot; specifically restricts
    /// whether an Element in a Namespace may be referenced by name from outside the Namespace and only
    /// otherwise restricts access to an Element as provided by specific constraints in the abstract syntax
    /// (e.g., preventing the import or inheritance of private Elements).
    /// </summary>
    public enum VisibilityKind
    {
        /// <summary>
        /// Indicates a Membership is not visible outside its owning Namespace.
        /// </summary>
        Private = 0,

        /// <summary>
        /// An intermediate level of visibility between public and private. By default, it is equivalent to
        /// private for the purposes of normal access to and import of Elements from a Namespace. However, other
        /// Relationships may be specified to include Memberships with protected visibility in the list of
        /// memberships for a Namespace (e.g., Specialization).
        /// </summary>
        Protected = 1,

        /// <summary>
        /// Indicates that a Membership is publicly visible outside its owning Namespace.
        /// </summary>
        Public = 2,

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
