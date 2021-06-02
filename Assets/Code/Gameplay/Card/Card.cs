﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameplay
{
    public class Card : IStateObject<Card>, IRuntimeDeserializable
    {
        public BattleEvent<ECardVisibility, ECardVisibility> VisibilityChanged {
            get 
            {
                return new BattleEvent<ECardVisibility, ECardVisibility>(GameController);
            }
        }
        private BattleEvent<ECardVisibility, ECardVisibility> _visibilityChanged;

        public ushort Id { get; private set; }
        public Position Position { get; private set; }
        public CardDefinition Definition { get; private set; }

        public CardManager CardManager { get; private set; }
        public GameController GameController => CardManager?.GameController;

        public ECardVisibility EVisibility { get
            {
                return _eVisibility;
            }
            private set
            {
                if (_eVisibility != value)
                {
                    ECardVisibility prev = _eVisibility;
                    _eVisibility = value;

                }
            }
        }
        private ECardVisibility _eVisibility;


        public Card(CardManager manager)
        {
            CardManager = manager;
        }

        public void Initialize(ushort id, CardDefinition definition, Position position)
        {
            Id = id;
            Position = position;
            EVisibility = ECardVisibility.Noone;

            Definition = definition;
        }
        /*/
        private ECardVisibility DetermineVisibility()
        {

        }*/

        public void Copy(Card other, GameController controller)
        {

        }

        public void FromPacket(GameController controller, Packet packet)
        {

        }

        public void ToPacket(Packet packet)
        {

        }
    }
}