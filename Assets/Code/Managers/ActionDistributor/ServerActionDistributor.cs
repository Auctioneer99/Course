using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class ServerActionDistributor : AActionDistributor
    {
        public ServerActionDistributor(GameController controller) : base(controller) { }

        public override void Add(ARequest request)
        {
            //sending and appling
            throw new NotImplementedException();
        }

        public override void Add(ANotification notification)
        {
            //sending and appling
            throw new NotImplementedException();
        }

        public override void Add(AAsk ask)
        {

        }
    }
}
