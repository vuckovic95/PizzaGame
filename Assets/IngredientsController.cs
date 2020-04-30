using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class IngredientsController : MonoBehaviour
{
    public AnimationCurve curve;
    public float time;
    public Vector3 endScale;

    private Transform pizzaPlate;
    private Transform endPoint;
    private Coroutines coroutines;
    private Transform tr;

    void Awake()
    {
        Init();
    }
    

    public void Init()
    {
        coroutines = GetComponent<Coroutines>();
        tr = transform;
        pizzaPlate = GlobalManager.GameManager.pizzaPlate;
        endPoint = GlobalManager.GameManager.endPoint;
        if (endScale == Vector3.zero)
            endScale = tr.localScale;
    }

    public void Throw()
    {
        StartCoroutine(LerpRotation(tr, tr.rotation, endPoint.rotation, time));
        StartCoroutine(LerpPositionZ(tr, tr.position, endPoint.position, time));
        StartCoroutine(LerpScale(tr, tr.localScale, endScale, time));
        StartCoroutine(LerpPositionY(tr, tr.position, endPoint.position, time, () =>
        {
            tr.SetParent(pizzaPlate);
        }));

        GlobalManager.GameManager.FiredOne();

    }

    #region Coroutines

    public IEnumerator LerpPositionZ(Transform tr, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Vector3 x = Vector3.Lerp(start, end, t);
            tr.position = new Vector3(tr.position.x, tr.position.y, x.z);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.position = new Vector3(tr.position.x, tr.position.y, end.z);

        action?.Invoke();
    }

    public IEnumerator LerpPositionY(Transform tr, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            //Vector3 x = Vector3.Lerp(start, end, t);
            float x = curve.Evaluate(t);
            tr.position = new Vector3(tr.position.x, x, tr.position.z);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.position = new Vector3(tr.position.x, end.y, tr.position.z);

        action?.Invoke();
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

    public IEnumerator LerpScale(Transform tr, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Vector3 x = Vector3.Lerp(start, end, t);
            tr.localScale = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.localScale = end;

        action?.Invoke();
    }

    #endregion
}
