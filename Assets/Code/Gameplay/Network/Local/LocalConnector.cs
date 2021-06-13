using System;
using System.Text;
using UnityEngine;

namespace Gameplay
{
    public class LocalConnector : AConnector
    {
        public GameInstance Instance { get; private set; }
        //public GameController Controller { get; private set; }

        public LocalConnector(GameInstance instance)
        {
            Instance = instance;
        }

        public override void GetDecks()
        {

        }

        public override void Update()
        {
            Instance.Update();
        }

        public override void Send(AAction action)
        {
            //Debug.Log("[Local Connector] Sending " + action.EAction + (Instance.HasAuthority ? " Authority side" : " Client Side"));
            if (Instance.HasAuthority)
            {
                if (action is ITargetedAction tAction)
                {
                    switch (tAction.Target)
                    {
                        case NetworkTarget.TargetPlayer:
                            Manager.SendToTarget(this, action, tAction.Connection);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                else
                {
                    if (action is ICensored)
                    {
                        GameController gc = Instance.Controller;
                        foreach (var p in gc.PlayerManager.Players.Values)
                        {
                            CensorSend(action, p.EPlayer);
                        }
                        if (gc.PlayerManager.HasSpectators)
                        {
                            Manager.SendToGroup(this, action, EPlayer.Spectators);
                        }
                    }
                    else
                    {
                        Manager.SendToGroup(this, action, EPlayer.NonAuthority);
                    }
                }
            }
            else
            {
                Manager.SendToHost(this, action);
            }
        }

        private void CensorSend(AAction action, EPlayer targetPlayer)
        {
            AAction copyToCensor = Instance.Controller.ActionFactory.Create(action.EAction);
            copyToCensor.Copy(action, Instance.Controller);
            (copyToCensor as ICensored).Censor(targetPlayer);
            Manager.SendToGroup(this, action, targetPlayer);
        }

        public override void HandleMessage(AConnector sender, AAction action)
        {
            if (CanHandleMessage(sender, action) == false)
            {
                Debug.Log(Manager.ToString());
                Debug.Log($"[Network Manager] Current Side = { (Instance.HasAuthority ? "server" : "client") }");
                Debug.Log($"[Network Manager] Cant handle message: sender = {sender.ToString()}");
                return;
            }
            GameController gc = Instance.Controller;

            action = action.Clone(gc);

            if (action is IPriorityAction)
            {
                gc.ActionDistributor.HandleAction(action);
            }
            else
            {
                gc.ActionDistributor.Add(action);
            }
        }

        private bool CanHandleMessage(AConnector connector, AAction action)
        {
            APlayerAction playerAction = action as APlayerAction;
            bool isAuthorityAction = action is IAuthorityAction;
            if (Instance.HasAuthority)
            {
                if (playerAction != null)
                {
                    if (connector.Role != playerAction.EPlayer)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (isAuthorityAction)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[LocalConnector]");
            sb.Append(base.ToString());
            return sb.ToString();
        }
    }
}
