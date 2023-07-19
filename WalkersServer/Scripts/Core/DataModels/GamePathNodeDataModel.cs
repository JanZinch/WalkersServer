namespace WalkersServer.Scripts.Core.DataModels;

public struct GamePathNodeDataModel
{
    public int Index { get; set; }
    public int BypassTargetIndex { get; set; }
    public long Bonus { get; set; }
    public PlayerDataModel[] Players { get; set; }
}