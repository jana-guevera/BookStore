﻿using Domain.ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Validates the model and throws ValidationError if validation failed
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ValidationError"></exception>
        public static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            
            if (!isValid)
            {
                throw new ModelValidationException("Model validation failed", validationResults);
            }
        }
    }
}
