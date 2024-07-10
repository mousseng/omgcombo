using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Sam : IJob
{
    public void Load(Configuration config, IconMap map)
    {
        map.Set(Gekko, MakeGekko(), config.Sam.DoGekkoCombo);
        map.Set(Kasha, MakeKasha(), config.Sam.DoKashaCombo);
        map.Set(Yukikaze, MakeYukikaze(), config.Sam.DoYukikazeCombo);
        map.Set(Mangetsu, MakeMangetsu(), config.Sam.DoMangetsuCombo);
        map.Set(Oka, MakeOka(), config.Sam.DoOkaCombo);
    }

    private const uint
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
        Oka = 7485;

    // TODO: add ikishoten
    // TODO: ogi namikiri?

    private static IconReplacer.ReplaceIcon? MakeGekko() => Player.Level switch
    {
        >= 92 => () => Utils.Do3Combo(Gyofu, Jinpu, Gekko),
        >= 30 => () => Utils.Do3Combo(Hakaze, Jinpu, Gekko),
        >=  4 => () => Utils.Do2Combo(Hakaze, Jinpu),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeKasha() => Player.Level switch
    {
        >= 92 => () => Utils.Do3Combo(Gyofu, Shifu, Kasha),
        >= 40 => () => Utils.Do3Combo(Hakaze, Shifu, Kasha),
        >= 18 => () => Utils.Do2Combo(Hakaze, Shifu),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeYukikaze() => Player.Level switch
    {
        >= 92 => () => Utils.Do2Combo(Gyofu, Yukikaze),
        >= 50 => () => Utils.Do2Combo(Hakaze, Yukikaze),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeMangetsu() => Player.Level switch
    {
        >= 86 => () => Utils.Do2Combo(Fuko, Mangetsu),
        >= 35 => () => Utils.Do2Combo(Fuga, Mangetsu),
        _     => null,
    };

    private static IconReplacer.ReplaceIcon? MakeOka() => Player.Level switch
    {
        >= 86 => () => Utils.Do2Combo(Fuko, Oka),
        >= 45 => () => Utils.Do2Combo(Fuga, Oka),
        _     => null,
    };
}
