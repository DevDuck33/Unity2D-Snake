using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Snake Next;

    public static bool GameOver;

    void Start()
    {
        GameOver = false;
    }

    public void SetNext(Snake IN)
    {
        Next = IN;
    }

    public Snake GetNext()
    {
        return Next;
    }

    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Food")
        {
            PlayerMovement.MaxSize ++;
            PlayerMovement.Timer -= 0.01f;
            Destroy(other.gameObject);
            PlayerMovement.FoodOnMap = false;
        }

        if (other.transform.tag == "Wall")
        {
            GameOver = true;
        }

        if (other.transform.tag == "Snake")
        {
            GameOver = true;
        }
    }

}
