// -------------------------------------------------------------------------------------------------
// <copyright file="ClassKindEnumMember.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    /// <summary>
    /// One member of the generated ClassKind enum — a <see cref="ClassKindRegistration" />
    /// projected into the render-ready form the core-classkind-enum-template consumes: release
    /// ordinals resolved to release names, nothing left to compute inside the template.
    /// </summary>
    /// <param name="Name">
    /// The metaclass name (the API @type value), e.g. "PartUsage"
    /// </param>
    /// <param name="Id">
    /// The frozen interned id (sysml2.class_kind.id) that becomes the explicit enum value
    /// </param>
    /// <param name="IsAbstract">
    /// Whether the metaclass is abstract in the metamodel
    /// </param>
    /// <param name="IntroducedIn">
    /// The name of the release that introduced the metaclass, e.g. "sysml-2.0-beta-4"
    /// </param>
    /// <param name="RemovedIn">
    /// The name of the first release WITHOUT the metaclass, or null while it is still part of the
    /// newest registered release
    /// </param>
    public sealed record ClassKindEnumMember(string Name, int Id, bool IsAbstract, string IntroducedIn, string RemovedIn);
}
