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

        protected override void ApplyImplementation()
        {
            Player player = GameController.PlayerManager.SetupPlayer(Player);
            Debug.Log("Seting up user on side = " + GameController.HasAuthority);
            Debug.Log(Player);
            Debug.Log(player);
            player.Info = Info;
            if (GameController.HasAuthority == false)
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
