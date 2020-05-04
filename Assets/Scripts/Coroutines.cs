using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using System;
using UnityEngine.UI;

public class Coroutines : MonoBehaviour
{
    void Awake()
    {
        GlobalManager.Coroutines = this;
    }

    //Do action after some time 
    public IEnumerator WaitForSeconds(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    //Lerp float 
    public IEnumerator LerpFloat(float result, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float x = Mathf.Lerp(start, end, t);
            result = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        result = end;

        action?.Invoke();
    }

    public IEnumerator LerpPositionWithCustomAxis(Transform tr, Vector3 start, Vector3 end, float time, int custom = 0, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            Vector3 pos = Vector3.Lerp(start, end, t);

            if (custom == 0)
            {
                tr.position = pos;
            }
            else if(custom == 1)
            {
                tr.position = new Vector3(pos.x, tr.position.y, tr.position.z);
            }
            else if(custom == 2)
            {
                tr.position = new Vector3(tr.position.x, pos.y, tr.position.z);
            }
            else if(custom == 3)
            {
                tr.position = new Vector3(tr.position.x, tr.position.y, pos.z);
            }
            else
            {
                Debug.LogError("Wrong parameter entered, 1 - x, 2 - y, 3 - z");
                break;
            }
            
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        if (custom == 0)
        {
            tr.position = end;
        }
        else if (custom == 1)
        {
            tr.position = new Vector3(end.x, tr.position.y, tr.position.z);
        }
        else if (custom == 2)
        {
            tr.position = new Vector3(tr.position.x, end.y, tr.position.z);
        }
        else if (custom == 3)
        {
            tr.position = new Vector3(tr.position.x, tr.position.y, end.z);
        }

        action?.Invoke();
    }

    public IEnumerator LerpFieldOfView(Camera c, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float x = Mathf.Lerp(start, end, t);
            c.fieldOfView = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        c.fieldOfView = end;

        action?.Invoke();
    }

    public IEnumerator LerpCamColor(Camera c, Color start, Color end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Color x = Color.Lerp(start, end, t);
            c.backgroundColor = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        c.backgroundColor = end;

        action?.Invoke();
    }

    //Lerp Int
    public IEnumerator LerpInt(int result, int start, int end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float x = Mathf.Lerp(start, end, t);
            result = (int)x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        result = end;

        action?.Invoke();
    }

    //LerpPositionV3
    public IEnumerator LerpV3Position(Transform tr, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Vector3 x = Vector3.Lerp(start, end, t);
            tr.position = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.position = end;

        action?.Invoke();
    }

    //Lerp scale
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

    //LerpPositionV2
    public IEnumerator LerpV2Position(Vector2 result, Vector2 start, Vector2 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Vector2 x = Vector2.Lerp(start, end, t);
            result = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        result = end;

        action?.Invoke();
    }

    //LerpRotationQuaternion
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

    //LerpLocalRot
    public IEnumerator LerpLocalRotation(Transform tr, Quaternion start, Quaternion end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Quaternion x = Quaternion.Lerp(start, end, t);
            tr.rotation = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.localRotation = end;

        action?.Invoke();
    }

    //LerpOnlyYRot
    public IEnumerator OnlyYRot(Transform tr, Quaternion start, Quaternion end, float time, Action action = null)
    {
        float t = 0f;
        Quaternion rot = new Quaternion();

        while (t < 1f)
        {

            float angle = Mathf.Lerp(start.eulerAngles.y, end.eulerAngles.y, t);
            rot.eulerAngles = new Vector3(tr.rotation.eulerAngles.x, angle, tr.rotation.eulerAngles.z);
            tr.localRotation = rot;
            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.localRotation = end;

        action?.Invoke();
    }

    //Lerp Slider Value (In-game)
    public IEnumerator LerpSlider(Slider s, float start, float end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            float x = Mathf.Lerp(start, end, t);
            s.value = x;

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        s.value = end;


        action?.Invoke();
    }
}
