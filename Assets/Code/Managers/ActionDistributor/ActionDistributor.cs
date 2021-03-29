using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public abstract class AActionDistributor : AManager
    {
        public AActionDistributor(GameController controller) : base(controller) { }

        public abstract void Add(ARequest request);

        public abstract void Add(ANotification notification);

        public abstract void Add(AAsk ask);
    }
}
