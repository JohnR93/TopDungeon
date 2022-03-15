using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable 
{
    public Sprite emptyChest;
    public int pesosAmount = 5;

    protected override void OnCollide(Collider2D coll)
    {
        if(!collected)
        {
            collected = true; //Set collected true
            GetComponent<SpriteRenderer>().sprite = emptyChest; 
            Debug.Log("Grant " + pesosAmount + " pesos!" );
        }
    }
}
