using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Battlefield : IRuntimeDeserializable, IStateObject<Battlefield>
    {
        private BoardManager _manager;
        private GameController GameController => _manager.GameController;

        public Tile[] Tiles { get; private set; }
        private Dictionary<TileDefinition, Dictionary<TileDefinition, bool>> _adjencencyMatrix;

        public Battlefield(BoardManager manager)
        {
            _manager = manager;
        }

        public void Setup(BattlefieldSettings settings)
        {
            _adjencencyMatrix = settings.Graph;

            List<Tile> internalTiles = new List<Tile>();
            foreach (var ps in settings.BattlefieldPlayerSettings)
            {
                BoardSide side = _manager.GetBoardSide(ps.EPlayer);
                foreach( var definition in ps.Influence)
                {
                    Tile tile = new Tile(side, definition);
                    internalTiles.Add(tile);
                }
            }
            Tiles = internalTiles.ToArray();

            GameController.EventManager.OnBattleFieldSetuped.Invoke(this);
        }

        public void Copy(Battlefield other, GameController controller)
        {

        }

        public void FromPacket(GameController controller, Packet packet)
        {
            throw new NotImplementedException();
        }

        public void ToPacket(Packet packet)
        {
            throw new NotImplementedException();
        }
    }
}
