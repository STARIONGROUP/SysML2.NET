// -------------------------------------------------------------------------------------------------
// <copyright file="StateSubactionKind.cs" company="RHEA System S.A.">
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

namespace SysML2.NET
{
    /// <summary>
    /// A StateSubactionKind indicates whether the <tt>action</tt> of a StateSubactionMembership is an 
    /// entry, do or exit action.A StateActionKind indicates whether a Action feature of a State is an 
    /// entry, do or exit Action. 
    /// </summary>
    public enum StateSubactionKind
    {
        /// <summary>
        /// Indicates that a subaction of a StateUsage is an entry action. 
        /// </summary>
        Entry = 0,

        /// <summary>
        /// Indicates that a subaction of a StateUsage is a do action. 
        /// </summary>
        Do = 1,

        /// <summary>
        /// Indicates that a subaction of a StateUsage is an exit action. 
        /// </summary>
        Exit = 2,

    }
}
