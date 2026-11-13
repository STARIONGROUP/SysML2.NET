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
        /// Initializes a new instance of the <see cref="RequestValidator{TRequest}"/> class and registers its hand-coded rules
        /// </summary>
        protected RequestValidator()
        {
            this.RegisterCustomValidation();
        }

        /// <summary>
        /// Registers the hand-coded rules that complement the rules derived from the OpenAPI document
        /// </summary>
        /// <remarks>
        /// The base constructor invokes this method once, before the derived constructor registers the generated rules.
        /// </remarks>
        protected virtual void AddCustomValidation()
        {
        }

        /// <summary>
        /// Invokes <see cref="AddCustomValidation"/> from the constructor through a non-virtual call site
        /// </summary>
        private void RegisterCustomValidation()
        {
            this.AddCustomValidation();
        }
    }
}
