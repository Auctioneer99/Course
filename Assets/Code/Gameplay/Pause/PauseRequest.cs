namespace Gameplay
{
    public struct PauseRequest
    {
        public int Id;
        public EPauseType Type;
        public IPauseRequester Requester;
        public long TimeRemaining;

        public PauseRequest(int id, EPauseType type, IPauseRequester requester, long duration)
        {
            Id = id;
            Type = type;
            Requester = requester;
            TimeRemaining = duration;
        }
    }
}
