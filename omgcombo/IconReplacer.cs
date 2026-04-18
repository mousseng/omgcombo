using Dalamud.Hooking;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using omgcombo.Jobs;
using omgcombo.Services;

namespace omgcombo;

public sealed class IconReplacer : IDisposable
{
    public delegate uint ReplaceIcon();
    private delegate uint OnGetIcon(byte self, uint action);
    private delegate uint OnCanReplace(uint action);

    private readonly Hook<OnGetIcon> getIcon;
    private readonly Hook<OnCanReplace> canReplace;
    private readonly IconMap iconMap = new();

    /// <summary>
    /// Set up our skill replacers and function hooks.
    /// </summary>
    public IconReplacer(IGameInteropProvider interop)
    {
        // hook the relevant functions
        getIcon = interop.HookFromAddress<OnGetIcon>(
            ActionManager.Addresses.GetAdjustedActionId.Value,
            GetIcon);

        canReplace = interop.HookFromSignature<OnCanReplace>(
            "40 53 48 83 EC 20 8B D9 48 8B 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 48 85 C0 74 1F",
            CanReplace);

        // turn on the hooks
        getIcon.Enable();
        canReplace.Enable();
    }

    public void Build(Configuration config)
    {
        new Ast().Load(config, iconMap);
        new Blm().Load(config, iconMap);
        new Dnc().Load(config, iconMap);
        new Gnb().Load(config, iconMap);
        new Rdm().Load(config, iconMap);
        new Sam().Load(config, iconMap);
        new Smn().Load(config, iconMap);
    }

    /// <summary>
    /// This function normally determines if a skill can have its icon replaced;
    /// while we could just return "true" always, maybe there's some value in
    /// skipping the GetIcon call for non-swapping icons.
    /// </summary>
    private uint CanReplace(uint action)
    {
        return iconMap.ContainsKey(action) ? 1 : canReplace.Original(action);
    }

    /// <summary>
    /// Look up the actions we have configured for replacing and run their logic
    /// to figure out what icon we're switching to. If it's an action we haven't
    /// configured, or we don't have all the data we need yet, simply pass back
    /// to the original.
    /// </summary>
    private uint GetIcon(byte self, uint action)
    {
        if (!Combos.IsReady)
        {
            return getIcon.Original(self, action);
        }

        if (iconMap.TryGetValue(action, out var replacer))
        {
            return replacer();
        }

        return getIcon.Original(self, action);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        getIcon.Dispose();
        canReplace.Dispose();
    }
}
