using System.Runtime.InteropServices;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace omgcombo.Services;

public static class Combos
{
    private static IntPtr comboTimer;
    private static IntPtr lastComboMove;

    public static bool IsReady => comboTimer != IntPtr.Zero && lastComboMove != IntPtr.Zero;
    public static float ComboTime => Marshal.PtrToStructure<float>(comboTimer);
    public static int LastMove => Marshal.ReadInt32(lastComboMove);

    public static unsafe void Init()
    {
        var actionManager = (byte*)ActionManager.Instance();
        comboTimer = (IntPtr)(actionManager + 0x60);
        lastComboMove = comboTimer + 0x4;
    }
}
