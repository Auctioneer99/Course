﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _tilePrefab;
    [SerializeField]
    private GameObject _unitPrefab;
    [SerializeField]
    private float _distanceBetween = 0;
    [SerializeField]
    private float _perlinPower = 1;
    [SerializeField]
    private float _perlinDistance = 10;
    [SerializeField]
    private float _scale;

    private static Quaternion rotation = Quaternion.Euler(-Mathf.Atan(Mathf.Sqrt(2) / 2) * (180 / Mathf.PI), 0, 45);

    public void Build(Vector3 originPosition, IEnumerable<Tile> field)
    {
        GameObject playground = new GameObject("Playground");
        playground.transform.localScale = new Vector3(_scale, _scale, _scale);
        playground.transform.position = originPosition;
        StartCoroutine(BuildCoroutine(playground, field));
    }

    private IEnumerator BuildCoroutine(GameObject parent, IEnumerable<Tile> field)
    {
        Vector2 perlinPostion = new Vector2(Random.Range(0, 10000), Random.Range(0, 10000));

        foreach (var tile in field)
        {
            GameObject tileModel = Instantiate(_tilePrefab, parent.transform);
            tileModel.GetComponent<UnityTile>().Tile = tile;
            Vector3 rawPosition = new Vector3(tile.Position.X, tile.Position.Y, tile.Position.Z);
            Vector3 position = rotation * rawPosition;
            tileModel.transform.position = position * _distanceBetween;

            StartCoroutine(MoveCoroutine(tileModel, perlinPostion));
            yield return null;
        }
        yield break;
    }

    private IEnumerator MoveCoroutine(GameObject tile, Vector2 perlinPosition)
    {
        Vector3 origin = tile.transform.position;
        Vector3 forPerlin = origin / _perlinDistance;
        float y = Mathf.PerlinNoise(forPerlin.x + perlinPosition.x, forPerlin.z + perlinPosition.y) * _perlinPower + 0.01f;
        
        Vector3 target = new Vector3(origin.x, y, origin.z);
        for (float i = 0; i < 1; i +=Time.deltaTime)
        {
            tile.transform.position = Vector3.Lerp(origin, target, i);
            yield return null;
        }
        tile.transform.position = target;
        UnityTile uTile = tile.GetComponent<UnityTile>();
        if (uTile.Tile.Unit != null)
        {
                StartCoroutine(CreateUnit(uTile.Tile.Unit, tile));
        }
        yield break;
    }
    private IEnumerator CreateUnit(Unit unit, GameObject tile)
    {
        GameObject unitModel = Instantiate(_unitPrefab, tile.transform);
        unitModel.GetComponent<UnityUnit>().Unit = unit;
        yield break;
    }
}
