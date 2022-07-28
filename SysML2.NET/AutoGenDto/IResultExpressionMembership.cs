// -------------------------------------------------------------------------------------------------
// <copyright file="IResultExpressionMembership.cs" company="RHEA System S.A.">
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
    /// A ResultExpressionMembership is a FeatureMembership that indicates that the ownedResultExpression
    /// provides the result values for the Function or Expression that owns it. The owning Function or
    /// Expression must contain a BindingConnector between the result parameter of the ownedResultExpression
    /// and the result parameter of the Function or Expression.
    /// </summary>
    public partial interface IResultExpressionMembership : IFeatureMembership
    {
    }
}
