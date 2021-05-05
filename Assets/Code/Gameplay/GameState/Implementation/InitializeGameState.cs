using System;

namespace Gameplay
{
    public class InitializeGameState : AAuthorityGameState
    {
        public InitializeGameState(GameController controller) : base(controller, EGameState.Init) { }

        public override void OnEnterState(EGameState prevState)
        {
            base.OnEnterState(prevState);
            if (GameController.HasAuthority)
            {
                ActionDistributor distributor = GameController.ActionDistributor;
                ActionFactory factory = GameController.ActionFactory;
                /*
                SyncTimersSettingsAction settings = factory.Create<SyncTimersSettingsAction>()
                    .Initialize(GameController.GameInstance.Settings.TimerSettings);
                distributor.Add(settings);
                */

                int length = GameController.GameInstance.Settings.PlayersSettings.Count;
                for (byte i = 0; i < length; i++)
                {
                    EPlayer Eplayer = (EPlayer)(1 << i);
                    PlayerSettings pSets = GameController.GameInstance.Settings.PlayersSettings[Eplayer];

                    //SetupPlayerAction playerSetup = factory.Create<SetupPlayerAction>()
                    //    .Initialize(Eplayer, player.PlayerInfo);
                    //distributor.Add(playerSetup);

                    SetupPlayerDeckAction deck = factory.Create<SetupPlayerDeckAction>()
                        .Initialize(Eplayer, pSets.BattleDeck);
                    distributor.Add(deck);
                }

                //Define turns

                _isReady = true;
            }
        }

        public override void OnLeaveState(EGameState newStateId)
        {
            base.OnLeaveState(newStateId);
        }

        protected override void OnFinished()
        {

            SwitchState(EGameState.Mulligan);
        }
    }
}
