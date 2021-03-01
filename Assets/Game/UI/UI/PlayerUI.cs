using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text _hand, _deck, _mana, _name, _team;

    public Player Player
    {
        get 
        {
            return _player;
        }
        set 
        {
            if (_player != null)
            {
                _player.OnManaChange -= UpdateMana;
                _player.OnDrawCard -= UpdateCards;
            }
            _player = value;
            _player.OnManaChange += UpdateMana;
            _player.OnDrawCard += UpdateCards;

            UpdateCards();
            _mana.text = _player.Mana.ToString();
            _name.text = _player.Name;
            _team.text = _player.Team.ToString();
        }
    }
    private Player _player;

    private void UpdateCards()
    {
        _hand.text = _player.Hand.Count.ToString();
        _deck.text = _player.Deck.Count.ToString();
    }

    private void UpdateMana(ChangeEvent e)
    {
        _mana.text = e.FinalValue.ToString();
    }
}
