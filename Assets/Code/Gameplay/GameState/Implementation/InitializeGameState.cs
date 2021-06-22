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

                int length = GameController.GameInstance.Settings.PlayersSettings.Count;

                SetupBattlefieldAction action = factory.Create<SetupBattlefieldAction>()
                    .Initialize(GameController.GameInstance.Settings.BattlefieldSettings);
                distributor.Add(action);

                for (byte i = 0; i < length; i++)
                {
                    EPlayer Eplayer = (EPlayer)(1 << i);
                    PlayerSettings pSets = GameController.GameInstance.Settings.PlayersSettings[Eplayer];

                    SetupPlayerDeckAction deck = factory.Create<SetupPlayerDeckAction>()
                        .Initialize(Eplayer, pSets.BattleDeck);
                    distributor.Add(deck);
                }

                //Define turns


                //draw cards
                DrawInitialCards();



                _isReady = true;
            }
        }

        public override void OnLeaveState(EGameState newStateId)
        {
            base.OnLeaveState(newStateId);

            GameController.Initialize();

            if (GameController.HasAuthority)
            {
                var action = GameController.ActionFactory.Create<SpawnCardsAction>().Initialize(0);

                //action.Spawns.Add(new SpawnDefinition(GameController.CardManager, new CardDefinition(1), new Position(0, ELocation.Field, 1)));
                //action.Spawns.Add(new SpawnDefinition(GameController.CardManager, new CardDefinition(1), new Position(new TileDefinition(EPlayer.Neutral, 1, 1), ELocation.Field, 1)));
                action.Spawns.Add(new SpawnDefinition(GameController.CardManager, new CardDefinition(1), new Position(new TileDefinition(EPlayer.Neutral, -2, -2))));

                GameController.ActionDistributor.Add(action);

            }
        }

        private void DrawInitialCards()
        {

        }

        protected override void OnFinished()
        {
            SwitchState(EGameState.Mulligan);
        }
    }
}
