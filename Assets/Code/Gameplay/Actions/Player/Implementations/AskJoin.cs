using UnityEngine;

namespace Gameplay
{
    public sealed class AskJoinAction : APlayerAction, IUserAction
    {
        public override EAction EAction => EAction.AskJoinPlayer;

        public bool ValidWhenBlocked => true;

        public EPlayer Place { get; private set; }

        public AskJoinAction Initialize(int connection, EPlayer place)
        {
            base.Initialize(connection);
            Place = place;
            return this;
        }

        public override bool IsValid()
        {
            if (base.IsValid() == false)
            {
                return false;
            }

            return GameController.StateMachine.ECurrentState == EGameState.AwaitingPlayers;
        }

        protected override void PlayerCopyImplementation(APlayerAction copyFrom, GameController controller)
        {
            Place = (copyFrom as AskJoinAction).Place;
        }

        protected override void ApplyImplementation()
        {
            Debug.Log("Setting up player on side " + GameController.HasAuthority);
            if (GameController.HasAuthority)
            {
                Player player = GameController.PlayerManager.GetPlayer(Place);
                if (player == null)
                {
                    SetupPlayerAction setup = GameController.ActionFactory.Create<SetupPlayerAction>()
                        .Initialize(Place, ConnectionId, new PlayerInfo("lolka", PlayerVanityData.Default()));
                    GameController.ActionDistributor.Add(setup);
                }
            }
        }

        protected override void PlayerAttributesTo(Packet packet)
        {
            packet.Write(Place);
        }

        protected override void PlayerAttributesFrom(Packet packet)
        {
            Place = packet.ReadEPlayer();
        }
    }
}
