using Dalamud.Game.ClientState.JobGauge.Types;
using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Dnc : IJob
{
    private readonly DNCGauge _gauge = Gauges.Get<DNCGauge>();

    public void Load(Configuration config, IconMap map)
    {
        map.Set(Windmill, MakeWindmill(), true);
        map.Set(Bladeshower, MakeBladeshower(), true);
        map.Set(FanDance2, MakeFanDance(), true);
        map.Set(Flourish, MakeFlourish(), true);
        map.Set(Devilment, MakeDevilment(), true);
        map.Set(StandardStep, MakeLastDance(), true);
    }

    private const uint
        // statuses
        SSymm = 2693,
        SFlow = 2694,
        FSymm = 3017,
        FFlow = 3018,
        StandardDancing = 1818,
        Threefold = 1820,
        Fourfold = 2699,
        StarfallReady = 2700,
        LastDanceReady = 3867,
        FinishingMoveReady = 3868,
        // actions
        Windmill = 15993,
        Bladeshower = 15994,
        RisingWindmill = 15995,
        Bloodshower = 15996,
        StandardStep = 15997,
        StandardFinish = 16003,
        SingleStandardFinish = 16191,
        DoubleStandardFinish = 16192,
        Devilment = 16011,
        Flourish = 16013,
        FanDance1 = 16007,
        FanDance2 = 16008,
        FanDance3 = 16009,
        FanDance4 = 25791,
        Starfall = 25792,
        LastDance = 36983,
        FinishingMove = 36984;

    private static IconReplacer.ReplaceIcon? MakeWindmill() => Player.Level switch
    {
        >= 35 => () => Player.HasBuff(SSymm) || Player.HasBuff(FSymm) ? RisingWindmill : Windmill,
        _ => null
    };

    private static IconReplacer.ReplaceIcon? MakeBladeshower() => Player.Level switch
    {
        >= 45 => () => Player.HasBuff(SFlow) || Player.HasBuff(FFlow) ? Bloodshower : Bladeshower,
        _ => null
    };

    private static IconReplacer.ReplaceIcon? MakeFanDance() => Player.Level switch
    {
        >= 66 => () => Player.HasBuff(Threefold) ? FanDance3 : FanDance2,
        _ => null
    };

    private static IconReplacer.ReplaceIcon? MakeFlourish() => Player.Level switch
    {
        >= 86 => () => Player.HasBuff(Fourfold) ? FanDance4 : Flourish,
        _ => null
    };

    private static IconReplacer.ReplaceIcon? MakeDevilment() => Player.Level switch
    {
        >= 90 => () => Player.HasBuff(StarfallReady) ? Starfall : Devilment,
        _     => null
    };

    private IconReplacer.ReplaceIcon? MakeLastDance() => Player.Level switch
    {
        >= 96 => () => Player.HasBuff(LastDanceReady) ? LastDance : DoEnhancedStandardStep(),
        >= 92 => () => Player.HasBuff(LastDanceReady) ? LastDance : DoStandardStep(),
        _     => null
    };

    private uint DoEnhancedStandardStep()
    {
        if (!_gauge.IsDancing)
        {
            return Player.HasBuff(FinishingMoveReady) ? FinishingMove : StandardStep;
        }

        return _gauge.CompletedSteps switch
        {
            2 => DoubleStandardFinish,
            1 => SingleStandardFinish,
            _ => StandardFinish
        };
    }

    private uint DoStandardStep()
    {
        if (!(_gauge.IsDancing && Player.HasBuff(StandardDancing)))
        {
            return StandardStep;
        }

        return _gauge.CompletedSteps switch
        {
            2 => DoubleStandardFinish,
            1 => SingleStandardFinish,
            _ => StandardFinish,
        };
    }
}
