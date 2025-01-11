// -------------------------------------------------------------------------------------------------
// <copyright file="ApiDeSerializationProvider.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.PIM.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;
    using SysML2.NET.Serializer.Json;

    /// <summary>
    /// Delegate provider for the appropriate deserialization method for <see cref="System.Type"/> from the 
    /// <see cref="SysML2.NET.API"/> namespace
    /// </summary>
    internal static class ApiDeSerializationProvider
    {
        /// <summary>
        /// a dictionary that provides delegates for deserialization
        /// </summary>
        private static readonly Dictionary<string, Func<JsonElement, SerializationModeKind, ILoggerFactory, IData>>
            DeSerializerActionMap =
                new Dictionary<string, Func<JsonElement, SerializationModeKind, ILoggerFactory, IData>>
                {
                    { "Branch", BranchDeserializer.DeSerialize },
                    { "Commit", CommitDeSerializer.DeSerialize },
                    { "DataIdentity", DataIdentityDeSerializer.DeSerialize },
                    { "Project", ProjectDeSerializer.DeSerialize },
                    { "Tag", TagDeserializer.DeSerialize}
                };
    
        /// <summary>
        /// Provides the delegate <see cref="Func{JsonElement, SerializationModeKind, ILoggerFactory, IData}"/> for the
        /// <see cref="System.Type"/> that is to be deserialized
        /// </summary>
        /// <param name="typeName">
        /// The name of the subject <see cref="System.Type"/> that is to be serialized
        /// </param>
        /// <returns>
        /// A Delegate of <see cref="Func{JsonElement, SerializationModeKind, ILoggerFactory, IData}"/>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// Thrown when the <see cref="System.Type"/> is not supported.
        /// </exception>
        internal static Func<JsonElement, SerializationModeKind, ILoggerFactory, IData> Provide(string typeName)
        {
            if (!DeSerializerActionMap.TryGetValue(typeName, out var func))
            {
                throw new NotSupportedException($"The {typeName} is not supported by the ApiDeSerializationProvider.");
            }

            return func;
        }

        /// <summary>
        /// Asserts whether the <paramref name="typeName"/> is supported by the provider
        /// </summary>
        /// <param name="typeName">
        /// The name of the subject <see cref="System.Type"/> for which support is asserted
        /// </param>
        /// <returns></returns>
        internal static bool IsTypeSupported(string typeName)
        {
            return DeSerializerActionMap.ContainsKey(typeName);
        }
    }
}
