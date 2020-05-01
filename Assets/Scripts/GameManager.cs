using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using NaughtyAttributes;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Win,
        Lose,
        Play,
        Menu
    }

    public enum Ingredients
    {
        Salama,
        Sir,
        Kobasica
    }

    [BoxGroup("GameState")] public GameState state;

    [BoxGroup("Instances That Objects Use")] public Transform pizzaPlate; 
    [BoxGroup("Instances That Objects Use")] public Transform endPoint;
    [BoxGroup("Instances That Objects Use")] public Transform startPoint;
    [BoxGroup("Instances That Objects Use")] public Transform box;

    [BoxGroup("Level Info")] public List<IngredientType> recipe;
    [BoxGroup("Level Info")] public int currentIngr = 0;
    [BoxGroup("Level Info")] public int ingrFired = 0;
    [BoxGroup("Level Info")] public int overlaps = 0;

    private Coroutines coroutines;

    private void Awake()
    {
        Init();
    }

    void Start()
    {
        state = GameState.Menu;
        //GlobalManager.LevelManager.SetLevel(GlobalManager.levelNum);

        SpawnIngridient();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                IngredientsController ic = hit.transform.GetComponent<IngredientsController>();
                if(ic != null)
                {
                    ic.Throw();
                }
            }
        }
    }

    private void Init()
    {
        GlobalManager.GameManager = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        coroutines = GetComponent<Coroutines>();
    }

    public void SpawnIngridient()
    {
        for (int i = 0; i < recipe[currentIngr].amount; i++)
        {
            Transform instance = GlobalManager.PoolManager.getIngredient(recipe[currentIngr].ingredientName);
            instance.position = startPoint.position;
            instance.rotation = startPoint.rotation;
        }
    }

    public void FiredOne()
    {
        ingrFired++;
        if (ingrFired == recipe[currentIngr].amount)
        {
            StartCoroutine(coroutines.WaitForSeconds(0.5f, () =>
            {
                ingrFired = 0;
                currentIngr++;
                endPoint.position = endPoint.position - new Vector3(0f, 0f, 0.1f);
                if (currentIngr < recipe.Count)
                {
                    PizzaCheck();
                    SpawnIngridient();
                }
                else
                {
                    PizzaCheck();
                    StartCoroutine(coroutines.LerpFieldOfView(Camera.main, Camera.main.fieldOfView, 100, 0.7f));
                    StartCoroutine(LerpPositionX(box, box.position, pizzaPlate.position, 1f, ()=>
                    {
                        box.transform.GetComponent<BoxController>().MakeBox();
                        GlobalManager.CameraController.WinRotate();
                    }));
                    //Win();
                }
            }));
        }
    }

    public void PizzaCheck()
    {
        overlaps = overlaps / 2;
        if(overlaps == 0)
        {
            print("PERFECT");
        }
        else if(overlaps == 1)
        {
            print("GOOD");
        }
        else if(overlaps == 2)
        {
            print("OKAY");
        }
        else
        {
            Lose();
            print("FAIL");
        }

        overlaps = 0;
    }

    public void Win()
    {
        state = GameState.Win;
        GlobalManager.UI_Manager.SwitchPanel("Won");
        NextLevel();


        //TODO 
    }

    public void Lose()
    {
        state = GameState.Lose;
        GlobalManager.UI_Manager.SwitchPanel("Lost");
        GlobalManager.LevelManager.ResetLevel();

        //TODO 
    }

    public void NextLevel()
    {
        GlobalManager.levelNum++;
        GlobalManager.UI_Manager.NextLevel(GlobalManager.levelNum, 0, 10);                    //different max for every level
        GlobalManager.LevelManager.SetLevel(GlobalManager.levelNum);
    }

    public void ChangeState(string _state)
    {
        switch (_state)
        {
            case "Menu":
                state = GameState.Menu;
                break;
            case "Play":
                state = GameState.Play;
                break;
            case "Won":
                state = GameState.Win;
                break;
            case "Lost":
                state = GameState.Lose;
                break;
            default:
                state = GameState.Menu;
                break;
        }
    }

    public IEnumerator LerpPositionX(Transform tr, Vector3 start, Vector3 end, float time, Action action = null)
    {
        float t = 0f;

        while (t < 1f)
        {

            Vector3 x = Vector3.Lerp(start, end, t);
            tr.position = new Vector3(x.x, tr.position.y, tr.position.z);

            t += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }

        tr.position = new Vector3(end.x, tr.position.y, tr.position.z);

        action?.Invoke();
    }
}

[System.Serializable]
public class IngredientType
{
    public GameManager.Ingredients ingredientName;
    public int amount;
}
