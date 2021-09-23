using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }

    public Sprite[] Sprites;

    public float AnimationTime = 0.25f;
    public int AnimationFrame { get; private set; }

    public bool Loop = true;

    private void Awake()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance),this.AnimationTime, this.AnimationTime);
    }

    private void Advance()
    {

        if(!this.SpriteRenderer.enabled)
        {
            return;
        }

        this.AnimationFrame++;

        if(this.AnimationFrame >= this.Sprites.Length && this.Loop)
        {
            this.AnimationFrame = 0;
        }

        if(this.AnimationFrame >= 0 && this.AnimationFrame < this.Sprites.Length)
        {
            this.SpriteRenderer.sprite = this.Sprites[this.AnimationFrame];
        }
    }    


    public void Restart()
    {
        this.AnimationFrame = -1 ;

        Advance();
    }
}
