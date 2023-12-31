using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFish : MonoBehaviour
{

    public Sprite circleSprite;

    private SpriteRenderer circleSpriteRenderer;
    private Color circleColorActive = Color.cyan;
    private Color circleColorPassive = Color.white;
    private float t = 0;
    private bool pulse = false;
    private GameObject circleGameObject;

    // Create the circle
    void Start()
    {
        // Create a new GameObject as a child of this one
        circleGameObject = new GameObject("CircleSprite");
        circleGameObject.transform.parent = this.transform;

        // Add a SpriteRenderer to the new GameObject
        circleSpriteRenderer = circleGameObject.AddComponent<SpriteRenderer>();

        // Set the sprite
        circleSpriteRenderer.sprite = circleSprite;

        circleGameObject.transform.position = transform.position;
        circleGameObject.transform.localScale = new Vector3(2, 2, 2);

    }

    void Update()
    {
        // Circle pulsating animation
        t += Time.deltaTime;
        if(t>=1f){
            if(pulse){
                circleGameObject.transform.localScale = new Vector3(2, 2, 2);
            }else{
                circleGameObject.transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);
            }
            pulse = !pulse;
            t = 0f;
        }
    }

    // Change circle counter
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            circleSpriteRenderer.color = circleColorActive;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            circleSpriteRenderer.color = circleColorPassive;
        }
    }
}