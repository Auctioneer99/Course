﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public interface IVisibilityChangeRequester
    {
        void AddCardsToChangeVisibility(ChangeVisibilityAction action);
    }
}