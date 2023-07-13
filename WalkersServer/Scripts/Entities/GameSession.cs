using WalkersServer.Scripts.Core;
using WalkersServer.Scripts.Factories;

namespace WalkersServer.Scripts.Entities;

public class GameSession
{
    private string _id;
    private GameSessionState _state = GameSessionState.None;
    private LinkedList<Player> _players = new LinkedList<Player>();
    private GamePathFactory _pathFactory = null;

    public event Action<int> OnPlayersCountChanged;
    
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
            OnPlayersCountChanged?.Invoke(_players.Count);
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
            OnPlayersCountChanged?.Invoke(_players.Count);
            return true;
        }
        else
        {
            return false;
        }
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