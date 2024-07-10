using omgcombo.Services;

namespace omgcombo.Jobs;

public static class Utils
{
    public static uint Do2Combo(uint step1, uint step2)
    {
        if (Combos.ComboTime <= 0)
        {
            return step1;
        }

        if (Combos.LastMove == step1)
        {
            return step2;
        }

        return step1;
    }

    public static uint Do3Combo(uint step1, uint step2, uint step3)
    {
        if (Combos.ComboTime <= 0)
        {
            return step1;
        }

        if (Combos.LastMove == step1)
        {
            return step2;
        }

        if (Combos.LastMove == step2)
        {
            return step3;
        }

        return step1;
    }
}
