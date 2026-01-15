using Dalamud.Game.ClientState.JobGauge.Types;
using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Blm : IJob
{
    private readonly BLMGauge _gauge = Gauges.Get<BLMGauge>();

    public void Load(Configuration config, IconMap map)
    {
        // map.Set(Fire4, ReplaceFire4, false);
        if (Player.Level >= 50)
        {
            map.Set(Flare, ReplaceFlare, config.Blm.PlaceFreezeOnFlare);
        }

        if (Player.Level >= 100)
        {
            map.Set(Despair, ReplaceDespair, config.Blm.PlaceFlareStarOnDespair);
        }
    }

    private const uint
        Freeze = 159,
        Flare = 162,
        Blizzard4 = 3576,
        Fire4 = 3577,
        Despair = 16505,
        FlareStar = 36989;

    // private uint ReplaceFire4()
    // {
    //     return _gauge.InUmbralIce ? Blizzard4 : Fire4;
    // }

    private uint ReplaceFlare()
    {
        return _gauge.InUmbralIce ? Freeze : Flare;
    }

    private uint ReplaceDespair()
    {
        return _gauge.AstralSoulStacks >= 6 ? FlareStar : Despair;
    }
}
