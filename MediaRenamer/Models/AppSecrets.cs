using FluentValidation;
using FluentValidation.Results;

namespace MediaRenamer.Models;

public class AppSecrets
{
    public string ApiKeyTMDb { get; set; } = string.Empty;


    #region methods
    public bool TryValidate(out IEnumerable<ValidationFailure> errors)
    {
        var validator = new AppSecretsValidator();
        var result = validator.Validate(this);
        errors = result.Errors;
        return result.IsValid;
    }
    #endregion
}

public class AppSecretsValidator : AbstractValidator<AppSecrets>
{
    public AppSecretsValidator()
    {
        RuleFor(config => config.ApiKeyTMDb).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("You must provide an api key");
    }
}
