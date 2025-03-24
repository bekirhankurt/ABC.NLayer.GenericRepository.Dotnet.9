using FluentValidation;
using System.Configuration;

namespace Core.CrossCuttingConcerns.Validation;

public static class ValidationTool
{
    public static void Validate(IValidator validator, IValidationContext entity)
    {
        var result = validator.Validate(entity);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}