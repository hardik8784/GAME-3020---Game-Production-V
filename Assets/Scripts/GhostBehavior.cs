using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]

public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost Ghost { get; private set; }

    public float Duration;
    private void Awake()
    {
        this.Ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.Duration);
    }

    public virtual void Enable(float Duration)
    {
        this.enabled = true;


        CancelInvoke();
        Invoke(nameof(Disable), Duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
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
