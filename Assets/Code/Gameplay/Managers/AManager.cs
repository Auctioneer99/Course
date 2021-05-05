namespace Gameplay
{
    public abstract class AManager
    {
        public GameController GameController { get; protected set; }

        protected AManager(GameController gameController)
        {
            GameController = gameController;
        }
    }
}
