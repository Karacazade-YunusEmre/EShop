namespace EShop.Domain.Results;

/// <summary>
/// Değer döndüren işlemlerin sonucunu taşır. Başarılıysa Value erişilebilir,
/// başarısızsa Value'ya erişim hatadır.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public sealed class Result<TValue> : Result
{
    /// <summary>
    ///  Başarılı sonucun değeri. Başarısız bir sonuçta erişmek istisna fırlatır.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public TValue Value => IsSuccess
        ? field!
        : throw new InvalidOperationException("Başarısız bir sonucun değerine erişilemez.");

    internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        this.Value = value!;
    }


    /// <summary>
    /// Bir değerden örtük (implicit) Result üretir: null ise başarısız, değilse başarılı.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null
            ? Success(value)
            : Failure<TValue>(Error.NullValue);
}