using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{

    private void OnDisable()
    {
        this.Ghost.Scatter.Enable();
        Debug.Log("Started Chase");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Chase");
        Node Node = collision.GetComponent<Node>();

        if (Node != null && this.enabled) //&& !this.Ghost.Frightened.enabled)
        {
            Debug.Log("main Loop");
            Vector2 Direction = Vector2.zero;
            float MinDistance = float.MaxValue;

            foreach (Vector2 AvailableDirection in Node.AvailableDirections)
            {
                Vector3 NewPosition = this.transform.position + new Vector3(AvailableDirection.x, AvailableDirection.y, 0.0f);
                float Distance = (this.Ghost.Pacman.position - NewPosition).sqrMagnitude;

                if(Distance < MinDistance)
                {
                    Direction = AvailableDirection;
                    MinDistance = Distance;
                }
            }

            this.Ghost.Movement.SetDirection(Direction);
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
