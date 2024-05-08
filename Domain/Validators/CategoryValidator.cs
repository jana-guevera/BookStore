using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Validators
{
	public class CategoryValidator : AbstractValidator<Category>
	{
		public CategoryValidator() { 
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("Category name is required")
				.MaximumLength(20).WithMessage("Category length should not be higher than 20");
			RuleFor(c => c.DisplayOrder)
				.NotEmpty().WithMessage("Category display order is required")
				.InclusiveBetween(1, 100).WithMessage("category display order should be between 1 and 100");
		}

		public IDictionary<string, string[]> ValidateModel(Category category)
		{
			ValidationResult results = Validate(category);

			if (!results.IsValid)
			{
				return results.ToDictionary();
			}

			return null;
		}
	}
}
