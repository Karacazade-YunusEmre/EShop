namespace EShop.Application.Abstractions.Messaging;

/// <summary>
/// TResponse tipinde cevap dönen bir isteği (use-case) temsil eder.
/// Tüm command ve query'lerin ortak atası.
/// </summary>
public interface IRequest<TResponse>
{
}