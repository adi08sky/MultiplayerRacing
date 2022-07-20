using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class RaceController : MonoBehaviourPunCallbacks
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
    public Button restartBtn;
    public Button nextLvlBtn;
    //Players
    public GameObject carPrefab;
    public Transform[] spawnPos;
    public int playerCount;
    //Multi
    public GameObject startRace;
    public GameObject waitingText;
    public RawImage mirror;


    void Start()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        startText.gameObject.SetActive(false);
        startRace.SetActive(false);
        waitingText.SetActive(false);

        //int randomStartPosition = Random.Range(0, spawnPos.Length);
        //Vector3 startPos = spawnPos[PhotonNetwork.LocalPlayer.GetPlayerNumber()].position;
        //Quaternion startRot = spawnPos[PhotonNetwork.LocalPlayer.GetPlayerNumber()].rotation;
        GameObject playerCar = null;

        if (PhotonNetwork.IsConnected)
        {
            Vector3 startPos = spawnPos[PhotonNetwork.LocalPlayer.ActorNumber - 1].position;
            Quaternion startRot = spawnPos[PhotonNetwork.LocalPlayer.ActorNumber - 1].rotation;

            object[] instanceData = new object[4];
            instanceData[0] = (string)PlayerPrefs.GetString("PlayerName");
            instanceData[1] = PlayerPrefs.GetInt("Red");
            instanceData[2] = PlayerPrefs.GetInt("Green");
            instanceData[3] = PlayerPrefs.GetInt("Blue");

            if (OnlinePlayer.LocalPlayerInstance == null)
            {
                playerCar = PhotonNetwork.Instantiate(carPrefab.name, startPos, startRot, 0, instanceData);
                playerCar.GetComponent<CarApperance>().SetLocalPlayer();
            }
            if (PhotonNetwork.IsMasterClient)
            {
                startRace.SetActive(true);
            }
            else
            {
                waitingText.SetActive(true);
            }
        }
        playerCar.GetComponent<DrivingScript>().enabled = true;
        playerCar.GetComponent<PlayerController>().enabled = true;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var players = PhotonNetwork.PlayerList;
            for (int i = 1; i < players.Length; i++)
            {
                PhotonNetwork.CloseConnection(players[i]);
            }
        }

        int finishedLap = 0;
        if (racing)
        {
            foreach (CheckPointController controller in carsController)
            {
                if (controller.lap == totalLaps + 1) finishedLap++;

                if (finishedLap == carsController.Length && racing)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        endPanel.SetActive(true);
                        Debug.Log("FinishRace");
                        racing = false;
                    }
                    else
                    {
                        endPanel.SetActive(true);
                        nextLvlBtn.interactable = false;
                        restartBtn.interactable = false;
                        Debug.Log("FinishRace");
                        racing = false;
                    }

                }
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

    public void LoadScene(string scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }

    [PunRPC]
    public void StartGame()
    {
        InvokeRepeating("CountDown", 1, 1);
        startRace.SetActive(false);
        waitingText.SetActive(false);
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    public void BeginGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartGame", RpcTarget.All, null);
        }
    }

    public void SetMirror(Camera backCamera)
    {
        mirror.texture = backCamera.targetTexture;
    }
}
