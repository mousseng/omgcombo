using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Rdm : IJob
{
    public void Load(Configuration config, IconMap map)
    {
        map.Set(Verfire, MakeVerfire(), config.Rdm.ReplaceVerfire);
        map.Set(Verstone, MakeVerstone(), config.Rdm.ReplaceVerstone);
        map.Set(Riposte, MakeEnchantedCombo(), config.Rdm.DoEnchantedCombo);
    }

    private const uint
        // statuses
        VerfireReady = 1234,
        VerstoneReady = 1235,
        // actions
        Jolt = 7503,
        Jolt2 = 7524,
        Jolt3 = 37004,
        Verfire = 7510,
        Verstone = 7511,
        Riposte = 7504,
        Zwerchhau = 7512,
        EnchantedRiposte = 7527,
        EnchantedZwerchhau = 7528,
        EnchantedRedoublement = 7529;

    private static IconReplacer.ReplaceIcon? MakeVerfire() => Player.Level switch
    {
        >= 84 => () => Player.HasBuff(VerfireReady) ? Verfire : Jolt3,
        >= 62 => () => Player.HasBuff(VerfireReady) ? Verfire : Jolt2,
        >= 26 => () => Player.HasBuff(VerfireReady) ? Verfire : Jolt,
        >=  2 => () => Jolt,
        _     => null
    };

    private static IconReplacer.ReplaceIcon? MakeVerstone() => Player.Level switch
    {
        >= 84 => () => Player.HasBuff(VerstoneReady) ? Verstone : Jolt3,
        >= 62 => () => Player.HasBuff(VerstoneReady) ? Verstone : Jolt2,
        >= 30 => () => Player.HasBuff(VerstoneReady) ? Verstone : Jolt,
        >=  2 => () => Jolt,
        _     => null
    };

    private static IconReplacer.ReplaceIcon? MakeEnchantedCombo() => Player.Level switch
    {
        // for whatever unhinged reason, LastMove is set to the unenchanted
        // version of the weaponskills. we'll do a bespoke Combo impl here
        // until the need arises elsewhere
        >= 50 => Do3Combo,
        >= 35 => Do2Combo,
        _     => null
    };

    private static uint Do2Combo()
    {
        if (Combos.LastMove == Riposte)
        {
            return EnchantedZwerchhau;
        }

        return EnchantedRiposte;
    }

    private static uint Do3Combo()
    {
        if (Combos.LastMove == Riposte)
        {
            return EnchantedZwerchhau;
        }

        if (Combos.LastMove == Zwerchhau)
        {
            return EnchantedRedoublement;
        }

        return EnchantedRiposte;
    }
}
