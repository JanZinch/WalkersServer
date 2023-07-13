using System.Diagnostics.Contracts;

namespace WalkersServer.Scripts.Entities;

public class ConnectionGroup
{
    private LinkedList<string> _connectionIds;
    private GameSession _linkedSession;

    public GameSession LinkedSession => _linkedSession;
    
    public event Action<ConnectionGroup, int> OnPlayersCountChanged; 

    public ConnectionGroup(string hostConnectionId, GameSession linkedSession)
    {
        _connectionIds = new LinkedList<string>();
        _connectionIds.AddLast(hostConnectionId);
        
        _linkedSession = linkedSession;
        _linkedSession.OnPlayersCountChanged += (count) =>
        {
            OnPlayersCountChanged?.Invoke(this, count);
        };
    }
    
    [Pure]
    public List<string> GetConnectionIds()
    {
        List<string> idsOutput = new List<string>(_connectionIds);

        foreach (string id in _connectionIds)
        {
            idsOutput.Add(id);
        }

        return idsOutput;
    }
}