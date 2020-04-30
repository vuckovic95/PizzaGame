using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPlate : MonoBehaviour
{
    public float speed;

    private Transform tr;

    private void Awake()
    {
        tr = transform;   
    }

    private void FixedUpdate()
    {
        tr.Rotate(new Vector3(0, speed, 0));
    }
}
