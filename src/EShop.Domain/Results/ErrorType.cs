namespace EShop.Domain.Results;

/// <summary>
/// Hatanın türü. İleride API katmanında HTTP durum koduna eşlenecek.
/// </summary>
public enum ErrorType
{
    None = 0,
    Failure = 1,
    Validation = 2,
    NotFound = 3,
    Conflict = 4,
}