// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializationProvider.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.DTO;

    internal static class DeSerializationProvider
    {
        private static readonly Dictionary<string, Func<JsonElement, SerializationModeKind, ILoggerFactory, IElement>>
            DeSerializerActionMap = new Dictionary<string, Func<JsonElement, SerializationModeKind, ILoggerFactory, IElement>>
            {
                { "PartDefinition", PartDefinitionDeSerializer.DeSerialize },
            };

        internal static Func<JsonElement, SerializationModeKind, ILoggerFactory, IElement> Provide(string typeName)
        {
            if (!DeSerializerActionMap.TryGetValue(typeName, out var func))
            {
                throw new NotSupportedException($"The {typeName} is not supported by the DeSerializationProvider.");
            }

            return func;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
