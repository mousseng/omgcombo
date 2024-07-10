namespace omgcombo;

public sealed class IconMap : Dictionary<uint, IconReplacer.ReplaceIcon>
{
    public void Set(uint actionId, IconReplacer.ReplaceIcon? replacer, bool shouldReplace)
    {
        if (shouldReplace && replacer is not null)
        {
            this[actionId] = replacer;
        }
        else
        {
            Remove(actionId);
        }
    }
}
