using Microsoft.AspNetCore.SignalR;

namespace WalkersServer.Scripts;

public class WalkersHub : Hub
{

    public void Send(string message)
    {
        Console.WriteLine("Received MSG");
        
        Clients.All.SendAsync("Ping", message);
    }

}