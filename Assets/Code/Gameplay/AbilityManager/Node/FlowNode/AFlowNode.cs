namespace Gameplay
{
    public interface IFlowNode
    {
        OutputFlowConnector OutputFlow { get; }

        bool Execute();
    }
}
