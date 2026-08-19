// -------------------------------------------------------------------------------------------------
// <copyright file="ModelVersionRegistration.cs" company="Starion Group S.A.">
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
    /// One registered metamodel release in the append-only model-version registry
    /// (<see cref="ClassKindRegistry" />). The id is an ordinal: a higher id is a later release,
    /// and ids are never renumbered once assigned — the SQL schema's model_version table and
    /// commit.model_version_id stamps persist them.
    /// </summary>
    /// <param name="Id">
    /// The frozen ordinal of the release (sysml2.model_version.id)
    /// </param>
    /// <param name="Name">
    /// The human-readable release label, e.g. "sysml-2.0-beta-4"
    /// </param>
    /// <param name="SourceFingerprint">
    /// The root-package fingerprint ("Name:XmiId") of the UML model the release was generated
    /// from — the generator refuses to run when the model on disk no longer matches the newest
    /// registered fingerprint, so a metamodel change forces a conscious registry update
    /// </param>
    public sealed record ModelVersionRegistration(int Id, string Name, string SourceFingerprint);
}