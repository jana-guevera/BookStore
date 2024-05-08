using Domain.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddEntityValidationServices(this IServiceCollection services)
		{
			services.AddScoped<CategoryValidator>();
			return services;
		}
	}
}
