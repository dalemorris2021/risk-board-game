using System.CommandLine;
using System.Net;
using System.Net.Sockets;

namespace Risk;

public class App
{
    public static async Task<int> Main(string[] args)
    {
        var numPlayersOption = new Option<int>(
            name: "-n",
            description: "The number of players");

        var rootCommand = new RootCommand("Risk board game");
        rootCommand.AddOption(numPlayersOption);

        rootCommand.SetHandler(StartGame, numPlayersOption);

        return await rootCommand.InvokeAsync(args);
    }

    private static async Task StartGame(int numPlayers)
    {
        if (numPlayers < 2 || numPlayers > 6)
        {
            throw new ArgumentException("Invalid number of players");
        }

        var ipEndPoint = new IPEndPoint(IPAddress.Any, 1234);
        TcpListener listener = new(ipEndPoint);

        var clients = await Connect(listener, numPlayers);

        IList<IPlayer> players = [];
        foreach (TcpClient client in clients)
        {
            players.Add(new Player(client));
        }

        Game game = new Game(players);
        Thread gameThread = new Thread(new ThreadStart(game.Run));
        gameThread.Start();
    }

    private static async Task<IList<TcpClient>> Connect(TcpListener listener, int numConnections)
    {
        listener.Start();
        IList<TcpClient> handlers = [];

        for (int i = 0; i < numConnections; i++)
        {
            TcpClient handler = await listener.AcceptTcpClientAsync();
            handlers.Add(handler);
        }

        listener.Stop();

        return handlers;
    }
}
