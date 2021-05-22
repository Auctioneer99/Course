using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Gameplay;

namespace Gameplay.Unity
{
    public class TestTile : MonoBehaviour
    {
        public void Awake()
        {
            BattlefieldFactory.DefaultCreate(2);
            //TileDefinition td = new TileDefinition(-22, 10);
        }
    }
}
