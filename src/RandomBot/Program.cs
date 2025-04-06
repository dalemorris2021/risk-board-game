using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RandomBot;

public class Program(string name, Color color)
{
    public static async Task Main(string[] args)
    {
        var hostName = Dns.GetHostName();
        IPHostEntry localhost = await Dns.GetHostEntryAsync(hostName);
        // This is the IP address of the local machine
        IPAddress localIpAddress = localhost.AddressList[0];

        var ipEndPoint = new IPEndPoint(localIpAddress, 1234);

        using TcpClient client = new();
        await client.ConnectAsync(ipEndPoint);
        await using NetworkStream stream = client.GetStream();

        var buffer = new byte[1_024];
        int received = await stream.ReadAsync(buffer);

        var message = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine($"Message received: \"{message}\"");
        // Sample output:
        //     Message received: "📅 8/22/2022 9:07:17 AM 🕛"
    }
}
