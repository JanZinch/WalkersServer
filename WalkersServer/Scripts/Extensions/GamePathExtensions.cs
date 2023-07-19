using WalkersServer.Scripts.Core;
using WalkersServer.Scripts.Core.DataModels;

namespace WalkersServer.Scripts.Extensions
{
    public static class GamePathExtensions
    {
        public static GamePathNodeDataModel[] ToDataModel(this IEnumerable<GamePathNode> gamePathNodes)
        {
            return gamePathNodes.Select(node => node.ToDataModel()).ToArray();
        }
    }
}


