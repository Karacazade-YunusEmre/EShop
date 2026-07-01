namespace EShop.Application.Abstractions.Messaging;

/// <summary>
/// Sistemin durumunu DEĞİŞTİREN bir isteği temsil eder (yaz/güncelle/sil).
/// </summary>
public interface ICommand<TResponse> : IRequest<TResponse>
{
}