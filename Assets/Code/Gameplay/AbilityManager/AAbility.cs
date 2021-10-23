using AbilitySystem.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Abilities
{
    public enum EAbilityId
    {
        Empty = 0,
    }

    public enum EAbilityType
    {
        Effect
    }

    public abstract class AAbility : IBlackboard, IPersistent<AAbility>
    {
        public EAbilityId AbilityId { get; set; }
        public abstract EAbilityType AbilityType { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Variable> PersistentVariables => _persistentVariables;
        public IEnumerable<Variable> TemporaryVariables => _temporaryVariables;

        protected List<Variable> _persistentVariables;
        private List<Variable> _temporaryVariables;
        private IEnumerable<Variable> _variables => _persistentVariables.Concat(_temporaryVariables);

        protected List<ANode> _nodes;

        protected List<APassiveTrigger> _passiveTriggers;

        public IEnumerable<IVarSource> Variables => _variables;

        public AAbility()
        {
            _persistentVariables = new List<Variable>();
            _nodes = new List<ANode>();
            _passiveTriggers = new List<APassiveTrigger>();
        }

        public AAbility Load(string data)
        {
            throw new NotImplementedException();
        }

        protected abstract void LoadChild(string data);

        public string Save()
        {
            throw new NotImplementedException();
        }

        protected abstract string SaveChild(); 

        public bool TryGet<T>(string name, out IVarSource<T> variable)
        {
            foreach(var v in _variables)
            {
                if (v.Name == name && v is Variable<T> casted)
                {
                    variable = casted;
                    return true;
                }
            }
            variable = null;
            return false;
        }

        public void AddVariable(Variable variable)
        {
            _temporaryVariables.Add(variable);
        }

        public void RemoveVariable(Variable variable)
        {
            _temporaryVariables.Remove(variable);
        }

        public void AddNode(ANode node)
        {
            _nodes.Add(node);
            if (node is APassiveTrigger trigger)
            {
                _passiveTriggers.Add(trigger);
            }
        }

        public void RemoveNode(ANode node)
        {
            _nodes.Remove(node);
            if (node is APassiveTrigger trigger)
            {
                _passiveTriggers.Remove(trigger);
            }
        }
    }

    public interface IAbilityIdDistributor
    {
        int GetVariableId();
        int GetNodeId();
    }

    public class AbilityIdDistributor : IAbilityIdDistributor
    {
        private int _variableCounter = 0;
        private int _nodeCounter = 0;

        public AbilityIdDistributor()
        {

        }

        public AbilityIdDistributor(AbilityData ablityData)
        {
            //setting max id;
        }

        public int GetNodeId()
        {
            return _nodeCounter++;
        }

        public int GetVariableId()
        {
            return _variableCounter++;
        }
    }

    public struct AbilityData
    {
        public EAbilityId AbilityId;
        public EAbilityType Type;
        public string Name;
        public string Description;
        public List<NodeData> Nodes;
        public List<VariableData> Variables;
        public List<Connection> VariableConnections;
        public List<Connection> FlowConnections;
    }

    public struct Connection
    {
        public int First;
        public int Second;
    }

    public struct NodeData
    {
        public int Id;
        public ENodeType NodeType;
        public List<FlowData> FlowsOut;
        public List<FlowData> FlowsIn;
        public List<VariableData> Inputs;
        public List<VariableData> Outputs;
    }

    public struct FlowData
    {
        public int Id;
    }

    public struct VariableData
    {
        public int Id;
    }

    public enum ENodeType
    {
        GetBoolean
    }

    public interface IBlackboard
    {
        IEnumerable<IVarSource> Variables { get; }

        bool TryGet<T>(string name, out IVarSource<T> variable);
    }

    public interface IPersistent<T>
    {
        string Save();

        T Load(string data);
    }
}
