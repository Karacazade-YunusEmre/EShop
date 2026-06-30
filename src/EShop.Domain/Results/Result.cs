namespace EShop.Domain.Results;

/// <summary>
/// Bir işlemin başarı/başarısızlık sonucunu taşır. Değer döndürmeyen işlemler için.
/// </summary>
public class Result
{
    protected bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException("Başarılı sonuç bir hata içeremez.");
            case false when error == Error.None:
                throw new InvalidOperationException("Başarısız sonuç bir hata içermelidir.");
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    protected static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    protected static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);
}