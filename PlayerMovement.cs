using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static int MaxSize;
    public static int CurrentSize;
    public GameObject SnakePrefab;
    public Snake Head;
    public Snake Tail;

    public int XSpawn;
    public int YSpawn;
    public GameObject FoodPrefab;
    public GameObject CurrentFood;
    public static bool FoodOnMap = false;

    public int NESW;
    public Vector2 NextPos;

    public static float Timer;

    void Start()
    {
        //InvokeRepeating("TimerInvoker", 0 , Timer);
        Timer = 0.2f;
        MaxSize = 1;
        CurrentSize = 1;
    }

    void Update()
    {
        if (Snake.GameOver == false)
        {
            ChangeDirection();
            FoodFunction();
        }
        else
        {
            GameOver();
        }
    }

    void TimerInvoker()
    {
        Movement();
        if(CurrentSize >= MaxSize)
        {
            TailFunction();
        }
        else
        {
            CurrentSize++;
        }
    }

    void Movement()
    {
        GameObject Temp;
        NextPos = Head.transform.position;
        switch(NESW)
        {
            case 0:
                NextPos = new Vector2(NextPos.x, NextPos.y + 1);
                break;
            case 1:
                NextPos = new Vector2(NextPos.x + 1, NextPos.y);
                break;
            case 2:
                NextPos = new Vector2(NextPos.x, NextPos.y - 1);
                break;
            case 3:
                NextPos = new Vector2(NextPos.x - 1, NextPos.y);
                break;
        }
        Temp = (GameObject)Instantiate(SnakePrefab, NextPos, transform.rotation);
        Head.SetNext(Temp.GetComponent<Snake>());
        Head = Temp.GetComponent<Snake>();

        return;
    }

    void ChangeDirection()
    {
        if(NESW != 2 && Input.GetKeyDown(KeyCode.W))
        {
            NESW = 0;
        }

        if (NESW != 3 && Input.GetKeyDown(KeyCode.D))
        {
            NESW = 1;
        }

        if (NESW != 0 && Input.GetKeyDown(KeyCode.S))
        {
            NESW = 2;
        }

        if (NESW != 1 && Input.GetKeyDown(KeyCode.A))
        {
            NESW = 3;
        }
    }

    void TailFunction()
    {
        Snake TempSnake = Tail;
        Tail = Tail.GetNext();
        TempSnake.RemoveTail();
    }

    public void FoodFunction ()
    {
        if (FoodOnMap == false)
        {
            if (Timer >= 0.05f)
            {
                CancelInvoke("TimerInvoker");
                InvokeRepeating("TimerInvoker", 0, Timer);
            }
            int XPos = Random.Range(-XSpawn, XSpawn);
            int YPos = Random.Range(-YSpawn, (YSpawn-4));

            CurrentFood = (GameObject)Instantiate(FoodPrefab, new Vector2(XPos, YPos), transform.rotation);
            FoodOnMap = true;
        }
    }

    public void GameOver()
    {
        if(Snake.GameOver == true)
        {
            CancelInvoke("TimerInvoker");
            Destroy(Head);
            //Debug.Log("Game OVER");
        }
    }
}
