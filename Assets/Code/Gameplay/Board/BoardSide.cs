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
        public BattlefieldPlayerSettings Settings { get; private set; }

        public Dictionary<ELocation, Location> Locations { get; private set; }

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

            Locations = new Dictionary<ELocation, Location>()
            {
                { ELocation.Hand, Hand },
                { ELocation.Deck, Deck },
                { ELocation.Graveyard, Graveyard },
                { ELocation.Mulligan, Mulligan }
            };
        }

        public void Initialize(BattlefieldPlayerSettings settings)
        {
            Settings = settings;
        }

        public Location GetLocation(ELocation location)
        {
            Locations.TryGetValue(location, out Location result);
            return result;
        }

        public void Add(Card card, Position position)
        {
            Location location = GetLocation(position.Location);
            if (location != null)
            {
                location.TryAdd(card, position.Index);
            }
        }

        public void Remove(Card card)
        {
            Location location = GetLocation(card.Position.Location);
            if (location != null)
            {
                location.TryRemove(card);
            }
        }

        public void Copy(BoardSide other, GameController controller)
        {
            foreach(var l in Locations)
            {
                l.Value.Copy(other.GetLocation(l.Key), controller);
            }
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            EPlayer = packet.ReadEPlayer();

            foreach(var loc in Locations)
            {
                loc.Value.FromPacket(controller, packet);
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(EPlayer);

            foreach(var loc in Locations)
            {
                packet.Write(loc.Value);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[BoardSide]");
            sb.AppendLine($"Player = {EPlayer}");
            foreach(var loc in Locations)
            {
                sb.AppendLine($"{loc.Key} = {loc.Value.ToString()}");
            }
            return sb.ToString();
        }
    }
}
