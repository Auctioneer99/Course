using AbilitySystem.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay.Abilities
{
    public sealed class EffectAbility : AAbility
    {
        public override EAbilityType AbilityType => EAbilityType.Effect;

        private Variable<Card> _hostVariable;
        private Variable<int> _timerVariable;

        public EffectAbility()
        {
            _hostVariable = new Variable<Card>("Host", null);
            _timerVariable = new Variable<int>("Timer", 0);
            _persistentVariables.Add(_hostVariable);
            _persistentVariables.Add(_timerVariable);
        }


        public void Initialize(Card host)
        {
            _hostVariable.Value = host;
        }

        protected override void LoadChild(string data)
        {
            throw new NotImplementedException();
        }

        protected override string SaveChild()
        {
            throw new NotImplementedException();
        }
    }
}
