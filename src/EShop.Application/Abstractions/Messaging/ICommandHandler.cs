namespace EShop.Application.Abstractions.Messaging;

/// <summary>
/// Bir command'i işleyen handler'ın sözleşmesi.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}