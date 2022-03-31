using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;

    public float triggerLength = 1;
    public float chaselength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTrasform;
    private Vector3 startingPosition;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        //playerTrasform = GameObject.Find(player).transform;
        playerTrasform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Is that player in range?
        if(Vector3.Distance(playerTrasform.position, startingPosition) < chaselength)
        {
            chasing = Vector3.Distance(playerTrasform.position, startingPosition) < triggerLength;

            if (chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTrasform.position - transform.position).normalized);
                }
                else
                {
                    UpdateMotor(startingPosition - transform.position);
                }
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;    
        }

        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for(int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null)
                continue;
                
            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //The array is not cleaned up, so we do it ourself
            hits[i] = null;
        }


    }

}
