using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public Transform StartPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 Position = collision.transform.position;
        Position.x = this.StartPosition.position.x;
        Position.y = this.StartPosition.position.y;

        collision.transform.position = Position;
    }
}
