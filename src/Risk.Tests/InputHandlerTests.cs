using Risk;

public class InputHandlerTests
{
    [Theory]
    [InlineData("cat")]
    [InlineData("dog")]
    public void ReturnsGivenString(string value)
    {
        StringReader reader = new StringReader(value);
        Console.SetIn(reader);

        string input = InputHandler.GetInput();

        Assert.Equal(value, input);
    }

    [Fact]
    public void ThrowsExceptionAtEof()
    {
        StringReader reader = new StringReader("dog");
        Console.SetIn(reader);

        _ = InputHandler.GetInput();

        Assert.Throws<EndOfStreamException>(() => InputHandler.GetInput());
    }
}
