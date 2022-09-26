// -------------------------------------------------------------------------------------------------
// <copyright file="ISuccessionItemFlow.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// A SuccessionItemFlow is an ItemFlow that also provides temporal ordering. It classifies Transfers
    /// that cannot start until the source Occurrence has completed and that must complete before the target
    /// Occurrence can start.A SuccessionItemFlow must be typed by the Interaction TransferBefore from the
    /// Kernel Library, or a specialization of it.SuccessionItemFlows are ItemFlows that also provide
    /// temporal ordering. They classify Transfers that must complete before the target behavior can
    /// start.Must be typed by M1 TransferBefore or one of its
    /// specializations.<br>association-&gt;is=OrSpecializationOf(TransferBefore) }
    /// </summary>
    public partial interface ISuccessionItemFlow : IItemFlow, ISuccession
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
