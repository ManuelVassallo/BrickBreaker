﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public GameManager gm;
    public Transform powerup;
    AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
 
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            return;
        }
        if (inPlay == false)
        {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }

    }
    // if ball fallsdown the screen
    // reduce 1 life
    // restart the game
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bottom"))
        {
            // if Ball hit the bottom of the screen
            //player looses 1 life
            Debug.Log("Ball hit the bottom of the screen");
            rb.velocity = Vector2.zero;
            inPlay = false;
            //player looses 1 live
            gm.UpdateLives(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //is the item a brick?
        //if yes 
        //destroy it
        //Play explosion particles
        if (other.transform.CompareTag("brick"))
        {
            // if brick has more than 1 hit to break
            // replace sprite with creacke bricked sprite 
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript> ();
            if (brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            }
            else
            {
                // extra life power up spawner
                // random chance
                // if number is less that 50
                // spawn extra life power up
                int randChance = Random.Range(1, 101);
                if (randChance > 50)
                {
                    Instantiate(powerup, other.transform.position, other.transform.rotation);
                }

                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                //Destroy explosion particle from unity after 2.5f
                Destroy(newExplosion.gameObject, 2.5f);

                // check the worth of points the brick that the user hit and give points accordingly
                gm.UpdateScore(brickScript.points);
                gm.UpdateNumberOfBricks();
                Destroy(other.gameObject);
            }
            // play ball sound effect
            audio.Play ();

        }

            
    }

}
