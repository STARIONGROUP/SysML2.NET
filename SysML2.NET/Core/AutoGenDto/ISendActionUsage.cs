// -------------------------------------------------------------------------------------------------
// <copyright file="ISendActionUsage.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A SendActionUsage is an ActionUsage that specifies the sending of a payload given by the result of
    /// its payloadArgument Expression via a MessageTransfer whose source is given by the result of the
    /// senderArgument Expression and whose target is given by the result of the receiverArgument
    /// Expression. If no senderArgument is provided, the default is the this context for the action. If no
    /// receiverArgument is given, then the receiver is to be determined by, e.g., outgoing Connections from
    /// the sender. payloadArgument = argument(1)senderArgument = argument(2)receiverArgument =
    /// argument(3)inputParameters->size() >= 3specializesFromLibrary("Actions::sendActions")isComposite and
    /// owningType <> null and(owningType.oclIsKindOf(ActionDefinition) or
    /// owningType.oclIsKindOf(ActionUsage)) implies   
    /// specializesFromLibrary('Actions::Action::acceptSubactions')
    /// </summary>
    public partial interface ISendActionUsage : IActionUsage
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
