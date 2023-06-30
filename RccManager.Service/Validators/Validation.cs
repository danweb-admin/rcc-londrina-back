

using System.ComponentModel.DataAnnotations;

namespace RccManager.Service.Validators;

public static class Validation
{
    public static IEnumerable<ValidationResult> getValidationErros(object obj)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext (obj, null, null);
        Validator.TryValidateObject(obj, context, validationResults, true);
        return validationResults;
    }
}

