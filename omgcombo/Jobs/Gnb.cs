using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Gnb : IJob
{
    public void Load(Configuration config, IconMap map)
    {
        map.Set(SolidBarrel, MakeSolidBarrel(), config.Gnb.DoSolidBarrelCombo);
        map.Set(DemonSlaughter, MakeDemonSlaughter(), config.Gnb.DoDemonSlaughterCombo);
    }

    private const uint
        KeenEdge = 16137,
        BrutalShell = 16139,
        SolidBarrel = 16145,
        DemonSlice = 16141,
        DemonSlaughter = 16149;

    private static IconReplacer.ReplaceIcon? MakeSolidBarrel() => Player.Level switch
    {
        >= 26 => () => Utils.Do3Combo(KeenEdge, BrutalShell, SolidBarrel),
        >=  4 => () => Utils.Do2Combo(KeenEdge, BrutalShell),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeDemonSlaughter() => Player.Level switch
    {
        >= 40 => () => Utils.Do2Combo(DemonSlice, DemonSlaughter),
        _     => null,
    };
}
