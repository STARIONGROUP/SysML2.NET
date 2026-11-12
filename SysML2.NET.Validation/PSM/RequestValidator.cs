// ------------------------------------------------------------------------------------------------
// <copyright file="RequestValidator.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.PSM
{
    using FluentValidation;

    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// Base class of the validators that check a request payload of the Systems Modeling API and Services before it is processed
    /// </summary>
    /// <typeparam name="TRequest">
    /// The <see cref="IRequest"/> type that is validated
    /// </typeparam>
    public abstract class RequestValidator<TRequest> : AbstractValidator<TRequest> where TRequest : IRequest
    {
        /// <summary>
        /// Registers the hand-coded rules that complement the rules derived from the OpenAPI document
        /// </summary>
        /// <remarks>
        /// The generated validator constructor invokes this method once, after registering its generated rules.
        /// </remarks>
        protected virtual void AddCustomValidation()
        {
        }
    }
}
