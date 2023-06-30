using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 1;
    public int direction = 1; // either 1 for moing right or -1 for moving left

    void Update()
    {

        //add movement parallax
        transform.position = new Vector3(transform.position.x+speed*0.2f*direction*Time.deltaTime, transform.position.y, transform.position.z);

        //reset when right threshold reached
        if(transform.position.x-(GetComponent<Renderer>().bounds.size.x/2)>12.0f && direction == 1){
            transform.position = new Vector3(transform.position.x-(GetComponent<Renderer>().bounds.size.x)*2, transform.position.y, transform.position.z);
        }

        //reset when left threshhold reached
        if(transform.position.x+(GetComponent<Renderer>().bounds.size.x/2)<-12.0f && direction == -1){
            transform.position = new Vector3(transform.position.x+(GetComponent<Renderer>().bounds.size.x)*2, transform.position.y, transform.position.z);
        }

    }
}
