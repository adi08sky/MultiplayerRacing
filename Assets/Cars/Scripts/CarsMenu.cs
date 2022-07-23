using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarsMenu : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject Panel;
    public GameObject[] Cars;
    int currentCar = 0;
    void Start()
    {
        Cars[0].SetActive(true);
    }

    public void NextCar()
    {
        Cars[currentCar].SetActive(false);
        currentCar++;
        if (currentCar >= Cars.Length)
            currentCar = 0;
        Cars[currentCar].SetActive(true);
    }
    public void PreviousCar()
    {
        Cars[currentCar].SetActive(false);
        currentCar--;
        if (currentCar < 0)
            currentCar = Cars.Length - 1;
        Cars[currentCar].SetActive(true);
    }
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetKey(KeyCode.LeftControl))
        {
            Buttons.SetActive(false);
            Panel.SetActive(false);
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            Buttons.SetActive(true);
            Panel.SetActive(true);
        }
    }
}
