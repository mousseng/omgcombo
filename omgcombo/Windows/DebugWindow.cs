using Dalamud.Bindings.ImGui;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using omgcombo.Services;

namespace omgcombo.Windows;

public sealed unsafe class DebugWindow(
    IDalamudPluginInterface dalamud,
    Configuration config)
    : Window("omgcombo debug")
{
    public override void Draw()
    {
        ImGui.Text("put some debug stuff here");
        var blm = Gauges.Get<BLMGauge>();
        ImGui.Text($"Astral Soul: {blm.AstralSoulStacks}");
    }
}
