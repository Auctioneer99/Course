using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    
    private GameObject _health, _attack, _moves, _initiative, _ability1, _ability2, _ability3, _healthBar, _image;
    
    [SerializeField]
    private Text _attackText, _healthText, _movesText, _initiativeText;

    private UnityUnit _unit;

    public void SetUnit(UnityUnit unit)
    {
        _unit = unit;
        _attackText.text = _unit.Unit.Attack.Current.ToString();
        _healthText.text = _unit.Unit.Health.Current.ToString();
        _movesText.text = _unit.Unit.Moves.Current.ToString();
        _initiativeText.text = _unit.Unit.Initiative.Current.ToString();
    }
}
