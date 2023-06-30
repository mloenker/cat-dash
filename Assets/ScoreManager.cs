using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public TextMesh scoreText;
    
    private float highestPosition;

    private void Start()
    {
        highestPosition = player.position.y;
    }

    private void Update()
    {
        if (player.position.y > highestPosition)
        {
            highestPosition = player.position.y;
        }
        scoreText.text = "Score: " + highestPosition.ToString("0");
    }
}
