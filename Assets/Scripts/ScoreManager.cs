using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public TextMeshPro scoreText;
    
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
        scoreText.text = "SCORE: " + highestPosition.ToString("0");
    }
}
