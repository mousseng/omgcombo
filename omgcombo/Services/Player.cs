using Dalamud.Game.ClientState.Statuses;
using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Player
{
    private static IClientState clientState;
    public static int Level => clientState.LocalPlayer?.Level ?? default;
    // public static uint Job => ClientState.LocalPlayer?.ClassJob.Id ?? default;
    public static StatusList? Buffs => clientState.LocalPlayer?.StatusList;

    public static bool HasBuff(uint buffId)
    {
        var buffs = clientState.LocalPlayer?.StatusList;
        return buffs is not null && buffs.Any(buff => buff.StatusId == buffId);
    }

    public static void Init(IClientState clientState)
    {
        Player.clientState = clientState;
    }
}
