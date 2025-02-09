using System.Drawing;

namespace Risk;

public class App {
    public static void Main(string[] args) {
        Console.WriteLine("Welcome to Risk!");
        Console.WriteLine("----------------");
        Console.WriteLine("Need 2 to 6 players to start.");
        Console.WriteLine();

        /*
        int numPlayers = GetNumPlayers();
        IList<IPlayer> players = CreatePlayers(numPlayers);
        */
        IList<IPlayer> players = [new RandomBot(), new RandomBot()];

        Game game = new Game(players);
        Thread gameThread = new Thread(new ThreadStart(game.Run));
        gameThread.Start();
    }

    private static int GetNumPlayers() {
        const string ENTER_NUM_PLAYERS_MESSAGE = "Please enter a number between 2 and 6.";

        Console.WriteLine("How many players are there?");
        int numPlayers;
        while (true) {
            string input = InputHandler.GetInput();
            if (Int32.TryParse(input, out numPlayers)) {
                if (numPlayers < 2 || numPlayers > 6) {
                    Console.WriteLine(ENTER_NUM_PLAYERS_MESSAGE);
                } else {
                    break;
                }
            } else {
                Console.WriteLine(ENTER_NUM_PLAYERS_MESSAGE);
            }
        }

        return numPlayers;
    }

    private static IList<IPlayer> CreatePlayers(int numPlayers) {
        IList<Color> colors = [Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Yellow, Color.Orange];
        IList<IPlayer> players = [];
        for (int i = 0; i < numPlayers; i++) {
            Console.WriteLine($"Enter player {i + 1}'s name.");
            string name = InputHandler.GetInput();
            players.Add(new Player(name, colors[i]));
        }

        if (numPlayers == 2) {
            players.Add(new NeutralBot(colors[2]));
        }

        return players;
    }
}
