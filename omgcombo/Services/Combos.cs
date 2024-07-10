using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace omgcombo.Services;

public static class Combos
{
    private static IntPtr ComboTimer;
    private static IntPtr LastComboMove;

    public static bool IsReady => ComboTimer != IntPtr.Zero && LastComboMove != IntPtr.Zero;
    public static float ComboTime => Marshal.PtrToStructure<float>(ComboTimer);
    public static int LastMove => Marshal.ReadInt32(LastComboMove);

    public static unsafe void Init()
    {
        var actionManager = (byte*)ActionManager.Instance();
        ComboTimer = (IntPtr)(actionManager + 0x60);
        LastComboMove = ComboTimer + 0x4;
    }
}
