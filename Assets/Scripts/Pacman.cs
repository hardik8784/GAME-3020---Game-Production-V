using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]

public class Pacman : MonoBehaviour
{
    public Movement Movement { get; private set; }
    
    public void Awake()
    {
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
        this.Movement.ResetState();
        this.gameObject.SetActive(true);
    }
}
