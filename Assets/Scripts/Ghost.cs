using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement Movement { get; private set; }

    public GhostChase Chase { get; private set; }

    public GhostBehavior InitialBehavior;
    
    public Transform Pacman;

    public int Points = 100;

    private void Awake()
    {
        this.Movement = GetComponent<Movement>();
        this.Chase = GetComponent<GhostChase>();
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

        this.Chase.Enable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Debug.Log("Pacman And Ghost Collide");
            FindObjectOfType<GameManager>().PacmanEaten();
        }
    }
}
