using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Cube interacted");
    }

    public void SetPlayer(Player player)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
