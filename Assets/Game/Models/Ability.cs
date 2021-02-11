public class Ability
{
    public IPlayground Playground => _playground;
    private IPlayground _playground;
    public string Name => "";

    public int Reload => _reload;
    private int _reload;

    virtual public void Use(Tile tile) { }

}
