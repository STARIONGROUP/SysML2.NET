// ------------------------------------------------------------------------------------------------
// <copyright file="DtoValidator.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Validation.Core.DTO
{
    using FluentValidation;

    using SysML2.NET.Core.DTO.Root.Elements;

    /// <summary>
    /// Base class of the validators that check a core data transfer object against the multiplicities and types of its metaclass
    /// </summary>
    /// <typeparam name="TElement">
    /// The <see cref="IElement"/> type that is validated
    /// </typeparam>
    public abstract class DtoValidator<TElement> : AbstractValidator<TElement> where TElement : IElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtoValidator{TElement}"/> class, registers the identifier rules and the hand-coded rules
        /// </summary>
        protected DtoValidator()
        {
            this.RuleFor(dto => dto.Id).NotEmpty();
            this.RuleFor(dto => dto.ElementId).NotEmpty().When(dto => dto.ElementId != null);
            this.RegisterCustomValidation();
        }

        /// <summary>
        /// Registers the hand-coded rules that complement the rules derived from the UML model
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
