namespace EShop.Application.Abstractions.Messaging;

/// <summary>
/// Bir isteği işleyip cevabını üreten sınıfın sözleşmesi.
/// </summary>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken ct);
}