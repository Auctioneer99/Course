using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class BoardSide : IStateObject<BoardSide>, IRuntimeDeserializable
    {
        public BoardManager BoardManager { get; private set; }
        public EPlayer EPlayer { get; private set; }
        public TileDefinition[] Influence { get; private set; }

        public Location[] Locations { get; private set; }

        public Location Hand { get; private set; }
        public Location Deck { get; private set; }
        public Location Graveyard { get; private set; }
        public Location Mulligan { get; private set; }

        public BoardSide(BoardManager manager, EPlayer player)
        {
            BoardManager = manager;
            EPlayer = player;

            Hand = new Location(this, ELocation.Hand);
            Deck = new Location(this, ELocation.Deck);
            Graveyard = new Location(this, ELocation.Graveyard);
            Mulligan = new Location(this, ELocation.Mulligan);

            Locations = new[]
            {
                Hand, Deck, Graveyard, Mulligan
            };
        }

        public void Initialize(BattlefieldPlayerSettings settings)
        {
            EPlayer = settings.EPlayer;
            Influence = settings.Influence;

            if (settings.ShouldSpawnLeader)
            {
                //Spawn;
            }
        }

        public void Copy(BoardSide other, GameController controller)
        {
            for (int i = 0; i < Locations.Length; i++)
            {
                Locations[i].Copy(other.Locations[i], controller);
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            EPlayer = packet.ReadEPlayer();

            foreach(var loc in Locations)
            {
                loc.FromPacket(controller, packet);
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EPlayer);

            foreach(var loc in Locations)
            {
                packet.Write(loc);
            }
        }
    }
}
