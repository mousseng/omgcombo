using Dalamud.Game.ClientState.Statuses;
using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Player
{
    private static IObjectTable clientState;
    public static int Level => clientState.LocalPlayer?.Level ?? 0;

    public static bool HasBuff(uint buffId)
    {
        var buffs = clientState.LocalPlayer?.StatusList;
        return buffs is not null && buffs.Any(buff => buff.StatusId == buffId);
    }

    public static void Init(IObjectTable clientState)
    {
        Player.clientState = clientState;
    }
}
