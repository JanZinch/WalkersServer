using Microsoft.AspNetCore.SignalR;
using WalkersServer.Scripts.Core;
using WalkersServer.Scripts.Core.DataModels;
using WalkersServer.Scripts.Entities;
using WalkersServer.Scripts.Factories;

namespace WalkersServer.Scripts;

public class WalkersHub : Hub
{
    private LinkedList<ConnectionGroup> _connectionGroups = new LinkedList<ConnectionGroup>();

    public void CreateGameSessionI()
    {
        Console.WriteLine("I: debug");
    }

    public void CreateGameSession(PlayerDataModel hostModel)
    {
        Console.WriteLine("Create GS");

        Player host = new Player(hostModel);
        
        GameSession gameSession = new GameSession(host, out string gameSessionId);
        
        ConnectionGroup connectionGroup = new ConnectionGroup(Context.ConnectionId, gameSession);
        connectionGroup.OnPlayersCountChanged += OnPlayersCountChanged;
        _connectionGroups.AddLast(connectionGroup);

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