namespace EncheresPredict.Application.Common.Interfaces;

public interface IN8nClient
{
    Task SendDocumentForAnalysisAsync(
        Guid summaryId,
        string pdfUrl,
        CancellationToken cancellationToken = default);
}