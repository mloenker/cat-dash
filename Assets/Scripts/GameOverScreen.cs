using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel; 
    public bool isGameOver = false;
    public GameObject scoreText;

    void Start(){
        Time.timeScale = 1;
    }

    // Restart game
    public void Update(){
        if(Input.GetButtonDown("Jump")&&isGameOver){
            RestartGame();
        }
    }

    public void GameOver()
    {   
        isGameOver=true;
        // Activate the Game Over panel
        gameOverPanel.SetActive(true);
        scoreText.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
