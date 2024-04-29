// -------------------------------------------------------------------------------------------------
// <copyright file="SerializationProvider.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.PIM.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Delegate provider for the appropriate serialization method to serialize a <see cref="Type" />
    /// </summary>
    internal static class ApiSerializationProvider
    {
        private static readonly Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind>> SerializerActionMap = new Dictionary<System.Type, Action<object, Utf8JsonWriter, SerializationModeKind>>
        {
            { typeof(SysML2.NET.PIM.DTO.Branch), BranchSerializer.Serialize },
            { typeof(SysML2.NET.PIM.DTO.Commit), CommitSerializer.Serialize },
            { typeof(SysML2.NET.PIM.DTO.DataIdentity), DataIdentitySerializer.Serialize },
            { typeof(SysML2.NET.PIM.DTO.DataVersion), DataVersionSerializer.Serialize },
            { typeof(SysML2.NET.PIM.DTO.Project), ProjectSerializer.Serialize },
            { typeof(SysML2.NET.PIM.DTO.Tag), TagSerializer.Serialize },
        };

        /// <summary>
        /// Provides the delegate <see cref="Action{object, Utf8JsonWriter, SerializationModeKind}"/> for the
        /// <see cref="System.Type"/> that is to be serialized
        /// </summary>
        /// <param name="type">
        /// The subject <see cref="System.Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Action{object, Utf8JsonWriter, SerializationModeKind}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="System.Type"/> is not supported.
        /// </exception>
        internal static Action<object, Utf8JsonWriter, SerializationModeKind> Provide(System.Type type)
        {
            if (!SerializerActionMap.TryGetValue(type, out var action))
            {
                throw new NotSupportedException($"The {type.Name} is not supported by the SerializationProvider.");
            }

            return action;
        }

        /// <summary>
        /// Asserts whether the <paramref name="type"/> is supported by the provider
        /// </summary>
        /// <param name="typeName">
        /// The name of the subject <see cref="System.Type"/> for which support is asserted
        /// </param>
        /// <returns></returns>
        internal static bool IsTypeSupported(System.Type type)
        {
            return SerializerActionMap.ContainsKey(type);
        }
    }
}
