using System.Collections;
using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.Extensions;
public static class StringExtensions
{
    public static Result Validate(this string value, string fieldName, 
        int maxLength, string module)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure(
                Error.Validation(
                    $"{module}.NullValue",
                    $"{fieldName} cannot be null or empty."));
        }

        if (value.Length > maxLength)
        {
            return Result.Failure(
                Error.Validation(
                    $"{module}.MaxLength",
                    $"{fieldName} cannot be longer than {maxLength} characters."));
        }

        return Result.Success();
    }

    public static Result ValidateIfHasValue(this string? value, string fieldName,
        int maxLength, string module)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
        {
            
                return Result.Failure(
                    Error.Validation(
                        $"{module}.MaxLength",
                        $"{fieldName} cannot be longer than {maxLength} characters."));
            

        }

        return Result.Success();
    }
}
