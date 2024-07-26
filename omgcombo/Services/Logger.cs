using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Logger
{
    private static IPluginLog PluginLog;

    public static void Init(IPluginLog pluginLog)
    {
        PluginLog = pluginLog;
    }

    public static void Debug(string messageTemplate, params object[] values)
    {
        PluginLog.Debug(messageTemplate, values);
    }
}
