using Dalamud.Bindings.ImGui;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using omgcombo.Services;

namespace omgcombo.Windows;

public sealed unsafe class DebugWindow: Window
{
    private readonly IDalamudPluginInterface _dalamud;
    private readonly Configuration _config;

    public DebugWindow(
        IDalamudPluginInterface dalamud,
        Configuration config)
        : base("omgcombo debug")
    {
        _dalamud = dalamud;
        _config = config;
    }

    public override void Draw()
    {
        ImGui.Text("put some debug stuff here");
        var blm = Gauges.Get<BLMGauge>();
        ImGui.Text($"Astral Soul: {blm.AstralSoulStacks}");
    }
}
