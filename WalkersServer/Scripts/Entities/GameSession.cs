using System.Diagnostics.Contracts;
using WalkersServer.Scripts.Core;
using WalkersServer.Scripts.Factories;

namespace WalkersServer.Scripts.Entities;

public class GameSession
{
    private string _id;
    private GameSessionState _state = GameSessionState.None;
    private LinkedList<Player> _players = new LinkedList<Player>();
    private GamePathFactory _pathFactory = null;

    public event Action<GameSession, int> OnPlayersCountChanged;
    
    public GameSession(Player host, out string sessionId)
    {
        _state = GameSessionState.Lobby;
        _players.AddLast(host);
        _id = GenerateId(8);
        sessionId = _id;
    }

    private static string GenerateId(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        
        return new string(Enumerable.Repeat(chars, length).Select(str => str[random.Next(str.Length)]).ToArray());
    }
    
    public bool AddPlayer(Player player)
    {
        if (_state == GameSessionState.Lobby)
        {
            _players.AddLast(player);
            OnPlayersCountChanged?.Invoke(this, _players.Count);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool RemovePlayer(Player player)
    {
        if (_state == GameSessionState.Lobby)
        {
            _players.Remove(player);
            OnPlayersCountChanged?.Invoke(this, _players.Count);
            return true;
        }
        else
        {
            return false;
        }
    }

    [Pure]
    public List<string> GetAllPlayerIds()
    {
        List<string> idsOutput = new List<string>(_players.Count);

        foreach (Player player in _players)
        {
            idsOutput.Add(player.Id);
        }

        return idsOutput;
    }

    public List<GamePathNode> Start()
    {
        if (_state != GameSessionState.Lobby || _players.Count < 2)
        {
            return null;
        }

        _state = GameSessionState.Match;
        _pathFactory = new GamePathFactory();

        return _pathFactory.CreateGamePath();
    }


}