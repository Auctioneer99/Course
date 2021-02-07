using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField]
    private GameObject _builder;

    void Start()
    {
        var position = new System.Numerics.Vector3(0, 0, 0);
        IEnumerable<Tile> field = FieldFactory.CustomField3();
        _builder.GetComponent<FieldBuilder>().Build(position, field.Select(t => t.Position));
    }
}
