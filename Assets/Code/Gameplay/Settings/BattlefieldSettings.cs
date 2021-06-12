using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class BattlefieldSettings
    {
        public Dictionary<EPlayer, BattlefieldPlayerSettings> BattlefieldPlayerSettings;

        public Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> Graph;

        public List<TileDefinition> TileDefinitions { get; private set; }

        public BattlefieldSettings(Dictionary<EPlayer, BattlefieldPlayerSettings> battlefieldPlayerSettings, List<TileDefinition> definitions, Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> graph)
        {
            BattlefieldPlayerSettings = battlefieldPlayerSettings;
            TileDefinitions = definitions;
            Graph = graph;
        }
    }
}
