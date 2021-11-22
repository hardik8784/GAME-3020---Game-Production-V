using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class Pacman : MonoBehaviour
{
    public AnimatedSprite DeathSequence;

    public SpriteRenderer SpriteRenderer { get; private set; }

    public new Collider2D Collider { get; private set; }
    public Movement Movement { get; private set; }
    
    public void Awake()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.Collider = GetComponent<Collider2D>();
        this.Movement = GetComponent<Movement>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.Movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.Movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.Movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.Movement.SetDirection(Vector2.right);
        }

        float Angle = Mathf.Atan2(this.Movement.Direction.y, this.Movement.Direction.x);
        this.transform.rotation = Quaternion.AngleAxis(Angle * Mathf.Rad2Deg, Vector3.forward);


    }

    public void ResetState()
    {
        this.enabled = true;
        this.SpriteRenderer.enabled = true;
        this.Collider.enabled = true;
        this.DeathSequence.enabled = false;
        this.DeathSequence.SpriteRenderer.enabled = false;
        this.Movement.ResetState();
        this.gameObject.SetActive(true);
    }

    public void FuncDeathSequence()
    {
        this.enabled = false;
        this.SpriteRenderer.enabled = false;
        this.Collider.enabled = false;
        this.Movement.enabled = false;
        this.DeathSequence.enabled = true;
        this.DeathSequence.SpriteRenderer.enabled = true;
        this.DeathSequence.Restart();
    }
}
