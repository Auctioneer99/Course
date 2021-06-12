using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class VisibilityChanger : AManager
    {
        public VisibilityChanger(GameController controller): base(controller)
        {

        }

        public void ChangeVisibility(IVisibilityChangeRequester requester)
        {
            if (GameController.HasAuthority)
            {
                ChangeVisibilityAction visibilityAction = GameController.ActionFactory.Create<ChangeVisibilityAction>().Initialize();

            }
        }
    }
}
