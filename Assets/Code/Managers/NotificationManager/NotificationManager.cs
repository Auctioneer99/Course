using System.Collections.Generic;
using System.Linq;

namespace Gameplay
{
    public class NotificationManager : AManager
    {
        private List<ANotification> _notifications;

        public NotificationManager(GameController controller) : base(controller) 
        {
            _notifications = new List<ANotification>(2);
        }

        public void Add(ANotification notification, bool pushToFront = false)
        {
            if (pushToFront || notification is IPriorityAction)
            {
                _notifications.Insert(0, notification);
            }
            else
            {
                _notifications.Add(notification);
            }
        }

        public void Update()
        {
            ANotification item = _notifications.FirstOrDefault();
            if (item != null)
            {
                Process(item);
            }
        }

        public void Process(ANotification notification)
        {
            bool isValid = true;
            //is valid
            //on action apply
            notification.Apply();

            if (isValid == false)
            {

            }
        }
    }
}
