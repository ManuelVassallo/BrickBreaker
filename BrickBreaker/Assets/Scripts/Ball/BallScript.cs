using System.Collections;
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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
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
            Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            //Destroy explosion particle from unity after 2.5f
            Destroy(newExplosion.gameObject, 2.5f);

            // check the worth of points the brick that the user hit and give points accordingly
            gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);

            Destroy(other.gameObject);
        }
    }

}
