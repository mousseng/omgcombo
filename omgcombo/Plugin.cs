using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using omgcombo.Services;
using omgcombo.Windows;

namespace omgcombo;

public sealed class Plugin : IDalamudPlugin
{
    [PluginService] private static IDalamudPluginInterface Dalamud { get; set; }
    [PluginService] private static IGameInteropProvider Interop { get; set; }
    [PluginService] private static IFramework Framework { get; set; }
    [PluginService] private static IClientState Game { get; set; }
    [PluginService] private static IJobGauges JobGauges { get; set; }
    [PluginService] private static IPluginLog PluginLog { get; set; }

    private WindowSystem WindowSystem { get; } = new("omgcombo");
    private Configuration Config { get; }
    private ConfigWindow? ConfigWindow { get; set; }
    private DebugWindow? DebugWindow { get; set; }
    private IconReplacer? IconReplacer { get; set; }

    public Plugin()
    {
        Config = Dalamud.GetPluginConfig() as Configuration ?? new Configuration();

        // set up the data we need, or defer until we're logged in
        if (Game.IsLoggedIn)
        {
            InitStatics();
            InitPlugin();
        }
        else
        {
            Game.Login += InitStatics;
            Game.Login += InitPlugin;
        }
    }

    private void InitPlugin()
    {
        IconReplacer = new IconReplacer(Interop);
        Framework.Update += WatchPlayerLevel;

        ConfigWindow = new ConfigWindow(Dalamud, Config, IconReplacer);
        DebugWindow = new DebugWindow(Dalamud, Config);
        WindowSystem.AddWindow(ConfigWindow);
        WindowSystem.AddWindow(DebugWindow);

        Dalamud.UiBuilder.Draw += WindowSystem.Draw;
        Dalamud.UiBuilder.OpenConfigUi += ConfigWindow.Toggle;
        Dalamud.UiBuilder.OpenMainUi += DebugWindow.Toggle;
    }

    private static void InitStatics()
    {
        Combos.Init();
        Gauges.Init(JobGauges);
        Player.Init(Game);
        Logger.Init(PluginLog);
    }

    private int _lastLevel;
    private void WatchPlayerLevel(IFramework framework)
    {
        if (Player.Level == _lastLevel)
        {
            return;
        }

        IconReplacer!.Build(Config);
        _lastLevel = Player.Level;
    }

    public void Dispose()
    {
        Framework.Update -= WatchPlayerLevel;
        WindowSystem.RemoveAllWindows();
        IconReplacer?.Dispose();
    }
}
