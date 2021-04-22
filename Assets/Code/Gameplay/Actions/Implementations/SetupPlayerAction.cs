using UnityEngine;

namespace Gameplay
{
    public class SetupPlayerAction : AAction, IAuthorityAction
    {
        public override EAction EAction => EAction.SetupPlayer;

        public EPlayer Player { get; private set; }
        public PlayerInfo Info { get; private set; }

        public SetupPlayerAction Initialize(EPlayer player, PlayerInfo info)
        {
            Initialize();
            Player = player;
            Info = info;
            return this;
        }

        public override void Copy(AAction copyFrom, GameController controller)
        {
            SetupPlayerAction other = copyFrom as SetupPlayerAction;
            base.Copy(copyFrom, controller);
            Player = other.Player;
            Info = other.Info.Clone(controller);
        }

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.SetupPlayer(Player);
            player.Info = Info;
            if (GameController.HasAuthority)
            {
                GameController.GameInstance.Settings.PlayersSettings[Player] = new PlayerSettings(Player, Info);
                //GameController.GameInstance.Settings.GetPlayerSettings(Player).PlayerInfo = Info;
            }
            GameController.EventManager.OnPlayerSetup.Invoke(player);
        }

        protected override void AttributesFrom(Packet packet)
        {
            Player = packet.ReadEPlayer();
            Info.FromPacket(packet);
        }

        protected override void AttributesTo(Packet packet)
        {
            packet.Write(Player)
                .Write(Info);
        }
    }
}
