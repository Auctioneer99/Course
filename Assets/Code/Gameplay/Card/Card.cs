using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gameplay.Events;

namespace Gameplay
{
    public class Card : IRuntimeStateObjectCloneable<Card>, IRuntimeDeserializable, ICensored
    {
        public BattleEvent<ECardVisibility, ECardVisibility> VisibilityChanged { 
            get
            {
                if (_visibilityChanged == null)
                {
                    LazyBattleEvent(out _visibilityChanged, true);
                }
                return _visibilityChanged;
            }
        }
        public BattleEvent<Card, Position> Moved
        {
            get
            {
                if (_moved == null)
                {
                    LazyBattleEvent(out _moved, false);
                }
                return _moved;
            }
        }
        public BattleEvent<Card, int, int> HealthChanged
        {
            get
            {
                if (_healthChanged == null)
                {
                    LazyBattleEvent(out _healthChanged, true);
                }
                return _healthChanged;
            }
        }
        public BattleEvent<Card, int, int> ActionPointsChanged
        {
            get
            {
                if (_actionPointsChanged == null)
                {
                    LazyBattleEvent(out _actionPointsChanged, true);
                }
                return _actionPointsChanged;
            }
        }
        public BattleEvent<Card, int, int> AttackChanged
        {
            get
            {
                if (_attackChanged == null)
                {
                    LazyBattleEvent(out _attackChanged, true);
                }
                return _attackChanged;
            }
        }
        public BattleEvent<Card, int, int> InitiativeChanged
        {
            get
            {
                if (_initiativeChanged == null)
                {
                    LazyBattleEvent(out _initiativeChanged, true);
                }
                return _initiativeChanged;
            }
        }
        public BattleEvent<Card, CardDefinition> DefinitionChanged
        {
            get
            {
                if (_definitionChanged == null)
                {
                    LazyBattleEvent(out _definitionChanged, true);
                }
                return _definitionChanged;
            }
        }

        public BattleEvent BecomeVisualyInconsistentState
        {
            get
            {
                if (_becomeVisualyInconsistentState == null)
                {
                    _becomeVisualyInconsistentState = new BattleEvent(GameController);
                }
                return _becomeVisualyInconsistentState;
            }
        }

        private BattleEvent<ECardVisibility, ECardVisibility> _visibilityChanged;
        private BattleEvent<Card, Position> _moved;
        private BattleEvent<Card, int, int> _actionPointsChanged;
        private BattleEvent<Card, int, int> _healthChanged;
        private BattleEvent<Card, int, int> _attackChanged;
        private BattleEvent<Card, int, int> _initiativeChanged;
        private BattleEvent<Card, CardDefinition> _definitionChanged;
        private BattleEvent _becomeVisualyInconsistentState;

        public ushort Id { get; private set; }

        public EPlayer Owner { get; set; }

        public EPlayer PlayedBy { get; set; }

        public Position Position { get; set; }

        public CardDefinition Definition
        {
            get
            {
                return _definition;
            }
            private set
            {
                _definition = value;
            }
        }
        private CardDefinition _definition;

        public ECardVisibility EVisibility { get
            {
                return _eVisibility;
            }
            set
            {
                if (_eVisibility != value)
                {
                    ECardVisibility prev = _eVisibility;
                    _eVisibility = value;
                    VisibilityChanged.Invoke(prev, value);
                }
            }
        }
        private ECardVisibility _eVisibility;

        public CardTemplate Template { get; private set; }

        public CardData CardData { get; private set; }
        public int Attack { get; }
        public int Health => CardData.Health.Current;
        public int Initiative { get; }
        public int ActionPoints { get; }

        public bool IsAlive {  get { return Health > 0; } }

        public CardManager CardManager { get; private set; }
        public GameController GameController => CardManager?.GameController;

        private bool _shouldCreateVisuals => (EGameMode.Client | EGameMode.Spectator | EGameMode.SinglePlayer).Contains(GameController.GameInstance.Mode);

        public bool CanDie => Health <= 0 && 
            Template.Type == (ECardType.Unit | ECardType.Leader) &&
            Position.Location == ELocation.Field;

        public Card(CardManager manager)
        {
            CardManager = manager;

            CardData = new CardData(this);
        }

        public void Initialize(GameRuntimeData gameData, ushort id, CardDefinition definition, Position position)
        {
            Id = id;
            Position = position;

            SetDefinition(definition, gameData);
            CardData.Initialize();
        }
        /*/
        private ECardVisibility DetermineVisibility()
        {

        }*/

        public void Hide()
        {
            SetDefinition(CardDefinition.Unknown, GameController.GameRuntimeData);
        }

        public void SetDefinition(CardDefinition definition, GameRuntimeData gameData)
        {
            CardDefinition oldDefinition = Definition;
            Definition = definition;
            UpdateTemplate(gameData);
            EVisibility = VisibilityManager.GetVisibility(this);

            DefinitionChanged.Invoke(this, oldDefinition);
        }

        private void UpdateTemplate(GameRuntimeData gameData)
        {
            CardTemplate template = gameData.GetCardTemplate(Definition.TemplateId);
            if (template != null)
            {
                Template = template;
                
            }
        }

        public void Kill()
        {
            return;
        }

        private void LazyBattleEvent(out BattleEvent battleEvent, bool shouldChangeVisuals)
        {
            battleEvent = new BattleEvent(GameController);
            if (shouldChangeVisuals && _shouldCreateVisuals)
            {
                battleEvent.VisualEvent.AddListener(() => BecomeVisualyInconsistentState.Invoke());
            }
        }

        private void LazyBattleEvent<T>(out BattleEvent<T> battleEvent, bool shouldChangeVisuals)
        {
            battleEvent = new BattleEvent<T>(GameController);
            if (shouldChangeVisuals && _shouldCreateVisuals)
            {
                battleEvent.VisualEvent.AddListener((_) => BecomeVisualyInconsistentState.Invoke());
            }
        }

        private void LazyBattleEvent<T1, T2>(out BattleEvent<T1, T2> battleEvent, bool shouldChangeVisuals)
        {
            battleEvent = new BattleEvent<T1, T2>(GameController);
            if (shouldChangeVisuals && _shouldCreateVisuals)
            {
                battleEvent.VisualEvent.AddListener((_, __) => BecomeVisualyInconsistentState.Invoke());
            }
        }

        private void LazyBattleEvent<T1, T2, T3>(out BattleEvent<T1, T2, T3> battleEvent, bool shouldChangeVisuals)
        {
            battleEvent = new BattleEvent<T1, T2, T3>(GameController);
            if (shouldChangeVisuals && _shouldCreateVisuals)
            {
                battleEvent.VisualEvent.AddListener((_, __, ___) => BecomeVisualyInconsistentState.Invoke());
            }
        }

        public void Censor(EPlayer player)
        {
            if (EVisibility.IsVisibleTo(Position.Player, player) == false)
            {
                Definition = CardDefinition.Unknown;
            }
        }

        public Card Clone(GameController controller)
        {
            Card card = new Card(CardManager);
            card.Copy(this, controller);
            return card;
        }

        public void Copy(Card other, GameController controller)
        {
            Id = other.Id;
            PlayedBy = other.PlayedBy;
            Position = other.Position;
            SetDefinition(other.Definition, GameController.GameRuntimeData);
            CardData.Copy(other.CardData, controller);
        }

        public void FromPacket(GameController controller, Packet packet)
        {
            Id = packet.ReadUShort();
            PlayedBy = packet.ReadEPlayer();
            Position.FromPacket(packet);
            CardDefinition def = new CardDefinition();
            def.FromPacket(packet);
            SetDefinition(def, GameController.GameRuntimeData);
            if (def != CardDefinition.Unknown)
            {
                CardData.FromPacket(controller, packet);
            }
        }

        public void ToPacket(Packet packet)
        {
            packet.Write(Id)
                .Write(PlayedBy)
                .Write(Position);

            packet.Write(Definition);
            if (Definition != CardDefinition.Unknown)
            {
                packet.Write(CardData);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Card]");
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"Owner = {Owner}");
            sb.AppendLine($"Position = {Position.ToString()}");
            sb.AppendLine($"Definition = {Definition.ToString()}");
            sb.AppendLine($"Visibility = {EVisibility}");
            return sb.ToString();
        }
    }
}
