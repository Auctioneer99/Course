using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;

    public Camera Camera { get; set; }
    public Client Client { get; set; }
    //private List<GameObject> _cards = new List<GameObject>();

    public void AddCard(Card card)
    {
        GameObject cardObject = Instantiate(_cardPrefab, this.transform);
        CardUI ui = cardObject.GetComponent<CardUI>();
        ui.Camera = Camera;
        //_cards.Add(cardObject);
    }
}
