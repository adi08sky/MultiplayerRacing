using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public int lap = 0;
    public int checkPoint = -1;
    int pointCount;
    public int nextPoint;
    public GameObject lastPoint;
    public GameObject hackNextPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        pointCount = checkpoints.Length;

        for (int i = 0; i < pointCount; i++)
        {
            if (checkpoints[i].name == "0")
            {
                lastPoint = checkpoints[i];
                hackNextPoint = checkpoints[i];
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            int thisPoint = int.Parse(other.gameObject.name);
            if (thisPoint == nextPoint)
            {
                lastPoint = other.gameObject;
                checkPoint = thisPoint;
                nextPoint++;
                nextPoint = nextPoint % pointCount;
                GameObject[] list = GameObject.FindGameObjectsWithTag("CheckPoint");                            //Dev hack
                hackNextPoint = list.Where( point => point.name == nextPoint.ToString()).FirstOrDefault();      //Dev hack
                

                if (checkPoint == 0)
                {
                    lap++;
                    Debug.Log("Lap: " + lap);
                }
                
            }

            if (thisPoint == 100)
            {
                lastPoint = other.gameObject;
                lap++;
                Debug.Log("Lap: " + lap);
            }
        }
    }
}
