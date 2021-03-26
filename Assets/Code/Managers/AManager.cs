namespace Gameplay
{
    public abstract class AManager
    {
        public GameController GameController { get; private set; }

        protected AManager(GameController gameController)
        {
            GameController = gameController;
        }
    }
}
