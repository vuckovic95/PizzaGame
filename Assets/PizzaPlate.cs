using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPlate : MonoBehaviour
{
    public float speed;
    public List<Transform> ingrPositions;
    public List<float> angles;

    private Transform tr;

    private void Awake()
    {
        tr = transform;   
    }

    private void FixedUpdate()
    {
        tr.Rotate(new Vector3(0, speed, 0));
    }
    

    public void CalculateAngles()
    {
        float perfectAngle = 360f / ingrPositions.Count;
        int goodAngles = 0;
        angles.Clear();

        for(int i = 0; i < ingrPositions.Count; i++)
        {
            Vector3 dir1, dir2;

            if (i != ingrPositions.Count - 1)
            {
                dir1 = tr.position - ingrPositions[i].position;
                dir2 = tr.position - ingrPositions[i + 1].position;
            }
            else
            {
                dir1 = tr.position - ingrPositions[i].position;
                dir2 = tr.position - ingrPositions[0].position;
            }

            float angle = Vector3.Angle(dir1, dir2);
            angles.Add(angle);

        }

        for(int i = 0; i < angles.Count; i++)
        {
            angles[i] = angles[i] - perfectAngle;
            if(angles[i] > -10f && angles[i] < 10f)
            {
                goodAngles++;
            }
        }

        if(goodAngles == angles.Count)
        {
            print("PERFECT");
        }
        if(goodAngles == angles.Count - 1)
        {
            print("GOOD");
        }
        if(goodAngles == angles.Count - 2)
        {
            print("OKAY");
        }
        if(goodAngles <= angles.Count - 3)
        {
            print("FAIL");
        }


        ingrPositions.Clear();
    }
}
