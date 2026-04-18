using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Plugin.Services;

namespace omgcombo.Services;

public static class Gauges
{
    private static IJobGauges jobGauges;

    public static T Get<T>() where T : JobGaugeBase
    {
        return jobGauges.Get<T>();
    }

    public static void Init(IJobGauges gauges)
    {
        jobGauges = gauges;
    }
}
