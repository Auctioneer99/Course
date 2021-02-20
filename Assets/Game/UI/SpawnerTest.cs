using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    [SerializeField]
    private GameObject _factoryObject;
    private UnityUnitFactory _factory;

    void Start()
    {
        _factory = _factoryObject.GetComponent<UnityUnitFactory>();

        Unit _unit = UnitFactory.Warrior(); 

        GameObject _uUnit = _factory.Spawn(_unit);
        _uUnit.transform.position = new Vector3(20, 20, 20);
    }
}
