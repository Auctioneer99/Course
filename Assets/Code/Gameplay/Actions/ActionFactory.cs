using System;

namespace Gameplay
{
    public class ActionFactory
    {
        private readonly GameController _gameController;

        public ActionFactory(GameController controller)
        {
            _gameController = controller;
        }

        public AAction Create(EAction actionId)
        {
            AAction action = Create(actionId.GetActionType());
            return action;
        }

        public T Create<T>() where T: AAction
        {
            Type type = typeof(T);
            return Create(type) as T;
        }

        private AAction Create(Type type)
        {
            AAction action = Activator.CreateInstance(type) as AAction;
            action.GameController = _gameController;

            if (action is APlayerAction playerAction)
            {
                playerAction.ConnectionId = _gameController.Network.ConnectionId;
            }

            return action;
        }
    }
}
