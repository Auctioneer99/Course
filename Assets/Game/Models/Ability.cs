public class Ability
{
    public Playground Playground => _playground;
    private Playground _playground;
    public string Name => "";

    public int Reload => _reload;
    private int _reload;

    virtual public void Use(Tile tile) { }

}
