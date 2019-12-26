using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        // user lives display
        livesText.text = "Lives: " + lives;
        //user score display
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives)
    {
        //increase or decrease the number of lives the user has
        lives += changeInLives;

        //check for no lives left and trigger the end of the game

        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int points)
    {
        //add score when user hits a brick
        score += points;

        scoreText.text = "Score: " + score;
    }
}
