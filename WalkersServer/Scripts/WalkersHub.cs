using Microsoft.AspNetCore.SignalR;
using WalkersServer.Scripts.Core;
using WalkersServer.Scripts.Entities;
using WalkersServer.Scripts.Factories;

namespace WalkersServer.Scripts;

public class WalkersHub : Hub
{
    private LinkedList<ConnectionGroup> _connectionGroups = new LinkedList<ConnectionGroup>();
    
    public void CreateGameSession(Player host)
    {
        GameSession gameSession = new GameSession(host, out string gameSessionId);
        
        ConnectionGroup connectionGroup = new ConnectionGroup(Context.ConnectionId, gameSession);
        connectionGroup.OnPlayersCountChanged += OnPlayersCountChanged;
        _connectionGroups.AddLast(connectionGroup);

        //TODO Another way | player Id == connectionId
        
        Clients.Client(Context.ConnectionId).SendAsync("OnGameSessionCreated", gameSessionId);
    }

    private void OnPlayersCountChanged(ConnectionGroup connectionGroup, int playersCount)
    {
        if (playersCount > 1)
        {
            List<GamePathNode> gamePath = connectionGroup.LinkedSession.Start();
            Clients.Clients(connectionGroup.GetConnectionIds()).SendAsync("OnGameSessionStarted", gamePath);
        }
        else
        {
            Clients.Clients(connectionGroup.GetConnectionIds()).SendAsync("OnPlayersCountChanged", playersCount);
        }
    }


    
    public void Send(string message)
    {
        Console.WriteLine("Received MSG");
        
        Clients.All.SendAsync("Ping", message);
    }

}