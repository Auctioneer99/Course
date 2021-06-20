using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class GameRuntimeData
    {
        public Dictionary<int, CardTemplate> CardTemplates { get; set; }


        public CardTemplate GetCardTemplate(int templateId)
        {
            CardTemplates.TryGetValue(templateId, out CardTemplate template);
            return template;
        }
    }
}
