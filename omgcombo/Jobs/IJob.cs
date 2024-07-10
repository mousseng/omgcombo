namespace omgcombo.Jobs;

/// <summary>
/// Provides the icon replacers for a given job.
/// </summary>
public interface IJob
{
    /// <summary>
    /// Places the correct icon replacer functions into the action map.
    /// </summary>
    public abstract void Load(Configuration config, IconMap map);
}
