namespace Gameplay.Unity
{
    public interface IGameListener
    {
        void Attach(GameController game, bool wasJustInitialized);

        void Detach(GameController game);

        void OnGameEndedCleanup();
    }
}
