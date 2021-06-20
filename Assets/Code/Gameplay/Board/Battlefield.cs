using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Battlefield : IRuntimeDeserializable, IRuntimeStateObject<Battlefield>
    {
        private BoardManager _manager;
        private GameController GameController => _manager.GameController;

        public Tile[] Tiles { get; private set; }

        public BattlefieldSettings Settings { get; private set; }


        public Battlefield(BoardManager manager)
        {
            _manager = manager;
        }

        public void Setup(BattlefieldSettings settings)
        {
            Settings = settings;

            List<Tile> internalTiles = new List<Tile>();

            foreach(var definition in settings.TileDefinitions)
            {
                BoardSide side = _manager.GetBoardSide(definition.Player);
                Tile tile = new Tile(side, definition);
                internalTiles.Add(tile);
            }

            Tiles = internalTiles.ToArray();

            GameController.EventManager.OnBattleFieldSetuped.Invoke(this);
        }

        public Tile GetTile(int id)
        {
            return Tiles.Where(t => t.Definition.Id == id).FirstOrDefault();
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
