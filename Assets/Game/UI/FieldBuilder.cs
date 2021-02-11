﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _tile;
    [SerializeField]
    private float _distanceBetween = 0;

    public void Build(System.Numerics.Vector3 originPosition, IEnumerable<System.Numerics.Vector3> field)
    {
        StartCoroutine(BuildCoroutine(originPosition, field));
    }

    private IEnumerator BuildCoroutine(System.Numerics.Vector3 originPosition, IEnumerable<System.Numerics.Vector3> field)
    {
        float anglex = -Mathf.Atan(Mathf.Sqrt(2) / 2) * (180 / Mathf.PI);
        Quaternion rotation = Quaternion.Euler(anglex, 0, 45);
        Vector3 origin = new Vector3(originPosition.X, originPosition.Y, originPosition.Z);

        foreach (var tile in field)
        {
            GameObject tileModel = Instantiate(_tile);
            Vector3 rawPosition = new Vector3(tile.X, tile.Y, tile.Z);
            Vector3 position = rotation * rawPosition;
            position.y = -0.3f;
            tileModel.transform.position = position * _distanceBetween;
            tileModel.transform.position += origin;
            StartCoroutine(MoveCoroutine(tileModel));
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    private IEnumerator MoveCoroutine(GameObject tile)
    {
        Vector3 origin = tile.transform.position;
        Vector3 target = new Vector3(origin.x, 0.1f, origin.z);
        for (float i = 0; i < 1; i +=Time.deltaTime)
        {
            tile.transform.position = Vector3.Lerp(origin, target, i);
            yield return null;
        }
        tile.transform.position = target;
        yield break;
    }
}
