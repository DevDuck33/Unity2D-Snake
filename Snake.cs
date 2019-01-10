using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private PlayerMovement PlayerMovement;
    private Snake Next;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Food")
        {
            Debug.Log("Food");
            Destroy(other);
        }
    }
}
