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
    private readonly ASTGauge astGauge = Gauges.Get<ASTGauge>();

    public override void Draw()
    {
        if (ImGui.CollapsingHeader("AST"))
        {
            ImGui.Text($"Draw Type: {astGauge.ActiveDraw}");
            ImGui.Text($"Card 0: {astGauge.DrawnCards[0]}");
            ImGui.Text($"Card 1: {astGauge.DrawnCards[1]}");
            ImGui.Text($"Card 2: {astGauge.DrawnCards[2]}");
            ImGui.Text($"Crown: {astGauge.DrawnCrownCard}");
        }
    }
}
