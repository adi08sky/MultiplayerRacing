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

    //Old Start - for local start
    /*void Start()
    {
        if (playerNumber == 0)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            carColor = ColorCar.IntToColor(PlayerPrefs.GetInt("Red"),
            PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));
        }
        else
        {
            playerName = "Random " + playerNumber;
            carColor = new Color(Random.Range(0f, 255f) / 255, Random.Range(0f, 255f) / 255,
            Random.Range(0f, 255f) / 255, 255);
        }

        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;
    }*/

    public void SetNameAndColor(string name, Color color)
    {
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
