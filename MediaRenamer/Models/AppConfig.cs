using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MediaRenamer.Models;

public class AppConfig
{
    public string MovieDestinationPath { get; set; } = string.Empty;
    public string TvDestinationPath { get; set; } = string.Empty;
    public bool OnlyTopDirectory { get; set; }
    public bool Overwrite { get; set; }
    public string? Langugage { get; set; }
    public IEnumerable<string> DefaultMask { get; private set; } = new[] { ".avi", ".m4v", ".mp4", ".mkv", ".ts", ".wmv", ".srt", ".idx", ".sub", ".webm", ".png", ".jpg", ".jpeg" };
    public IEnumerable<string> Mask { get; set; } = Enumerable.Empty<string>();


    #region methods
    public bool TryValidate(out IEnumerable<ValidationFailure> errors)
    {
        var validator = new AppConfigValidator();
        var result = validator.Validate(this);
        errors = result.Errors;
        return result.IsValid;
    }

    public IEnumerable<string> GetMask()
    {
        if (Mask == null || !Mask.Any())
            return DefaultMask;
        return Mask;
    }
    #endregion
}

public class AppConfigValidator : AbstractValidator<AppConfig>
{
    public AppConfigValidator()
    {
        RuleFor(config => config.MovieDestinationPath).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("You must provide a path")
            .Must(path => Directory.Exists(path)).WithMessage((config, path) => $"The path '{path}' doesn't exists");

        RuleFor(config => config.TvDestinationPath).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("You must provide a path")
            .Must(Directory.Exists).WithMessage((config, path) => $"The path '{path}' doesn't exists");

        When(config => config.Langugage != null, () =>
        {
            RuleFor(config => config.Langugage)
                .Length(2).WithMessage("You must provide a valid Language code (with 2 char).");
        });

        RuleForEach(config => config.Mask)
            .Matches(new Regex(@"^\.\w+$")).WithMessage((m, ext) => $"The extension '{ext}' must start with a dot (.) and have, at least, one character");
    }
}

