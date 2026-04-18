using Dalamud.Game.ClientState.JobGauge.Types;
using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Smn : IJob
{
    private readonly SMNGauge gauge = Gauges.Get<SMNGauge>();

    public void Load(Configuration config, IconMap map)
    {
        map.Set(EnergyDrain, MakeEnergyDrain(), config.Smn.EnergyDrainIntoFester);
        map.Set(EnergySiphon, MakeEnergySiphon(), config.Smn.EnergySiphonIntoPainflare);
    }

    private const uint
        EnergyDrain = 16508,
        EnergySiphon = 16510,
        Fester = 181,
        Necrotize = 36990,
        Painflare = 3578;

    private IconReplacer.ReplaceIcon? MakeEnergyDrain() => Player.Level switch
    {
        >= 92 => () => gauge.HasAetherflowStacks ? Necrotize : EnergyDrain,
        >= 10 => () => gauge.HasAetherflowStacks ? Fester : EnergyDrain,
        _     => null
    };

    private IconReplacer.ReplaceIcon? MakeEnergySiphon() => Player.Level switch
    {
        >= 40 => () => gauge.HasAetherflowStacks ? Painflare : EnergySiphon,
        _     => null
    };
}
