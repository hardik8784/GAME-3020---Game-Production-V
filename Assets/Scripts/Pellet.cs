using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{

    public int Points = 10;

    //[Header("Eat Sound")]
    //public AudioSource Eat_Sound;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
        //Eat_Sound = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
            //Eat_Sound.Play();
        }
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
