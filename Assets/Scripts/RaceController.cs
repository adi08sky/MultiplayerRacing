using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("-----------------------------");
        InvokeRepeating("CountDown", 3, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CountDown()
    {
        if (timer != 0)
        {
            Debug.Log("Rozpoczêcie wyœcigu za: " + timer);
            timer--;
        }
        else
        {
            Debug.Log("Start!!!");
            racing = true;
            CancelInvoke("CountDown");
        }
    }
}
