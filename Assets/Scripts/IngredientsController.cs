using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

#pragma warning disable 0108

public class IngredientsController : MonoBehaviour
{
    public GameManager.Ingredients type;
    public AnimationCurve curve;
    public float time;
    public Vector3 endScale;
    public Vector3 collider_scale;
     
    private Transform pizzaPlate;
    private Transform endPoint;
    private Coroutines coroutines;
    private Transform tr;
    private BoxCollider collider;
    private Vector3 startSize, startScale;
    private bool throwed = false;
    private Transform parent;

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

        collider = GetComponent<BoxCollider>();
        startSize = collider.size;
        startScale = tr.localScale;

        if (collider_scale == Vector3.zero)
            collider_scale = collider.size;

        parent = tr.parent;
    }

    public void Throw()
    {
        GetComponent<AudioSource>().Play();
        collider.size = collider_scale;
        StartCoroutine(LerpRotation(tr, tr.rotation, endPoint.rotation, time));
        StartCoroutine(LerpPositionZ(tr, tr.position, endPoint.position, time));
        StartCoroutine(LerpScale(tr, tr.localScale, endScale, time));
        StartCoroutine(LerpPositionY(tr, tr.position, endPoint.position, time, () =>
        {
            throwed = true;
            tr.SetParent(pizzaPlate);
            GlobalManager.GameManager.pizzaPlate.GetComponent<PizzaPlate>().ingrPositions.Add(transform);
            GlobalManager.GameManager.FiredOne();
        }));
    }

    public void ResetObject()
    {
        throwed = false;
        tr.parent = parent;
        tr.localScale = startScale;
        collider.size = startSize;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == this.gameObject.tag && throwed)
        {
            GlobalManager.GameManager.overlaps++;
        }
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
