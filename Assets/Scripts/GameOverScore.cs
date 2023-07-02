using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI currentText;
    
    private float highestPosition;


    private void Update()
    {
        currentText.text = "Your Score: " + scoreManager.getScore().ToString("0"); // Update text
    }
}
