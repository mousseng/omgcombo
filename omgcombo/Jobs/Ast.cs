using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using omgcombo.Services;

namespace omgcombo.Jobs;

public sealed class Ast : IJob
{
    private readonly ASTGauge _gauge = Gauges.Get<ASTGauge>();

    public void Load(Configuration config, IconMap map)
    {
        if (Player.Level >= 30)
        {
            map.Set(AstralDraw, ReplaceDraw, config.Ast.PlacePlay1OnDraw);
            map.Set(Exaltation, ReplaceExaltation, config.Ast.PlacePlay2OnExaltation);
            map.Set(Intersection, ReplaceIntersection, config.Ast.PlacePlay3OnIntersection);
        }
    }

    private const uint
        AstralDraw = 37017,
        UmbralDraw = 37018,
        TheBalance = 37023,
        TheArrow = 37024,
        TheSpire = 37025,
        TheSpear = 37026,
        TheBole = 37027,
        TheEwer = 37028,
        Intersection = 16556,
        Exaltation = 25873;

    private uint ReplaceDraw() => MapCard(_gauge.DrawnCards[0], MapDraw(_gauge.ActiveDraw));
    private uint ReplaceExaltation() => MapCard(_gauge.DrawnCards[1], Exaltation);
    private uint ReplaceIntersection() => MapCard(_gauge.DrawnCards[2], Intersection);

    private static uint MapDraw(DrawType activeDraw) => activeDraw switch
    {
        DrawType.ASTRAL => AstralDraw,
        DrawType.UMBRAL => UmbralDraw,
        _ => throw new NotImplementedException()
    };

    private static uint MapCard(CardType card, uint defaultAction) => card switch
    {
        CardType.BALANCE => TheBalance,
        CardType.ARROW => TheArrow,
        CardType.SPIRE => TheSpire,
        CardType.SPEAR => TheSpear,
        CardType.BOLE => TheBole,
        CardType.EWER => TheEwer,
        _ => defaultAction,
    };
}
