using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField]
    private GameObject _builder;

    void Start()
    {
        Vector3 position = new Vector3(0, 0, 0);
        IEnumerable<Tile> field = FieldFactory.CustomField3();
        _builder.GetComponent<FieldBuilder>().Build(position, field);
    }
}
