using Microsoft.AspNetCore.Http;

namespace EncheresPredict.Api.Contracts.Auctions;

public sealed class UploadCcvRequest
{
    public IFormFile Pdf { get; set; } = null!;
}