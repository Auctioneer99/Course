using AbilitySystem.Variables;
using Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors.NodeGraphSkillEditor
{
    [CreateAssetMenu(menuName = "Assets/Create/AbilityContaineer")]
    public abstract class AbilityContaineer : ScriptableObject
    {
        public string Description;
        public List<ANode> Nodes;
        public List<Variable> TemporaryVariables;
        public IEnumerable<Variable> PresistentVariables => _persistentVariables;

        private List<Variable> _persistentVariables;
        public AbilityContaineer()
        {
            Nodes = new List<ANode>();
            TemporaryVariables = new List<Variable>();
        }

        public abstract void SetPersistentVariables(List<Variable> variables);
    }

    public sealed class EffectAbilityContaineer : AbilityContaineer
    {
        public override IEnumerable<Variable> PresistentVariables => throw new NotImplementedException();

        private List<Variable> _persistentVariables;

        public EffectAbilityContaineer()
        {
            _persistentVariables
        }
    }
}
