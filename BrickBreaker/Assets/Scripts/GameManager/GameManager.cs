using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public int numberOfBricks;
    public GameObject loadLevelPanel;


    public Transform[] levels;
    public int currentLevelIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        // user lives display
        livesText.text = "Lives: " + lives;
        //user score display
        scoreText.text = "Score: " + score;

        //find out the number of bricks are in this level
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;

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
        if(lives <= 0)
        {
            lives = 0;
            GameOver();
        }

        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int points)
    {
        //add score when user hits a brick
        score += points;

        scoreText.text = "Score: " + score;
    }
    
    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if(numberOfBricks <= 0)
        {
            // if there are 0 bricks and no more levels
            // gameover
            if(currentLevelIndex >= levels.Length - 1)
            {
                GameOver();
            }
            // else if there are 0 bricks but another level
            // load level
            else
            {
                //turn on load level panel
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevelIndex + 2);
                gameOver = true;
                //instantiate the next level after 3 seconds
                Invoke("LoadLevel",3f);
            }
        }
    }

    void LoadLevel()
    {
        //Loads next level 
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        //find the number of bricks
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        //turn off load level panel
        loadLevelPanel.SetActive(false);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void PlayAgian()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

}
