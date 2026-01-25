// -------------------------------------------------------------------------------------------------
// <copyright file="SerializerBase.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack
{
    using global::MessagePack;
    using global::MessagePack.Resolvers;

    using SysML2.NET.Serializer.MessagePack.Helpers;

    /// <summary>
    /// Base class from which the Serializer and Deserializer derive
    /// </summary>
    public abstract class SerializerBase
    {
        /// <summary>
        /// Creates an instance of the <see cref="MessagePackSerializerOptions"/> 
        /// </summary>
        /// <returns>
        /// An instance of <see cref="MessagePackSerializerOptions"/>
        /// </returns>
        protected MessagePackSerializerOptions CreateSerializerOptions()
        {
            var formatterResolver = CompositeResolver.Create(
                DataFormatterResolver.Instance,
                StandardResolver.Instance);

            var options = MessagePackSerializerOptions.Standard
                .WithResolver(formatterResolver)
                .WithOldSpec(false);

            return options;
        }
    }
}
