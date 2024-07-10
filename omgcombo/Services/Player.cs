using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Player
{
    private static IClientState ClientState;
    public static int Level => ClientState.LocalPlayer?.Level ?? default;
    // public static uint Job => ClientState.LocalPlayer?.ClassJob.Id ?? default;

    public static void Init(IClientState clientState)
    {
        ClientState = clientState;
    }
}
