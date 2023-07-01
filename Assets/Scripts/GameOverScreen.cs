using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel; // Drag the Panel here in inspector
    public bool isGameOver = false;


    public void Update(){
        if(Input.GetButtonDown("Jump")&&isGameOver){
            RestartGame();
        }
    }

    // Call this function when the game is over
    public void GameOver()
    {   
        //Time.timeScale = 0;
        isGameOver=true;
        // Activate the Game Over panel
        gameOverPanel.SetActive(true);
    }

    // Call this function from the Restart button
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1;
    }
}
