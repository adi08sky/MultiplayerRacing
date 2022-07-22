using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFuel : MonoBehaviour
{
    public DrivingScript ds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Add()
    {
        if (ds.enabled)
        {
            ds.nitroFuel += 1;
            ds.ChangeFuelTekst();
            return true;
        }
        else
        {
            return false;
        }
    }
}
