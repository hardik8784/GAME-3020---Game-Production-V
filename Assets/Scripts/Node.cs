using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> AvailableDirections { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.AvailableDirections = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
