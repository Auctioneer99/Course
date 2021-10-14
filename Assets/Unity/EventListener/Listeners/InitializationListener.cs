using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Unity
{
    public class InitializationListener : MonoBehaviour, IGameListener
    {
        private void Start()
        {
            
        }

        public void Attach(GameController game, bool wasJustInitialized)
        {
            throw new NotImplementedException();
        }

        public void Detach(GameController game)
        {
            throw new NotImplementedException();
        }

        public void OnGameEndedCleanup()
        {
            throw new NotImplementedException();
        }
    }
}
