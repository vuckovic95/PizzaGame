﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public Transform leftPivot1, rightPivot1, topPivot1, botPivot1, leftPivot2, rightPivot2, topPivot2, botPivot2;

    public Quaternion rot1, rot2, rot3, rot4, rot5;
    public bool make = false;
    public AnimationCurve curve;

    private void Start()
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
        
    }

    private void Update()
    {
        if (make)
        {
            make = false;
            MakeBox();
        }
    }

    public void MakeEdges()
    {
        StartCoroutine(LerpRotation(topPivot1, topPivot1.rotation, rot1, .8f));
        StartCoroutine(LerpRotation(rightPivot1, rightPivot1.rotation, rot2, .8f));
        StartCoroutine(LerpRotation(botPivot1, botPivot1.rotation, rot3, .8f));
        StartCoroutine(LerpRotation(topPivot2, topPivot2.rotation, rot1, .8f));
        StartCoroutine(LerpRotation(leftPivot2, leftPivot2.rotation, rot4, .8f));
        StartCoroutine(LerpRotation(botPivot2, botPivot2.rotation, rot3, .8f));
    }

    public void MakeBox()
    {
        MakeEdges();
        StartCoroutine(WaitForSeconds(1f, () =>
        {
            StartCoroutine(LerpRotation(leftPivot1, leftPivot1.rotation, rot4, .8f, ()=>
            {
                StartCoroutine(LerpRotation(rightPivot2, rightPivot2.rotation, rot5, 1f));
            }));
        }));
        //StartCoroutine(WaitForSeconds(1.9f, () =>
        //{
        //}));
        StartCoroutine(WaitForSeconds(2.9f, () =>
        {
            StartCoroutine(LerpRotationZ(transform, transform.rotation, transform.rotation, 2f));
        }));
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