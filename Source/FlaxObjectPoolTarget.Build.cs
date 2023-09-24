using Flax.Build;

public class FlaxObjectPoolTarget : GameProjectTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for game
        Modules.Add("FlaxObjectPool");
    }
}
