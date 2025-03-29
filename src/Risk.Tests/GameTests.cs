using System.Drawing;
using Risk;

public class GameTests
{
    [Fact]
    public void TwoBotsCanPlay()
    {
        IList<IPlayer> players = [new RandomBot("John", Color.Black), new RandomBot("George", Color.Blue)];
        Game game = new Game(players);
        game.Run();
    }

    [Fact]
    public void SixBotsCanPlay()
    {
        IList<IPlayer> players = [
            new RandomBot("Alan", Color.Red),
            new RandomBot("Barry", Color.Green),
            new RandomBot("Callie", Color.Blue),
            new RandomBot("Dorothy", Color.Cyan),
            new RandomBot("Edgar", Color.Magenta),
            new RandomBot("Franklin", Color.Yellow),
        ];
        Game game = new Game(players);
        game.Run();
    }
}