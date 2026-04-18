using Dalamud.Configuration;

namespace omgcombo;

[Serializable]
public sealed class Configuration : IPluginConfiguration
{
    public int Version { get; set; }

    public AstConfiguration Ast { get; } = new();
    public BlmConfiguration Blm { get; } = new();
    public DncConfiguration Dnc { get; } = new();
    public DrgConfiguration Drg { get; } = new();
    public GnbConfiguration Gnb { get; } = new();
    public RdmConfiguration Rdm { get; } = new();
    public SamConfiguration Sam { get; } = new();
    public SmnConfiguration Smn { get; } = new();
}

[Serializable]
public sealed class AstConfiguration
{
    public bool PlacePlay1OnDraw;
    public bool PlacePlay2OnExaltation;
    public bool PlacePlay3OnIntersection;
}

[Serializable]
public sealed class BlmConfiguration
{
    public bool PlaceFreezeOnFlare;
    public bool PlaceFlareStarOnDespair;
}

[Serializable]
public sealed class DncConfiguration
{
    public bool PlaceRisingOnWindmill;
    public bool PlaceBloodOnBlade;
    public bool PlaceFd3OnFd2;
    public bool PlaceFd4OnFlourish;
    public bool PlaceLastDanceOnStandardStep;
}

[Serializable]
public sealed class DrgConfiguration
{
}

[Serializable]
public sealed class GnbConfiguration
{
    public bool DoSolidBarrelCombo;
    public bool DoDemonSlaughterCombo;
}

[Serializable]
public sealed class RdmConfiguration
{
    public bool ReplaceVerfire;
    public bool ReplaceVerstone;
    public bool DoEnchantedCombo;
}

[Serializable]
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

[Serializable]
public sealed class SmnConfiguration
{
    public bool EnergyDrainIntoFester;
    public bool EnergySiphonIntoPainflare;
}
