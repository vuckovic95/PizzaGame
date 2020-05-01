using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using System;

public class CameraController : MonoBehaviour
{

    private Transform parent;
    private Transform tr;
    private Quaternion rot;

    void Awake()
    {
        GlobalManager.CameraController = this;
        parent = transform.parent;
        rot = new Quaternion();
        rot.eulerAngles = new Vector3(45f, 0f, 0f);
    }
    
    public void WinRotate()
    {
        StartCoroutine(LerpRotation(parent, parent.rotation, rot, 1f));
    }

    public IEnumerator LerpRotation(Transform tr, Quaternion start, Quaternion end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Quaternion x = Quaternion.Lerp(start, end, t);
            tr.rotation = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.rotation = end;

        action?.Invoke();
    }
}
