using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarApperance : MonoBehaviour
{
    public string playerName;
    public int playerNumber;
    public Color carColor;
    public Text nameText;
    public Renderer carRenderer;
    public Camera backCamera;
    int carRegistartion;
    bool registartionSet = false;
    public CheckPointController checkPoint;

    private void LateUpdate()
    {
        if (!registartionSet)
        {
            carRegistartion = Leaderboard.RegisterCar(playerName);
            registartionSet = true;
            return;
        }
        Leaderboard.SetPosition(carRegistartion, checkPoint.lap, checkPoint.checkPoint);
    }

    public void SetNameAndColor(string name, Color color)
    {
        playerName = name;
        nameText.text = name;
        carRenderer.material.color = color;
        nameText.color = color;
    }

    public void SetLocalPlayer()
    {
        FindObjectOfType<CameraController>().SetCameraProperties(this.gameObject);
        playerName = PlayerPrefs.GetString("PlayerName");
        carColor = ColorCar.IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));

        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;

        RenderTexture rt = new RenderTexture(768, 256, 0);
        backCamera.targetTexture = rt;
        FindObjectOfType<RaceController>().SetMirror(backCamera);
    }
}
