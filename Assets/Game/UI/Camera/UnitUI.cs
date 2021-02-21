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
        if (_unit != null)
        {
            _unit.Unit.Attack.Changed -= UpdataAttack;
            _unit.Unit.Moves.Current.Changed -= UpdataMoves;
            _unit.Unit.Health.Current.Changed -= UpdataHealth;
            _unit.Unit.Initiative.Changed -= UpdataInitiative;
        }
        _unit = unit;
        _attackText.text = _unit.Unit.Attack.Max.ToString();
        _healthText.text = _unit.Unit.Health.Current.Amount.ToString();
        _movesText.text = _unit.Unit.Moves.Current.Amount.ToString();
        _initiativeText.text = _unit.Unit.Initiative.Max.ToString();

        _unit.Unit.Attack.Changed += UpdataAttack;
        _unit.Unit.Moves.Current.Changed += UpdataMoves;
        _unit.Unit.Health.Current.Changed += UpdataHealth;
        _unit.Unit.Initiative.Changed += UpdataInitiative;
    }

    private void UpdataAttack(ChangeEvent e)
    {
        StartCoroutine(ChangeAtribute(_attackText, e.FinalValue));
    }

    private void UpdataHealth(ChangeEvent e)
    {
        StartCoroutine(ChangeAtribute(_healthText, e.FinalValue));
    }

    private void UpdataMoves(ChangeEvent e)
    {
        StartCoroutine(ChangeAtribute(_movesText, e.FinalValue));
    }

    private void UpdataInitiative(ChangeEvent e)
    {
        StartCoroutine(ChangeAtribute(_initiativeText, e.FinalValue));
    }

    private IEnumerator ChangeAtribute(Text text, int value)
    {
        int initial = int.Parse(text.text);
        for (float i = 0; i < 0.2; i += Time.deltaTime)
        {
            text.text = ((int)Mathf.Lerp(initial, value, i*5)).ToString();
            yield return null;
        }
        text.text = value.ToString();
        yield break;
    }
}
