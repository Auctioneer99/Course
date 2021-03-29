using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class ClientActionDistributor : AActionDistributor
    {

        public ClientActionDistributor(GameController controller) : base(controller) { }

        public override void Add(ARequest request)
        {
            //appling
            GameController.RequestManager.AddRequest(request);
        }

        public override void Add(ANotification notification)
        {
            GameController.NotificationManager.Add(notification);
            //appling

        }

        public override void Add(AAsk ask)
        {
            GameController.Network.Send(ask);
            //sending / appling?
        }
    }
}
