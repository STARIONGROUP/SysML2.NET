// -------------------------------------------------------------------------------------------------
// <copyright file="ClassKindRegistration.cs" company="Starion Group S.A.">
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
    /// One frozen row of the append-only class-kind registry (<see cref="ClassKindRegistry" />).
    /// The id is assigned once — when the metaclass first appears in a registered metamodel
    /// release — and never renumbered: sysml2.element_version.class_kind values and the generated
    /// ClassKind C# enum persist it forever. A metaclass dropped by a later release keeps its
    /// registration, closed with <paramref name="RemovedIn" />.
    /// </summary>
    /// <param name="Id">
    /// The frozen interned id (sysml2.class_kind.id)
    /// </param>
    /// <param name="Name">
    /// The API @type value, e.g. "PartUsage"
    /// </param>
    /// <param name="IsAbstract">
    /// Whether the metaclass is abstract in the metamodel
    /// </param>
    /// <param name="IntroducedIn">
    /// The <see cref="ModelVersionRegistration.Id" /> of the release that introduced the metaclass
    /// </param>
    /// <param name="RemovedIn">
    /// The <see cref="ModelVersionRegistration.Id" /> of the first release WITHOUT the metaclass,
    /// or null while it is still part of the newest registered release
    /// </param>
    public sealed record ClassKindRegistration(int Id, string Name, bool IsAbstract, int IntroducedIn, int? RemovedIn = null);
}