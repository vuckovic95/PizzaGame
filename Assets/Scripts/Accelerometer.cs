using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{
    public Text display_currentAcc, displayForce;

    private float minValue = 0.1f;
    private Vector3 lastAc;
    private Vector3 currentAc;

    void Start()
    {
        lastAc = Input.acceleration;
    }
    
    void Update()
    {
        MeasureAcc();
    }

    public void MeasureAcc()
    {
        currentAc = Input.acceleration;

        if(currentAc != lastAc)
        {
            if (Mathf.Abs(Vector3.Distance(currentAc, lastAc)) < minValue)
            {
                display_currentAcc.text = "NoChange";
                displayForce.text = Mathf.Abs(Vector3.Distance(currentAc, lastAc)).ToString();
            }
            else
            {
                display_currentAcc.text = currentAc.ToString();
                displayForce.text = Mathf.Abs(Vector3.Distance(currentAc, lastAc)).ToString();
            }

        }
        else
        {
            display_currentAcc.text = "NoChange";
            displayForce.text = "0";
        }

        lastAc = currentAc;
    }
}
