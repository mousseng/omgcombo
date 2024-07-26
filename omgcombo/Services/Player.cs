using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Player
{
    private static IClientState ClientState;
    public static int Level => ClientState.LocalPlayer?.Level ?? default;
    // public static uint Job => ClientState.LocalPlayer?.ClassJob.Id ?? default;

    public static bool HasBuff(uint buffId)
    {
        var buffs = ClientState.LocalPlayer?.StatusList;
        return buffs is not null && buffs.Any(buff => buff.StatusId == buffId);
    }

    public static void Init(IClientState clientState)
    {
        ClientState = clientState;
    }
}
