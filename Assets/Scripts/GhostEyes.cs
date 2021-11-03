using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }

    public Movement Movement { get; private set; }

    public Sprite Up;

    public Sprite Down;

    public Sprite Left;

    public Sprite Right;

    public void Awake()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.Movement = GetComponentInParent<Movement>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.Movement.Direction == Vector2.up)
        {
            this.SpriteRenderer.sprite = this.Up;
        }
        else if (this.Movement.Direction == Vector2.down)
        {
            this.SpriteRenderer.sprite = this.Down;
        }
        else if (this.Movement.Direction == Vector2.left)
        {
            this.SpriteRenderer.sprite = this.Left;
        }
        else if (this.Movement.Direction == Vector2.right)
        {
            this.SpriteRenderer.sprite = this.Right;
        }
    }
}
