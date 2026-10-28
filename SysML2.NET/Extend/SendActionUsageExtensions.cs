// ------------------------------------------------------------------------------------------------
// <copyright file="SendActionUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Actions
{
    using System;

    using SysML2.NET.Decorators;

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
        [DerivedProperty(name: nameof(ISendActionUsage.payloadArgument))]
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
        [DerivedProperty(name: nameof(ISendActionUsage.receiverArgument))]
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
        [DerivedProperty(name: nameof(ISendActionUsage.senderArgument))]
        internal static IExpression ComputeSenderArgument(this ISendActionUsage sendActionUsageSubject)
        {
            return sendActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(sendActionUsageSubject))
                : sendActionUsageSubject.Argument(2);
        }
    }
}
