using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class BoxController : MonoBehaviour
{
    public Transform leftPivot1, rightPivot1, topPivot1, botPivot1, leftPivot2, rightPivot2, topPivot2, botPivot2;

    public Quaternion lp1, rp1, up1, dp1, lp2, rp2, up2, dp2;

    public Quaternion rot1, rot2, rot3, rot4, rot5;
    public bool make = false;
    public AnimationCurve curve;
    public float edgeTime;

    private Vector3 startPos;
    private Quaternion startRot;

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (make)
        {
            make = false;
            ResetBox();
        }
    }

    private void Init()
    {
        rot1 = new Quaternion();
        rot2 = new Quaternion();
        rot4 = new Quaternion();
        rot3 = new Quaternion();
        rot5 = new Quaternion();
        rot1.eulerAngles = new Vector3(-90f, 0f, 0f);
        rot3.eulerAngles = new Vector3(90f, 0f, 0f);
        rot2.eulerAngles = new Vector3(0f, 90f, 90f);
        rot4.eulerAngles = new Vector3(0f, -90f, 90f);
        rot5.eulerAngles = new Vector3(0, -180f, 90f);

        lp1 = leftPivot1.rotation;
        rp1 = rightPivot1.rotation;
        up1 = topPivot1.rotation;
        dp1 = botPivot1.rotation;
        lp2 = new Quaternion();
        lp2.eulerAngles = new Vector3(0f, 0f, 90f);
        rp2 = rightPivot2.rotation;
        up2 = topPivot2.rotation;
        dp2 = botPivot2.rotation;
    }

    public void MakeEdges()
    {
        StartCoroutine(LerpRotation(topPivot1, topPivot1.rotation, rot1, edgeTime));
        StartCoroutine(LerpRotation(rightPivot1, rightPivot1.rotation, rot2, edgeTime));
        StartCoroutine(LerpRotation(botPivot1, botPivot1.rotation, rot3, edgeTime));
        StartCoroutine(LerpRotation(topPivot2, topPivot2.rotation, rot1, edgeTime));
        StartCoroutine(LerpRotation(leftPivot2, leftPivot2.rotation, rot4, edgeTime));
        StartCoroutine(LerpRotation(botPivot2, botPivot2.rotation, rot3, edgeTime));
    }

    public void MakeBox()
    {
        MakeEdges();
        StartCoroutine(WaitForSeconds(edgeTime + 0.1f, () =>
        {
            StartCoroutine(LerpRotation(leftPivot1, leftPivot1.rotation, rot4, edgeTime, ()=>
            {
                StartCoroutine(LerpRotation(rightPivot2, rightPivot2.rotation, rot5, edgeTime));
            }));
        }));
        StartCoroutine(WaitForSeconds(3*edgeTime + 0.2f, () =>
        {
            StartCoroutine(LerpRotationZ(transform, transform.rotation, transform.rotation, 2f, ()=>
            {
                GlobalManager.GameManager.Win();
            }));
        }));
    }

    public void ResetBox()
    {
        transform.position = startPos;
        transform.rotation = startRot;

        leftPivot1.rotation = lp1;
        rightPivot1.rotation = rp1;
        topPivot1.rotation = up1;
        botPivot1.rotation = dp1;
        leftPivot2.rotation = lp2;
        rightPivot2.rotation = rp2;
        topPivot2.rotation = up2;
        botPivot2.rotation = dp2;
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

    public IEnumerator LerpRotationZ(Transform tr, Quaternion start, Quaternion end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            //Quaternion x = Quaternion.Lerp(start, end, t);
            float x = curve.Evaluate(t) * 360 * 6f;
            Quaternion newRot = new Quaternion();
            newRot.eulerAngles = new Vector3(0f, 0f, x);
            tr.rotation = newRot;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.rotation = end;

        action?.Invoke();
    }

    public IEnumerator WaitForSeconds(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

}
