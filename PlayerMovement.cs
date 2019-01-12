using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int Direction;
    public Vector2 NextPos;

    public static float Timer;

    private Vector2 StartPosition, EndPosition , DeltaPosition;

    void Start()
    {
        //InvokeRepeating("TimerInvoker", 0 , Timer);
        Snake.GameOver = false;
        Timer = 0.2f;
        MaxSize = 1;
        CurrentSize = 1;
        FoodOnMap = false;
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
        if(Input.GetMouseButtonDown(0))
        {
            StartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndPosition = Input.mousePosition;
            DeltaPosition = EndPosition - StartPosition;
            if(Mathf.Abs(DeltaPosition.x) > Mathf.Abs(DeltaPosition.y))
            {
                if(DeltaPosition.x > 0)
                {
                    //D
                    Direction = 2;
                }
                else
                {
                    //A
                    Direction = 4;
                }
            }
            else
            {
                if (DeltaPosition.y > 0)
                {
                    //W
                    Direction = 1;
                }
                else
                {
                    //S
                    Direction = 3;
                }
            }
        }

        if (NESW != 2 && Direction == 1)
        {
            NESW = 0;
        }

        if (NESW != 3 && Direction == 2)
        {
            NESW = 1;
        }

        if (NESW != 0 && Direction == 3)
        {
            NESW = 2;
        }

        if (NESW != 1 && Direction == 4)
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
        if (Snake.GameOver == true)
        {
            StartCoroutine("LostGame");
        }
        
    }

    IEnumerator LostGame()
    {
        if (Snake.GameOver == true)
        {
            CancelInvoke("TimerInvoker");
            Destroy(Head);
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }
    }
}
