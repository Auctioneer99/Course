using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnerTest : MonoBehaviour
{
    [SerializeField]
    private GameObject _factoryObject;
    private UnityUnitFactory _factory;

    private Unit _unit1, _unit2;

    void Start()
    {
        _factory = _factoryObject.GetComponent<UnityUnitFactory>();

        _unit1 = UnitFactory.Warrior();
        _unit2 = UnitFactory.Warrior();

        GameObject _uUnit1, _uUnit2;

        _uUnit1 = _factory.Spawn(_unit1);
        _uUnit1.transform.position = new Vector3(20, 20, 20);


        _uUnit2 = _factory.Spawn(_unit2);
        _uUnit2.transform.position = new Vector3(100, 20, 20);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MeleeAttack ability = new MeleeAttack();
            AbilityData d = _unit1.Abilities.First(a => a.Ability == Ability.MeleeAttack);
            ability.Use(d, _unit1, _unit2);
        }
    }
}
