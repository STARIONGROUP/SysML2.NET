// -------------------------------------------------------------------------------------------------
// <copyright file="PayloadFactory.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    using SysML2.NET.Core.DTO.Root.Annotations;

    /// <summary>
    /// The purpose of the <see cref="PayloadFactory"/> class is to create an
    /// instance of <see cref="Payload"/>
    /// </summary>
    internal static class PayloadFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="Payload"/> from the provided <see cref="IEnumerable{Thing}"/>
        /// </summary>
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{IData}"/> on the bases of which the <see cref="Payload"/> will
        /// be created
        /// </param>
        /// <returns>
        /// An instance of <see cref="Payload"/>
        /// </returns>
        internal static Payload ToPayload(this IEnumerable<IData> dataItems)
        {
            if (dataItems == null)
            {
                throw new ArgumentNullException(nameof(dataItems));
            }

            var payload = new Payload
            {
                Created = DateTime.UtcNow,
            };

            foreach (var dataItem in dataItems)
            {
                switch (dataItem)
                {
                    case AnnotatingElement annotatingElement:
                        payload.AnnotatingElement.Add(annotatingElement);
                        break;
                }
            }
            return payload;
        }

        /// <summary>
        /// Creates an <see cref="IEnumerable{IData}"/> from the provided <see cref="Payload"/>.
        /// </summary>
        /// <param name="payload">
        /// The <see cref="Payload"/> that carries the <see cref="IData"/>s.
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{IData}"/>.
        /// </returns>
        internal static IEnumerable<IData> ToDataItems(this Payload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            var result = new List<IData>();

            result.AddRange(payload.AnnotatingElement);

            return result;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
