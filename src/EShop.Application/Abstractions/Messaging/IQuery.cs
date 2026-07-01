namespace EShop.Application.Abstractions.Messaging;

/// <summary>
/// Sistemin durumunu DEĞİŞTİRMEDEN veri OKUYAN bir isteği temsil eder.
/// </summary>
public interface IQuery<TResponse> : IRequest<TResponse>
{
}