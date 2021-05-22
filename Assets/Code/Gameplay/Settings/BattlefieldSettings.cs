using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class BattlefieldSettings
    {
        public BattlefieldPlayerSettings[] BattlefieldPlayerSettings;

        public Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> Graph;

        public BattlefieldSettings(BattlefieldPlayerSettings[] battlefieldPlayerSettings, Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> graph)
        {
            BattlefieldPlayerSettings = battlefieldPlayerSettings;
            Graph = graph;
        }
    }
}
