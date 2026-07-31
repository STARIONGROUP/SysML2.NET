// -------------------------------------------------------------------------------------------------
// <copyright file="SendActionUsageExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Systems.Actions
{
    using System;

    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// The <see cref="SendActionUsageExtensions" /> class provides extensions methods for
    /// the <see cref="ISendActionUsage" /> interface
    /// </summary>
    internal static class SendActionUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// payloadArgument = argument(1)
        /// </code>
        /// </remarks>
        /// <param name="sendActionUsageSubject">
        /// The subject <see cref="ISendActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IExpression ComputePayloadArgument(this ISendActionUsage sendActionUsageSubject)
        {
            return sendActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(sendActionUsageSubject))
                : sendActionUsageSubject.Argument(1);
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// receiverArgument = argument(3)
        /// </code>
        /// </remarks>
        /// <param name="sendActionUsageSubject">
        /// The subject <see cref="ISendActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IExpression ComputeReceiverArgument(this ISendActionUsage sendActionUsageSubject)
        {
            return sendActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(sendActionUsageSubject))
                : sendActionUsageSubject.Argument(3);
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// senderArgument = argument(2)
        /// </code>
        /// </remarks>
        /// <param name="sendActionUsageSubject">
        /// The subject <see cref="ISendActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IExpression ComputeSenderArgument(this ISendActionUsage sendActionUsageSubject)
        {
            return sendActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(sendActionUsageSubject))
                : sendActionUsageSubject.Argument(2);
        }
    }
}
