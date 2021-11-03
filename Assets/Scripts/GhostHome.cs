using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform InsideTransform;

    public Transform OutsideTransform;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransitition());

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.Ghost.Movement.SetDirection(-this.Ghost.Movement.Direction);
        }
    }


    private IEnumerator ExitTransitition()
    {
        this.Ghost.Movement.SetDirection(Vector2.up, true);
        this.Ghost.Movement.RigidBody.isKinematic = true;
        this.Ghost.Movement.enabled = false;

        //TODO: Need to implement the behaviour to get out
        Vector3 Position = this.transform.position;
        float Duration = 0.5f;
        float Elapsed = 0.0f;

        while(Elapsed < Duration)
        {
            Vector3 NewPosition = Vector3.Lerp(Position, this.InsideTransform.position, Elapsed / Duration);
            NewPosition.z = Position.z;
            this.Ghost.transform.position = NewPosition;
            Elapsed += Time.deltaTime;
            yield return null;
        }

        Elapsed = 0.0f;

        while (Elapsed < Duration)
        {
            Vector3 NewPosition = Vector3.Lerp(this.InsideTransform.position, this.OutsideTransform.position, Elapsed / Duration);
            NewPosition.z = Position.z;
            this.Ghost.transform.position = NewPosition;
            Elapsed += Time.deltaTime;
            yield return null;
        }

        this.Ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f :1.0f,0.0f), true);
        this.Ghost.Movement.RigidBody.isKinematic = false;
        this.Ghost.Movement.enabled = true;
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
