using MediatR;

namespace EncheresPredict.Application.Alerts.Commands.MarkAlertAsRead;

public sealed record MarkAlertAsReadCommand(Guid Id) : IRequest;
