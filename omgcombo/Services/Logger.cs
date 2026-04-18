using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Logger
{
    private static IPluginLog pluginLog;

    public static void Init(IPluginLog pluginLog)
    {
        Logger.pluginLog = pluginLog;
    }

    public static void Debug(string messageTemplate, params object[] values)
    {
        pluginLog.Debug(messageTemplate, values);
    }
}
