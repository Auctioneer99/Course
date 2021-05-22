namespace Gameplay
{
    public enum NetworkTarget : byte
    {
        Default = 0,
        TargetPlayer = 1 << 0,
        AllOther = 1 << 1,
        Server = 1 << 2,
        Spectators = 1 << 3,

        AllPlayers = TargetPlayer | AllOther,
    }

    public static class NetworkTargetExtensions
    {
        public static bool Contains(this NetworkTarget origin, NetworkTarget other)
        {
            return (origin & other) != 0;
        }

        public static bool ContainsTarget(this NetworkTarget origin, NetworkTarget target)
        {
            return origin.Contains(target);
            //return (target == NetworkTarget.Default) ? (origin == NetworkTarget.Default) : ((origin & target) == target);
        }
    }
}
