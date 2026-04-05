using System.ComponentModel.DataAnnotations;

namespace Lilys_CM.Shared.Options;

public sealed class FrontendOptions
{
    public const string SectionName = "Frontend";

    [Required]
    public string BaseUrl { get; init; } = default!;
}