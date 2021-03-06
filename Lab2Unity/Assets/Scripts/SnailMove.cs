using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMove : MonoBehaviour
{
    Rigidbody2D body;

    SpriteRenderer sr;

    AudioSource sound;

    public Sprite right;
    public Sprite left;
    public Sprite up;
    public Sprite down;

    private float horizontal;

    private float vertical;

    private float moveLimiter = 0.7f;

    public float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameStarted())
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            if (horizontal > 0)
            {
                sr.sprite = right;
            }
            else if (horizontal < 0)
            {
                sr.sprite = left;
            }
            else if (vertical < 0)
            {
                sr.sprite = up;
            }
            else if (vertical > 0)
            {
                sr.sprite = down;
            }
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }
        if (horizontal != 0 || vertical != 0)
        {
            if (!sound.isPlaying)
            {
                sound.Play();
            }
        }
        else
        {
            sound.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) 
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

    }
}
