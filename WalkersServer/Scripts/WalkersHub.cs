using Microsoft.AspNetCore.SignalR;
using WalkersServer.Scripts.Core;
using WalkersServer.Scripts.Core.DataModels;
using WalkersServer.Scripts.Entities;
using WalkersServer.Scripts.Extensions;
using WalkersServer.Scripts.Factories;

namespace WalkersServer.Scripts;

public class WalkersHub : Hub
{
    private readonly LinkedList<GameSession> _gameSessions = new LinkedList<GameSession>();
    
    public void CreateGameSession(PlayerDataModel hostModel)
    {
        Player host = new Player(hostModel);
        GameSession gameSession = new GameSession(host, out string gameSessionId);
        gameSession.OnPlayersCountChanged += OnPlayersCountChanged;
        _gameSessions.AddLast(gameSession);

        Clients.Client(hostModel.Id).SendAsync("OnGameSessionCreated", gameSessionId);
        
        Console.WriteLine("Created session: " + gameSessionId);
    }

    private void OnPlayersCountChanged(GameSession gameSession, int playersCount)
    {
        if (playersCount > 1)
        {
            List<GamePathNode> gamePath = gameSession.Start();
            Clients.Clients(gameSession.GetAllPlayerIds()).SendAsync("OnGameSessionStarted", gamePath.ToDataModel());
        }
        else
        {
            Clients.Clients(gameSession.GetAllPlayerIds()).SendAsync("OnPlayersCountChanged", playersCount);
        }
    }


    
    public void Send(string message)
    {
        Console.WriteLine("Received MSG");
        
        Clients.All.SendAsync("Ping", message);
    }

}