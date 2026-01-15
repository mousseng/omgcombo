using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Bindings.ImGui;

namespace omgcombo.Windows;

public sealed class ConfigWindow(
    IDalamudPluginInterface dalamud,
    Configuration config,
    IconReplacer iconReplacer)
    : Window("omgcombo config")
{
    public override void Draw()
    {
        if (ImGui.CollapsingHeader("AST"))
        {
            DrawConfigItem("Place Play I on Draw", ref config.Ast.PlacePlay1OnDraw);
            DrawConfigItem("Place Play II on Exaltation", ref config.Ast.PlacePlay2OnExaltation);
            DrawConfigItem("Place Play III on Intersection", ref config.Ast.PlacePlay3OnIntersection);
        }

        if (ImGui.CollapsingHeader("BLM"))
        {
            DrawConfigItem("Place Freeze on Flare", ref config.Blm.PlaceFreezeOnFlare);
            DrawConfigItem("Place Play II on Exaltation", ref config.Blm.PlaceFlareStarOnDespair);
        }

        if (ImGui.CollapsingHeader("GNB"))
        {
            DrawConfigItem("Replace Solid Barrel Combo", ref config.Gnb.DoSolidBarrelCombo);
            DrawConfigItem("Replace Demon Slaughter Combo", ref config.Gnb.DoDemonSlaughterCombo);
        }

        if (ImGui.CollapsingHeader("RDM"))
        {
            DrawConfigItem("Replace Verfire with Jolt", ref config.Rdm.ReplaceVerfire);
            DrawConfigItem("Replace Verstone with Jolt", ref config.Rdm.ReplaceVerstone);
            DrawConfigItem("Replace Enchanted Combo", ref config.Rdm.DoEnchantedCombo);
        }

        if (ImGui.CollapsingHeader("SAM"))
        {
            DrawConfigItem("Replace Gekko Combo", ref config.Sam.DoGekkoCombo);
            DrawConfigItem("Replace Kasha Combo", ref config.Sam.DoKashaCombo);
            DrawConfigItem("Replace Yukikaze Combo", ref config.Sam.DoYukikazeCombo);
            DrawConfigItem("Replace Mangetsu Combo", ref config.Sam.DoMangetsuCombo);
            DrawConfigItem("Replace Oka Combo", ref config.Sam.DoOkaCombo);
            DrawConfigItem("Replace Iaijutsu", ref config.Sam.DoIaijutsu);
            DrawConfigItem("Replace Ikishoten", ref config.Sam.DoIkishoten);
        }

        if (ImGui.CollapsingHeader("SMN"))
        {
            DrawConfigItem("Replace Energy Drain with Fester", ref config.Smn.EnergyDrainIntoFester);
            DrawConfigItem("Replace Energy Siphon with Painflare", ref config.Smn.EnergySiphonIntoPainflare);
        }
    }

    private void DrawConfigItem(string label, ref bool configItem)
    {
        if (ImGui.Checkbox($"{label}", ref configItem))
        {
            dalamud.SavePluginConfig(config);
            iconReplacer.Build(config);
        }
    }
}
