// -------------------------------------------------------------------------------------------------
// <copyright file="ClassKindEnumPayload.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    /// <summary>
    /// The template payload for the core-classkind-enum-template — the full member list of the
    /// generated ClassKind enum, projected from <see cref="ClassKindRegistry.ClassKinds" />.
    /// </summary>
    /// <param name="Members">
    /// The enum members, ordered by frozen id
    /// </param>
    public sealed record ClassKindEnumPayload(IReadOnlyList<ClassKindEnumMember> Members);
}
