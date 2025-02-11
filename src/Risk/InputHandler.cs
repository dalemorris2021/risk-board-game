namespace Risk;

public class InputHandler {
    public static string GetInput() {
        const string END_OF_INPUT_MESSAGE = "Reached end of input";
        string? input = Console.ReadLine() ?? throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
        return input;
    }
}