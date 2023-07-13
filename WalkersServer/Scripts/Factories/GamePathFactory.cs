using WalkersServer.Scripts.Core;

namespace WalkersServer.Scripts.Factories;

public class GamePathFactory
{

    public List<GamePathNode> CreateGamePath()
    {
        List<GamePathNode> gamePath = new List<GamePathNode>();
        
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, 6, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, 10, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, 11, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));
        gamePath.Add(new GamePathNode(gamePath.Count, GamePathNode.EmptyIndex, 0));

        return gamePath;
    }


}