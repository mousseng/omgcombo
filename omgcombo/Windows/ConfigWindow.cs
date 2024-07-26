using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using ImGuiNET;

namespace omgcombo.Windows;

public sealed class ConfigWindow : Window
{
    private readonly IDalamudPluginInterface _dalamud;
    private readonly Configuration _config;
    private readonly IconReplacer _iconReplacer;

    public ConfigWindow(
        IDalamudPluginInterface dalamud,
        Configuration config,
        IconReplacer iconReplacer)
        : base("omgcombo config")
    {
        _dalamud = dalamud;
        _config = config;
        _iconReplacer = iconReplacer;
    }

    public override void Draw()
    {
        if (ImGui.CollapsingHeader("AST"))
        {
            DrawConfigItem("Place Play I on Draw", ref _config.Ast.PlacePlay1OnDraw);
            DrawConfigItem("Place Play II on Exaltation", ref _config.Ast.PlacePlay2OnExaltation);
            DrawConfigItem("Place Play III on Intersection", ref _config.Ast.PlacePlay3OnIntersection);
        }

        if (ImGui.CollapsingHeader("GNB"))
        {
            DrawConfigItem("Replace Solid Barrel Combo", ref _config.Gnb.DoSolidBarrelCombo);
            DrawConfigItem("Replace Demon Slaughter Combo", ref _config.Gnb.DoDemonSlaughterCombo);
        }

        if (ImGui.CollapsingHeader("SAM"))
        {
            DrawConfigItem("Replace Gekko Combo", ref _config.Sam.DoGekkoCombo);
            DrawConfigItem("Replace Kasha Combo", ref _config.Sam.DoKashaCombo);
            DrawConfigItem("Replace Yukikaze Combo", ref _config.Sam.DoYukikazeCombo);
            DrawConfigItem("Replace Mangetsu Combo", ref _config.Sam.DoMangetsuCombo);
            DrawConfigItem("Replace Oka Combo", ref _config.Sam.DoOkaCombo);
            DrawConfigItem("Replace Iaijutsu", ref _config.Sam.DoIaijutsu);
            DrawConfigItem("Replace Ikishoten", ref _config.Sam.DoIkishoten);
        }

        if (ImGui.CollapsingHeader("SMN"))
        {
            DrawConfigItem("Replace Energy Drain with Fester", ref _config.Smn.EnergyDrainIntoFester);
            DrawConfigItem("Replace Energy Siphon with Painflare", ref _config.Smn.EnergySiphonIntoPainflare);
        }
    }

    private void DrawConfigItem(string label, ref bool configItem)
    {
        if (ImGui.Checkbox($"{label}", ref configItem))
        {
            _dalamud.SavePluginConfig(_config);
            _iconReplacer.Build(_config);
        }
    }
}
