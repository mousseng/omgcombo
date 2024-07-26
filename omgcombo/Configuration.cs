using Dalamud.Configuration;

namespace omgcombo;

public sealed class Configuration : IPluginConfiguration
{
    public int Version { get; set; }

    public AstConfiguration Ast { get; } = new();
    public GnbConfiguration Gnb { get; } = new();
    public SamConfiguration Sam { get; } = new();
    public SmnConfiguration Smn { get; } = new();
}

public sealed class AstConfiguration
{
    public bool PlacePlay1OnDraw;
    public bool PlacePlay2OnExaltation;
    public bool PlacePlay3OnIntersection;
}

public sealed class GnbConfiguration
{
    public bool DoSolidBarrelCombo;
    public bool DoDemonSlaughterCombo;
}

public sealed class SamConfiguration
{
    public bool DoGekkoCombo;
    public bool DoKashaCombo;
    public bool DoYukikazeCombo;
    public bool DoMangetsuCombo;
    public bool DoOkaCombo;
    public bool DoIaijutsu;
    public bool DoIkishoten;
}

public sealed class SmnConfiguration
{
    public bool EnergyDrainIntoFester;
    public bool EnergySiphonIntoPainflare;
}
