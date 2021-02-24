using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out UnityUnit unit))
                {
                    Debug.Log(unit.Unit);
                    UIProvider.Get(0).UnitUI.SetUnit(unit);
                }
            }
        }
    }
}
