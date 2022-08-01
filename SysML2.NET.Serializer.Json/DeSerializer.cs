// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializer.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.DTO;
    

    public class DeSerializer
    {
        /// <summary>
        /// The (injected) <see cref="ILoggerFactory"/> used to setup logging
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// The <see cref="ILogger"/> used to log
        /// </summary>
        private readonly ILogger<DeSerializer> logger;

        public DeSerializer(ILoggerFactory loggerFactory = null)
        {
            this.loggerFactory = loggerFactory;

            this.logger = this.loggerFactory == null ? NullLogger<DeSerializer>.Instance : this.loggerFactory.CreateLogger<DeSerializer>();
        }

        public IEnumerable<IElement> DeSerialize(Stream stream, SerializationModeKind serializationModeKind)
        {
            var result = new List<IElement>();

            using (JsonDocument document = JsonDocument.Parse(stream))
            {
                JsonElement root = document.RootElement;

                foreach (JsonElement jsonElement in root.EnumerateArray())
                {
                    if (jsonElement.TryGetProperty("@type", out JsonElement typeElement))
                    {
                        var typeName = typeElement.GetString();

                        if (typeName == "PartDefinition")
                        {
                            var func = DeSerializationProvider.Provide(typeName);
                            var partDefinition = func(jsonElement, serializationModeKind);
                            result.Add(partDefinition);
                        }
                    }
                }
            }

            return result;
        }
    }
}
