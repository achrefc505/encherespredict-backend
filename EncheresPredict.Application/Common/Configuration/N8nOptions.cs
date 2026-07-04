namespace EncheresPredict.Application.Common.Configuration;

public sealed class N8nOptions
{
    public const string SectionName = "N8n";

    public string WebhookUrl { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;

    public string CallbackUrl { get; set; } = string.Empty;
}