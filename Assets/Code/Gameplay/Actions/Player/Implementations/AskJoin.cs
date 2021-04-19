using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class AskJoinAction : APlayerAction, IUserAction
    {
        public override EAction EAction => EAction.AskJoinPlayer;

        public bool ValidWhenBlocked => true;

        public EPlayer Place { get; private set; }

        public AskJoinAction Initialize(EPlayer player, EPlayer place)
        {
            base.Initialize(player);
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

        public override void Copy(AAction copyFrom, GameController controller)
        {
            base.Copy(copyFrom, controller);
            Place = (copyFrom as AskJoinAction).Place;
        }

        protected override void ApplyImplementation()
        {
            if (GameController.HasAuthority)
            {
                Player player = GameController.PlayerManager.GetPlayer(Place);
                if (player == null)
                {
                    Debug.Log("[AskJoin] " + Place);
                    SetupPlayerAction setup = GameController.ActionFactory.Create<SetupPlayerAction>()
                        .Initialize(Place, new PlayerInfo("lolka", PlayerVanityData.Default()));
                    GameController.ActionDistributor.Add(setup);
                }
            }
        }

        protected override void AttributesFrom(Packet packet)
        {
            base.AttributesFrom(packet);
            Place = packet.ReadEPlayer();
        }

        protected override void AttributesTo(Packet packet)
        {
            base.AttributesTo(packet);
            packet.Write(Place);
        }
    }
}
