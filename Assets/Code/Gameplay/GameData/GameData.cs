using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class GameData
    {
        public Dictionary<int, CardTemplate> CardTemplates { get; private set; }

        protected GameData()
        {

        }

        public static GameData Create()
        {
            GameData data = new GameData();

            CardTemplate template1 = new CardTemplate
            {
                Id = 0,
                Name = "UNKNOWN TEMPLATE",
                Availability = ECardAvailability.NotOwnable,
                ArtId = 0,
                Type = ECardType.Unit,
                Health = 10,
                Attack = 1,
                ActionPoints = 1,
                Initiative = 1,
            };

            CardTemplate template2 = new CardTemplate
            {
                Id = 1,
                Name = "Card 2",
                Availability = ECardAvailability.Base,
                ArtId = 0,
                Type = ECardType.Unit,
                Health = 8,
                Attack = 8,
                ActionPoints = 4,
                Initiative = 5,
            };

            CardTemplate template3 = new CardTemplate
            {
                Id = 2,
                Name = "Card 3",
                Availability = ECardAvailability.Base,
                ArtId = 0,
                Type = ECardType.Unit,
                Health = 2,
                Attack = 2,
                ActionPoints = 8,
                Initiative = 5,
            };

            CardTemplate template4 = new CardTemplate
            {
                Id = 3,
                Name = "Card 4",
                Availability = ECardAvailability.Base,
                ArtId = 0,
                Type = ECardType.Unit,
                Health = 32,
                Attack = 32,
                ActionPoints = 8,
                Initiative = 12,
            };

            data.CardTemplates = new Dictionary<int, CardTemplate>
            {
                { template1.Id, template1 },
                { template2.Id, template2 },
                { template3.Id, template3 },
                { template4.Id, template4 },
            };

            return data;
        }

        public GameRuntimeData CreateRuntime()
        {
            GameRuntimeData data = new GameRuntimeData();

            data.CardTemplates = CardTemplates;

            return data;
        }
    }
}
