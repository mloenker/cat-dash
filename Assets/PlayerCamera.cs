using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform target;

    private void LateUpdate(){
        Vector3 newPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
        transform.position = newPosition; 
    }

}

