using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Gauges
{
    private static IJobGauges JobGauges;

    public static T Get<T>() where T : JobGaugeBase
    {
        return JobGauges.Get<T>();
    }

    public static void Init(IJobGauges gauges)
    {
        JobGauges = gauges;
    }
}
