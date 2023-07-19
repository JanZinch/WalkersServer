using WalkersServer.Scripts.Core.DataModels;

namespace WalkersServer.Scripts.Core;

public class Player
{
    public string Id { get; private set; }
    public PlayerRole Role { get; private set; }

    public Player(PlayerDataModel dataModel)
    {
        Id = dataModel.Id;
        Role = dataModel.Role;
    }

    public PlayerDataModel ToDataModel()
    {
        return new PlayerDataModel()
        {
            Id = Id,
            Role = Role,
        };
    }

}