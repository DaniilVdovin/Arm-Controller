using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class xypan : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    public GameObject Target;
    public Slider h;
    public arm Arm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 t = Camera.main.ScreenToViewportPoint(image.transform.localPosition) * 15;
        Target.transform.position = new Vector3(t.x, Target.transform.position.y, t.y);
        //fix vertical invert
        Target.transform.position = new Vector3(Target.transform.position.x,-h.value, Target.transform.position.z);


    }
    public InputField inputFieldX;
    public InputField inputFieldY;
    public InputField inputFieldZ;
    public InputField AngelS;
    public void target()
    {
        image.transform.localPosition = new Vector2(
            int.Parse(inputFieldX.text.Trim()),
            int.Parse(inputFieldZ.text.Trim()));
        h.value = int.Parse(inputFieldY.text.Trim()) / 100;
    }
    public void setOffset()
    {
        Arm.U[4] = int.Parse(AngelS.text.Trim());
    }

    public Grip griper;
    public void grip(float value)
    {
        Serial.WriteLn($"G:{value}");
        Debug.Log($"Send G:{value}");
        if (value > 0) griper.grip = true;
        else griper.grip = false;

    }
}
