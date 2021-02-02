using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _tile;

    public void Build(Vector3 originPosition, IEnumerable<Tile> field)
    {
        float anglex = -Mathf.Atan(Mathf.Sqrt(2) / 2) * (180 / Mathf.PI);
        Quaternion rotation = Quaternion.Euler(anglex, 0, 45);

        foreach (var tile in field)
        {
            GameObject tileModel = Instantiate(_tile);
            Vector3 rawPosition = new Vector3(tile.Position.X, tile.Position.Y, tile.Position.Z);
            Vector3 position = rotation * rawPosition;
            position.y = 0;
            position += originPosition;
            tileModel.transform.position = position * 40;
        }
    }
}
