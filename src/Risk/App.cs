using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Risk;

public class App {
    public static async Task Main(string[] args) {
        Console.WriteLine("Welcome to Risk!");
        Console.WriteLine("----------------");
        Console.WriteLine("Need 2 to 6 players to start.");
        Console.WriteLine();

        var ipEndPoint = new IPEndPoint(IPAddress.Any, 1234);
        TcpListener listener = new(ipEndPoint);

        // TODO: Limit numConnections to 6 at most
        var numConnections = 4;

        try
        {    
            listener.Start();
            IList<TcpClient> handlers = [];

            for (int i = 0; i < numConnections; i++) {
                TcpClient handler = await listener.AcceptTcpClientAsync();
                handlers.Add(handler);
            }

            var handlerIndex = 0;
            bool looping = true;
            while (looping) {
                try {
                    await using NetworkStream stream = handlers[handlerIndex].GetStream();
                    var message = $"📅 {DateTime.Now} 🕛";
                    var dateTimeBytes = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(dateTimeBytes);

                    Console.WriteLine($"Sent message: \"{message}\"");
                    // Sample output:
                    //     Sent message: "📅 8/22/2022 9:07:17 AM 🕛"

                    handlerIndex += 1;
                    if (handlerIndex >= numConnections) {
                        handlerIndex = 0;
                    }
                } catch (InvalidOperationException)
                {
                    Console.Error.WriteLine("Client disconnected");
                    looping = false;
                }
            }
        }
        finally
        {
            listener.Stop();
        }

        /*
        int numPlayers = GetNumPlayers();
        IList<IPlayer> players = CreatePlayers(numPlayers);

        Game game = new Game(players);
        Thread gameThread = new Thread(new ThreadStart(game.Run));
        gameThread.Start();
        */
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

    /*
    private static IList<IPlayer> CreatePlayers(int numPlayers) {
        IList<Color> colors = [Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Yellow, Color.Orange];
        IList<IPlayer> players = [];
        for (int i = 0; i < numPlayers; i++) {
            Console.WriteLine($"Enter player {i + 1}'s name.");
            string name = InputHandler.GetInput();
            bool isBot;
            string input;
            while (true) {
                Console.WriteLine($"Is player {i + 1} a human (0) or bot (1)?");
                input = InputHandler.GetInput();
                if (input == "0") {
                    isBot = false;
                    break;
                } else if (input == "1") {
                    isBot = true;
                    break;
                } else {
                    Console.WriteLine("Please enter 0 or 1.");
                }
            }
            
            if (isBot) {
                players.Add(new RandomBot(name, colors[i]));
            } else {
                players.Add(new Player(name, colors[i]));
            }
        }

        if (numPlayers == 2) {
            players.Add(new NeutralBot(colors[2]));
        }

        return players;
    }
    */
}
