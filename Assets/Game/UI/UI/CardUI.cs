using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Client Client { get; set; }

    public Camera Camera { get; set; }

    private Vector2 _initialPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        _initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = 
            Camera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent<UnityTile>(out UnityTile tile))
            {
                Debug.Log("hit");
                Destroy(gameObject);
                IServerCommand command = new PlayCardServerCommand(1, tile.Tile.Position);
                Client.Send(command);
            }
        }
        else
        {
            Debug.Log("no hit");
            transform.position = _initialPosition;
        }
    }
}
