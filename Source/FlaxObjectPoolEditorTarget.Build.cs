using Flax.Build;

public class FlaxObjectPoolEditorTarget : GameProjectEditorTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for editor
        Modules.Add("FlaxObjectPool");
        Modules.Add("FlaxObjectPoolEditor");
    }
}
