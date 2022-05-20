using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class angels : MonoBehaviour
{
    public GameObject[] Angels;
    public Text[] pref;
    public GameObject Ball;
    public Text BallT;
    public arm Arm;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i<Angels.Length; i++)
        {
            pref[i].text = @$"U{i}:{(int)Arm.U[i]}";
            pref[i].transform.position = Camera.main.WorldToScreenPoint(Angels[i].transform.position);
        }
        BallT.text = $"({(int)(Ball.transform.position.x*100)},{(int)(Ball.transform.position.z*100)})";
        BallT.transform.position = Camera.main.WorldToScreenPoint(Ball.transform.position);
    }
}
