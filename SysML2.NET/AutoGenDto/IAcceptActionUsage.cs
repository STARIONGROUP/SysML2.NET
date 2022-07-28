// -------------------------------------------------------------------------------------------------
// <copyright file="IAcceptActionUsage.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An AcceptActionUsage is an ActionUsage that is typed, directly or indirectly, by the
    /// ActionDefinition AcceptAction from the Systems model library. It specifies the acceptance of an
    /// incomingTransfer from the Occurrence given by the result of its receiverArgument Expression. The
    /// payload of the accepted Transfer is output on its payloadParameter.Which Transfers may be accepted
    /// is determined by the typing and binding of the payloadParameter. If the triggerKind has any value
    /// other than accept, then the payloadParameter must be bound to a payloadArgument that is an
    /// InvocationExpression whose function is determined by the triggerKind.
    /// </summary>
    public interface IAcceptActionUsage : IActionUsage
    {
    }
}
