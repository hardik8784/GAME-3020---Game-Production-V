using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.Ghost.Chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node Node = collision.GetComponent<Node>();

        if(Node != null && this.enabled && !this.Ghost.Frightened.enabled)
        {
            int Index = Random.Range(0, Node.AvailableDirections.Count);

            if(Node.AvailableDirections[Index] == -this.Ghost.Movement.Direction && Node.AvailableDirections.Count > 1)
            {
                Index++;

                if(Index >= Node.AvailableDirections.Count)
                {
                    Index = 0;
                }
            }
            this.Ghost.Movement.SetDirection(Node.AvailableDirections[Index]);
        }
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
