namespace EShop.Application.Abstractions.Messaging;

/// <summary>
/// Bir query'yi işleyen handler'ın sözleşmesi.
/// </summary>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}