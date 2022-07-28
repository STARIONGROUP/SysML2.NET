// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureChainExpression.cs" company="RHEA System S.A.">
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
    /// A FeatureChainExpression is an OperatorExpression whose operator is ".", which resolves to the
    /// library Function ControlFunctions::'.'. It evaluates to the result of chaining the result Feature of
    /// its single argument Expression with its targetFeature.The first two members of a
    /// FeatureChainExpression must be its single argument Expression and its targetFeature. Its only other
    /// members shall be those necessary to complete it as an InvocationExpression.
    /// </summary>
    public interface IFeatureChainExpression : IOperatorExpression
    {
    }
}
