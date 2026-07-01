using EShop.Domain.Results;

using Xunit.Abstractions;

namespace EShop.Domain.UnitTests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        Result<string> result = "merhaba"; // implicit operator → başarılı sonuç
        _testOutputHelper.WriteLine($"[{result.Value}]");
    }
}