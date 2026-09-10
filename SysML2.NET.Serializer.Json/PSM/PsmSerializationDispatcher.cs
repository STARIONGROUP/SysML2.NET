// -------------------------------------------------------------------------------------------------
// <copyright file="PsmSerializationDispatcher.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.PSM
{
    using System;
    using System.Text.Json;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Delegate provider that resolves the serialization method of a value that may be a Systems Modeling API and
    /// Services data transfer object or a SysML Core element
    /// </summary>
    /// <remarks>
    /// The Data union admits Element alongside the Systems Modeling API and Services types, so a payload is resolved
    /// against both providers.
    /// </remarks>
    internal static class PsmSerializationDispatcher
    {
        /// <summary>
        /// Provides the delegate that serializes the specified <see cref="Type"/>
        /// </summary>
        /// <param name="type">
        /// The <see cref="Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// The delegate that serializes the <see cref="Type"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when neither provider supports the <see cref="Type"/>
        /// </exception>
        internal static Action<object, Utf8JsonWriter, SerializationModeKind, bool> Provide(Type type)
        {
            return SerializationProvider.IsTypeSupported(type)
                ? SerializationProvider.Provide(type)
                : SysML2.NET.Serializer.Json.Core.DTO.SerializationProvider.Provide(type);
        }
    }
}
