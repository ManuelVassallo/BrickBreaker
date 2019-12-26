using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move power up icon on y axis 
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);

        //if the power up game object is missed
        //destroy game obejct when its below the screen
        if(transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
