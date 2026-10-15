namespace Orders.Application.Erros
{
    public static class ValidationFailureExtension
    {
        public static IList<CustomValidationFailure> ToCustomValidationFailure(this IEnumerable<FluentValidation.Results.ValidationFailure> failures)
        {
            return failures.Select(f => new CustomValidationFailure(f.ErrorMessage, f.PropertyName)).ToList();
        }
    }
}