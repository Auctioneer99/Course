using System.Numerics;

public class UnityPlaygroundFactory : IPlaygroundFactory
{
    private IPlaygroundFactory original;
    private Vector3 _position;
    private FieldBuilder _builder;

    public UnityPlaygroundFactory(Vector3 position, FieldBuilder builder)
    {
        original = new SimplePlaygroundFactory();
        _position = position;
        _builder = builder;
    }

    public IPlayground NullPlayground()
    {
        IPlayground playground = original.NullPlayground();
        return new PlaygroundDecorator(playground, _position, _builder);
    }
}
