using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -2f || Mathf.Abs(transform.position.x) > 4 || Mathf.Abs(transform.position.x) > 4)
            transform.position = new Vector3(1.4f, -0.24f, -0.31f);
    }
}
