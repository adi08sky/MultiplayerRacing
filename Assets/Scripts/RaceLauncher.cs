using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.EventSystems;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public InputField playerName;
    public GameObject Buttons;
    public GameObject Panel;
    public GameObject[] Cars;
    int currentCar = 0;

    byte maxPlayerPerRoom = 3;
    bool isConnecting;
    public Text networkText;
    string gameVersion = "0.21";

    // Start is called before the first frame update
    void Awake()
    {        
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PlayerPrefs.HasKey("PlayerName")) playerName.text = PlayerPrefs.GetString("PlayerName");
        Cars[0].SetActive(true);
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

    public void NextCar()
    {
        Cars[currentCar].SetActive(false);
        currentCar++;
        if (currentCar >= Cars.Length)
            currentCar = 0;
        Cars[currentCar].SetActive(true);
        SetCar();
    }

    public void PreviousCar()
    {
        Cars[currentCar].SetActive(false);
        currentCar--;
        if (currentCar < 0)
            currentCar = Cars.Length - 1;
        Cars[currentCar].SetActive(true);
        SetCar();
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void SetCar()
    {
        PlayerPrefs.SetInt("PlayerCar", currentCar);
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            networkText.text += "OnConnectToMaster...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join random room.\n";
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = this.maxPlayerPerRoom
        });
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected because " + cause + "\n";
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        SetCar();
        networkText.text = "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + "players.\n";
        PhotonNetwork.LoadLevel("Main");
        PhotonNetwork.EnableCloseConnection = true;
    }

    public void ConnectNetwork()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;
        if (PhotonNetwork.IsConnected)
        {
            networkText.text += "Joing Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting...\n";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
