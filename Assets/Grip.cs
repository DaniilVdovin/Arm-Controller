using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour
{
    public bool grip = false;
    public bool canGrip = false;
    public GameObject ball;
    void Start()
    {
        
    }

    void Update()
    {
        if(canGrip && grip && ball != null)
        {
            ball.transform.position = transform.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        canGrip = other.gameObject.tag == "ball";
        if (canGrip)
        {
            ball = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ball = null;
    }
}
