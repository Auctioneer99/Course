using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class AuthTokenRequest : ARequest
    {
        public override EAction EAction => throw new NotImplementedException();

        public override ERequest ERequest => ERequest.AuthToken;

        public override void HandleAborted()
        {
            throw new NotImplementedException();
        }

        public override void HandleCancelled()
        {
            throw new NotImplementedException();
        }

        public override void HandleExpired()
        {
            throw new NotImplementedException();
        }

        public override void HandleFulfilled()
        {
            throw new NotImplementedException();
        }

        public override bool IsFulfilled()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyImplementation()
        {
            throw new NotImplementedException();
        }
    }
}
