using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement Movement { get; private set; }

    public GhostHome Home { get; private set; }

    public GhostScatter Scatter { get; private set; }

    public GhostChase Chase { get; private set; }

    public GhostFrightened Frightened { get; private set; }

    public GhostBehavior InitialBehavior;
    
    public Transform Pacman;

    public int Points = 100;

    private void Awake()
    {
        this.Movement = GetComponent<Movement>();
        this.Home = GetComponent<GhostHome>();
        this.Scatter = GetComponent<GhostScatter>();
        this.Chase = GetComponent<GhostChase>();
        this.Frightened = GetComponent<GhostFrightened>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.Movement.ResetState();

        this.Frightened.Disable();
        this.Chase.Disable();
        this.Scatter.Enable();

        if(this.Home != this.InitialBehavior)
        {
            this.Home.Disable();
        }

        if( this.InitialBehavior != null)
        {
            this.InitialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Debug.Log("Pacman And Ghost Collide");
            if (this.Frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
