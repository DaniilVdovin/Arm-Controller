using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm : MonoBehaviour
{
    public GameObject[] Legs;
    public float[]
        U = { 0, 0, 0, 0, 60 },
        L = { 0, 90, 105, 125, 175 },
        X = { 0, 0, 0, 0, 250 },
        Y = { 0, 90, 0, 0, 0 };
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {

        X[4] = Target.transform.localPosition.x*100; // абсцисса целевой точки
        Y[4] = Target.transform.localPosition.y*100; // высота целевой точки от 0 базы (если ниже, то минус)


        StartCoroutine(sender());
    }

    void FixedUpdate()
    {
        X[4] = Vector3.Distance(new Vector3(transform.position.x,0, transform.position.z),new Vector3(Target.transform.position.x,0, Target.transform.position.z)) * 100;
        Y[4] = Target.transform.localPosition.y * 100;

        // поправка на угол схвата
        X[3] = X[4] - L[4] * Mathf.Sin(((float)DegreeToRadian(U[4])));
        Y[3] = Y[4] + L[4] * Mathf.Cos(((float)DegreeToRadian(U[4])));

        float B = Mathf.Pow((Y[3]-Y[1]), 2f) + Mathf.Pow(X[3],2f);
        
        float q1 = (float)RadianToDegree(Mathf.Atan((Y[3] - Y[1]) / X[3]));
        float t = (Mathf.Pow(L[2], 2) - Mathf.Pow(L[3], 2) + B) / (2 * L[2] * Mathf.Sqrt(B));
        float q2 = (float)RadianToDegree(Mathf.Acos(t));
        U[1] = 90-(q2+q1);
        U[2] = 180-(float)RadianToDegree(Mathf.Acos((Mathf.Pow(L[2], 2) + Mathf.Pow(L[3], 2) - B) / (2 * L[2] * L[3])));
        float q3 =90-q2 - U[2];
        U[3] = (q2 - U[2]) + (q1) + U[4];
        //Debug.Log("t:"+ t + "q1:" + q1 + "q2:" + q2 + "q3:" + q3);

        Legs[0].transform.rotation = Quaternion.LookRotation(new Vector3(Target.transform.position.x, 0, Target.transform.position.z));
        U[0] = Legs[0].transform.rotation.eulerAngles.y;
        Legs[1].transform.localRotation = Quaternion.Euler((float)U[1], 0, 0);
        Legs[2].transform.localRotation = Quaternion.Euler((float)U[2], 0, 0);
        Legs[3].transform.localRotation = Quaternion.Euler((float)U[3], 0, 0);

    }
    IEnumerator sender()
    {
        while (true)
        {
            float offset = 90f;
            string temp = $"DATA:{ConwAngel(U[0])}{ConwAngel(U[1] + offset)}{ConwAngel(U[2] + offset)}{ConwAngel(-U[3] + offset)}000";
            Serial.WriteLn(temp);
            Debug.Log(temp);
            yield return new WaitForSeconds(2f);
            Serial.WriteLn("L");
        }
    }
    string ConwAngel(float angel)
    {
        int a = (int)Mathf.Clamp(angel,0,180);
        string s =
            a.ToString().Trim().Length == 3 ? a.ToString() :
            a.ToString().Trim().Length == 2 ? "0" + a.ToString().Trim() :
            a.ToString().Trim().Length == 1 ? "00" + a.ToString().Trim():"000";
        return s;
    }
    private double DegreeToRadian(double angle)
    {
        return (Mathf.PI * angle) / 180.0;
    }
    private double RadianToDegree(double angle)
    {
        return angle * (180.0 / Mathf.PI);
    }
    void OnSerialLine(string line)
    {
        Debug.Log("Got a line: " + line);
    }
}
