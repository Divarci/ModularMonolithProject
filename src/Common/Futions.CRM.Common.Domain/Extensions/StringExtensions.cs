using System.Text.RegularExpressions;
using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.Extensions;
public static class StringExtensions
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static Result Validate(
        this string? value,
        string fieldName,
        int maxLength,
        string module,
        bool isRequired = true,
        bool isEmail = false)
    {
        if (isRequired && string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure(
                Error.Validation(
                    $"{module}.NullValue",
                    $"{fieldName} cannot be null or empty."));
        }

        if (!string.IsNullOrWhiteSpace(value))
        {
            if (value.Length > maxLength)
            {
                return Result.Failure(
                    Error.Validation(
                        $"{module}.MaxLength",
                        $"{fieldName} cannot be longer than {maxLength} characters."));
            }

            if (isEmail && !EmailRegex.IsMatch(value))
            {
                return Result.Failure(
                    Error.Validation(
                        $"{module}.InvalidEmail",
                        $"{fieldName} must be a valid email address."));
            }
        }

        return Result.Success();
    }
}
