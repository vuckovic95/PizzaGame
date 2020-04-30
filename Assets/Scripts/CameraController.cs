using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class CameraController : MonoBehaviour
{
    void Awake()
    {
        GlobalManager.CameraController = this;
    }
    
    void Update()
    {
        
    }
}
