using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    public float Speed = 8.0f;

    public float SpeedMultiplier = 1.0f;

    public Vector2 InitialDirection;

    public LayerMask ObstacleLayer;

    public Rigidbody2D RigidBody { get; private set; }

    public Vector2 Direction { get; private set; }

    public Vector2 NextDirection { get; private set; }

    public Vector3 StartingPosition { get; private set; }

    private void Awake()
    {
        this.RigidBody = GetComponent<Rigidbody2D>();
        this.StartingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.SpeedMultiplier = 1.0f;
        this.Direction = this.InitialDirection;
        this.NextDirection = Vector2.zero;
        this.transform.position = this.StartingPosition;
        this.RigidBody.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        if(this.NextDirection !=Vector2.zero)
        {
            SetDirection(this.NextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 Position = this.RigidBody.position;
        Vector2 Translation = this.Direction * this.Speed * this.SpeedMultiplier * Time.fixedDeltaTime;
        this.RigidBody.MovePosition(Position + Translation);
    }

    public void SetDirection(Vector2 Direction, bool Forced = false)
    {
        if(Forced || !Occupied(Direction))
        {
            this.Direction = Direction;
            this.NextDirection = Vector2.zero;
        }
        else
        {
            this.NextDirection = Direction;
        }
    }   
    
    public bool Occupied(Vector2 Direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, Direction, 1.5f, this.ObstacleLayer);
        return hit.collider != null;
    }    

}
