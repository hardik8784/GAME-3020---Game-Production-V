using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer Body;
    public SpriteRenderer Eyes;
    public SpriteRenderer Blue;
    public SpriteRenderer White;

    public bool Eaten{ get; private set;}

    public override void Enable(float Duration)
    {
        base.Enable(Duration);

        this.Body.enabled = false;
        this.Eyes.enabled = false;
        this.Blue.enabled = true;
        this.White.enabled = false;

        Invoke(nameof(Flash), Duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();

        this.Body.enabled = true;
        this.Eyes.enabled = true;
        this.Blue.enabled = false;
        this.White.enabled = false;
    }

    private void Flash()
    {
        if (!this.Eaten)
        {
            this.Blue.enabled = false;
            this.White.enabled = true;
            this.White.GetComponent<AnimatedSprite>().Restart();
        } 
    }

    private void OnEnable()
    {
        this.Ghost.Movement.SpeedMultiplier = 0.5f;
        this.Eaten = false;
    }

    private void OnDisable()
    {
        this.Ghost.Movement.SpeedMultiplier = 1.0f;
        this.Eaten = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Chase");
        Node Node = collision.GetComponent<Node>();

        if (Node != null && this.enabled)
        {
            Vector2 Direction = Vector2.zero;
            float MaxDistance = float.MinValue;

            foreach (Vector2 AvailableDirection in Node.AvailableDirections)
            {
                Vector3 NewPosition = this.transform.position + new Vector3(AvailableDirection.x, AvailableDirection.y, 0.0f);
                float Distance = (this.Ghost.Pacman.position - NewPosition).sqrMagnitude;

                if (Distance > MaxDistance)
                {
                    Direction = AvailableDirection;
                    MaxDistance = Distance;
                }
            }

            this.Ghost.Movement.SetDirection(Direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            //Debug.Log("Pacman And Ghost Collide");
            if (this.enabled)
            {
                Eaten_();
                //FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    private void Eaten_()
    {
        this.Eaten = true;
        Vector3 Position = this.Ghost.Home.InsideTransform.position;
        Position.z = this.Ghost.transform.position.z;
        this.Ghost.transform.position = Position;

        this.Ghost.Home.Enable(this.Duration);

        this.Body.enabled = false;
        this.Eyes.enabled = true;
        this.Blue.enabled = false;
        this.White.enabled = false;
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
