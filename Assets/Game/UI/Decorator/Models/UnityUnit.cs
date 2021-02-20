using UnityEngine;
using UnityEngine.EventSystems;

public class UnityUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Unit Unit 
    { 
        get
        {
            return _unit;
        } 
        set
        {
            _unit = value;
        } 
    }
    private Unit _unit;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Unhovered");
    }
}
