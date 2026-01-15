using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Sam : IJob
{
    private readonly SAMGauge _gauge = Gauges.Get<SAMGauge>();

    public void Load(Configuration config, IconMap map)
    {
        map.Set(Gekko, MakeGekko(), config.Sam.DoGekkoCombo);
        map.Set(Kasha, MakeKasha(), config.Sam.DoKashaCombo);
        map.Set(Yukikaze, MakeYukikaze(), config.Sam.DoYukikazeCombo);
        map.Set(Mangetsu, MakeMangetsu(), config.Sam.DoMangetsuCombo);
        map.Set(Oka, MakeOka(), config.Sam.DoOkaCombo);
        map.Set(Iaijutsu, MakeIaijutsu(), config.Sam.DoIaijutsu);
        map.Set(Ikishoten, MakeIkishoten(), config.Sam.DoIkishoten);
    }

    private const uint
        // statuses
        MeikyoShisui = 1233,
        OgiNamikiriReady = 2959,
        ZanshinReady = 3855,
        TsubameReady2 = 3852,
        TsubameReady3 = 4216,
        // actions
        Hakaze = 7477,
        Gyofu = 36963,
        Jinpu = 7478,
        Shifu = 7479,
        Yukikaze = 7480,
        Gekko = 7481,
        Kasha = 7482,
        Fuga = 7483,
        Fuko = 25780,
        Mangetsu = 7484,
        Oka = 7485,
        Iaijutsu = 7867,
        Higanbana = 7489,
        TenkaGoken = 7488,
        MidareSetsugekka = 7487,
        KaeshiGoken = 16485,
        KaeshiSetsugekka = 16486,
        TendoGoken = 36965,
        TendoSetsugekka = 36966,
        TendoKaeshiGoken = 36967,
        TendoKaeshiSetsugekka = 36968,
        Ikishoten = 16482,
        OgiNamikiri = 25781,
        KaeshiNamikiri = 25782,
        Zanshin = 36964;

    private static uint Do2Combo(uint step1, uint step2)
    {
        return Player.HasBuff(MeikyoShisui) ? step2 : Utils.Do2Combo(step1, step2);
    }

    private static uint Do3Combo(uint step1, uint step2, uint step3)
    {
        return Player.HasBuff(MeikyoShisui) ? step3 : Utils.Do3Combo(step1, step2, step3);
    }

    private static IconReplacer.ReplaceIcon? MakeGekko() => Player.Level switch
    {
        >= 92 => () => Do3Combo(Gyofu, Jinpu, Gekko),
        >= 50 => () => Do3Combo(Hakaze, Jinpu, Gekko),
        >= 30 => () => Utils.Do3Combo(Hakaze, Jinpu, Gekko),
        >=  4 => () => Utils.Do2Combo(Hakaze, Jinpu),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeKasha() => Player.Level switch
    {
        >= 92 => () => Do3Combo(Gyofu, Shifu, Kasha),
        >= 50 => () => Do3Combo(Hakaze, Shifu, Kasha),
        >= 40 => () => Utils.Do3Combo(Hakaze, Shifu, Kasha),
        >= 18 => () => Utils.Do2Combo(Hakaze, Shifu),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeYukikaze() => Player.Level switch
    {
        >= 92 => () => Do2Combo(Gyofu, Yukikaze),
        >= 50 => () => Do2Combo(Hakaze, Yukikaze),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeMangetsu() => Player.Level switch
    {
        >= 86 => () => Do2Combo(Fuko, Mangetsu),
        >= 50 => () => Do2Combo(Fuga, Mangetsu),
        >= 35 => () => Utils.Do2Combo(Fuga, Mangetsu),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeOka() => Player.Level switch
    {
        >= 86 => () => Do2Combo(Fuko, Oka),
        >= 50 => () => Do2Combo(Fuga, Oka),
        >= 45 => () => Utils.Do2Combo(Fuga, Oka),
        _     => null,
    };

    private IconReplacer.ReplaceIcon? MakeIkishoten() => Player.Level switch
    {
        >= 100 => DoZanshin,
        >=  90 => DoIkishoten,
        _      => null
    };

    private IconReplacer.ReplaceIcon? MakeIaijutsu() => Player.Level switch
    {
        >= 100 => () => DoTsubame(Higanbana, TendoGoken, TendoSetsugekka, TendoKaeshiGoken, TendoKaeshiSetsugekka),
        >=  76 => () => DoTsubame(Higanbana, TenkaGoken, MidareSetsugekka, KaeshiGoken, KaeshiSetsugekka),
        >=  30 => () => DoIaijutsu(Higanbana, TenkaGoken, MidareSetsugekka),
        _      => null
    };

    private uint DoZanshin()
    {
        if (_gauge.Kaeshi == Kaeshi.Namikiri)
        {
            return KaeshiNamikiri;
        }

        if (Player.HasBuff(OgiNamikiriReady))
        {
            return OgiNamikiri;
        }

        if (Player.HasBuff(ZanshinReady))
        {
            return Zanshin;
        }

        return Ikishoten;
    }

    private uint DoIkishoten()
    {
        if (_gauge.Kaeshi == Kaeshi.Namikiri)
        {
            return KaeshiNamikiri;
        }

        if (Player.HasBuff(OgiNamikiriReady))
        {
            return OgiNamikiri;
        }

        return Ikishoten;
    }

    private uint DoTsubame(uint h, uint g, uint s, uint kg, uint ks)
    {
        if (Player.HasBuff(TsubameReady3))
        {
            return ks;
        }

        if (Player.HasBuff(TsubameReady2))
        {
            return kg;
        }

        return DoIaijutsu(h, g, s);
    }

    private uint DoIaijutsu(uint h, uint g, uint s)
    {
        return (byte)_gauge.Sen switch
        {
            0b001 or 0b010 or 0b100 => h,
            0b011 or 0b101 or 0b110 => g,
            0b111                   => s,
            _ => Iaijutsu
        };
    }
}
