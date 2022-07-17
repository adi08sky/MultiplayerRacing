using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    //Race
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;
    public CheckPointController[] carsController;
    //UI
    public Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;
    public GameObject endPanel;
    //Players
    public GameObject carPrefab;
    public Transform[] spawnPos;
    public int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);
        InvokeRepeating("CountDown", 3, 1);

        for (int i = 0; i < playerCount; i++)
        {
            GameObject car = Instantiate(carPrefab);
            car.transform.position = spawnPos[i].position;
            car.transform.rotation = spawnPos[i].rotation;
            car.GetComponent<CarApperance>().playerNumber = i;
            if (i == 0)
            {
                car.GetComponent<PlayerController>().enabled = true;
                GameObject.FindObjectOfType<CameraController>().SetCameraProperties(car);
            }
        }
        
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];

        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    void LateUpdate()
    {
        int finishedLap = 0;
        foreach (CheckPointController controller in carsController)
        {                        
            if (controller.lap == totalLaps + 1) finishedLap++;

            if (finishedLap == carsController.Length && racing)
            {
                endPanel.SetActive(true);
                Debug.Log("FinishRace");
                racing = false;
            }
        }
    }
    
    void CountDown()
    {
        startText.gameObject.SetActive(true);
        if (timer != 0)
        {
            startText.text = timer.ToString();
            audioSource.PlayOneShot(count);
            timer--;
        }
        else
        {
            startText.text = "START!!!";
            audioSource.PlayOneShot(start);
            racing = true;
            CancelInvoke("CountDown");
            Invoke("HideStartText", 1);
        }
    }

    void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
