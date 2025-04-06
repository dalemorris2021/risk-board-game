using System.Net.Sockets;
using System.Text;

namespace Risk;

public class Player
{
    private TcpClient client;

    public Player(TcpClient client)
    {
        this.client = client;
    }

    public async Task SendActions(IList<Action> actions)
    {
        using NetworkStream stream = client.GetStream();
        var message = string.Join(",", actions);
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(messageBytes);
    }

    public async Task<Action?> ReceiveAction()
    {
        using NetworkStream stream = client.GetStream();
        byte[] messageBuf = new byte[stream.Length];
        await stream.ReadAsync(messageBuf, 0, (int)stream.Length);
        var message = messageBuf.ToString();

        switch (message.ToLower())
        {
            case "deploy": return Action.DEPLOY;
            case "attack": return Action.ATTACK;
            case "fortify": return Action.FORTIFY;
            case "info": return Action.INFO;
            case "end": return Action.END;
            default: return null;
        }
    }
}
