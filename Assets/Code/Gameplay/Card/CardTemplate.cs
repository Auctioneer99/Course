using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    [Serializable]
    public class CardTemplate
    {
        public const int UNKNOWN_TEMPLATE_ID = 0;

        public int Id;

        public string Name;

        public ECardAvailability Availability;

        public int ArtId;

        public ECardType Type;

        public int Health;

        public int Attack;

        public int ActionPoints;

        public int Initiative;
    }
}
