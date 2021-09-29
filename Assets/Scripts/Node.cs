using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> AvailableDirections { get; private set; }

    public LayerMask ObstacleLayer;
    // Start is called before the first frame update
    private void Start()
    {
        this.AvailableDirections = new List<Vector2>();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.left);
    }

    private void CheckAvailableDirection(Vector2 Direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.50f, 0.0f, Direction, 1.0f, this.ObstacleLayer);

        if(hit.collider == null)
        {
            this.AvailableDirections.Add(Direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
