using FluentValidation;

namespace Dima.Web.Utils;

public static class ValidatorsUtils
{
    public static Func<object, string, IEnumerable<string>> GetValidatorFormCallback<TModel>(AbstractValidator<TModel> validator)
    {
        return (model, propertyName) =>
        {
            var ctxModel = ValidationContext<TModel>.CreateWithOptions(
                    (TModel)model, 
                    x => x.IncludeProperties(propertyName));
            var result = validator.Validate(ctxModel);
            return result.IsValid ? [] : result.Errors.Select(e => e.ErrorMessage);
        };
    }
    
    public static Func<object, string, Task<IEnumerable<string>>> GetValidatorFormCallbackAsync<TModel>(AbstractValidator<TModel> validator)
    {
        return async (model, propertyName) =>
        {
            var ctxModel = ValidationContext<TModel>.CreateWithOptions(
                (TModel)model, 
                x => x.IncludeProperties(propertyName));
            var result = await validator.ValidateAsync(ctxModel);
            return result.IsValid 
                ? Array.Empty<string>() 
                : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}