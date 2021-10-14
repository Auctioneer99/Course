namespace Gameplay
{
    public abstract class TriggerFlow : IFlowNode
    {
        public OutputFlowConnector OutputFlow => throw new System.NotImplementedException();

        public bool Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
