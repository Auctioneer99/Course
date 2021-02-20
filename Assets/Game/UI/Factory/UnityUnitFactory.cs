using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityUnitFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    public GameObject Spawn(Unit unit)
    {
        GameObject obj = Instantiate(_prefab);
        obj.GetComponent<UnityUnit>().Unit = unit;

        return obj;
    }
}
