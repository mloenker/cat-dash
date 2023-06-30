using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private GameObject player;
    // This is the static cooldown variable shared between all boundaries
    public static float cooldown = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only teleport player if cooldown has finished
        if (other.tag == "Player" && cooldown <= 0)
        {
            // Update the cooldown
            cooldown = 3f; // set this to the duration of your cooldown

            // Switch the x position based on which boundary was hit
            float newX = transform.position.x < 0 ? 10.0f : -10.0f;
            other.gameObject.transform.position = new Vector3(newX, other.gameObject.transform.position.y, 0);
        }
    }

    void Update()
    {
        // Move the boundary with the player
        transform.position = new Vector3(transform.position.x, player.transform.position.y, 0);

        // Decrease the cooldown over time
        cooldown -= Time.deltaTime;
        if (cooldown < 0)
            cooldown = 0;
    }
}
