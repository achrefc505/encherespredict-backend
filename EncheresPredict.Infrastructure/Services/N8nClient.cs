using System.Net.Http.Json;
using EncheresPredict.Application.Common.Configuration;
using EncheresPredict.Application.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace EncheresPredict.Infrastructure.Services;

public sealed class N8nClient(
    HttpClient httpClient,
    IOptions<N8nOptions> options)
    : IN8nClient
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly N8nOptions _options = options.Value;

    public async Task SendDocumentForAnalysisAsync(
        Guid summaryId,
        string pdfUrl,
        CancellationToken cancellationToken = default)
    {
        var payload = new
        {
            summaryId,
            pdfUrl,
            callbackUrl = _options.CallbackUrl
        };

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            _options.WebhookUrl);

        request.Headers.Add("x-secret", _options.Secret);

        request.Content = JsonContent.Create(payload);

        var response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}