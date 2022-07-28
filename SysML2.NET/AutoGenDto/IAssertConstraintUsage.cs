// -------------------------------------------------------------------------------------------------
// <copyright file="IAssertConstraintUsage.cs" company="RHEA System S.A.">
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
    /// An AssertConstraintUsage is a ConstraintUsage that is also an Invariant and, so, is asserted to be
    /// true (by default). The asserted ConstraintUsage (which may be the AssertConstraintUsage itself) is
    /// related to the AssertConstraintUsage by a Subsetting relationship.If the AssertConstraintUsage is
    /// owned by a Part, then it also subsets the assertedConstraints property of that Part (as defined in
    /// the library model for Part), otherwise it subsets constraintChecks, as required for a regular
    /// ConstraintUsage.
    /// </summary>
    public partial interface IAssertConstraintUsage : IConstraintUsage, IInvariant
    {
    }
}
