using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColorCar : MonoBehaviour
{
    public Renderer[] rend;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Text redSliderText;
    public Text greenSliderText;
    public Text blueSliderText;
    public Color col;

    void Start()
    {
        col = IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));
        rend[0].material.color = col;
        redSlider.value = (int)(col.r * 255f);
        greenSlider.value = (int)(col.g * 255f);
        blueSlider.value = (int)(col.b * 255f);
    }
    void Update()
    {
        SetCarColor((int)redSlider.value, (int)greenSlider.value, (int)blueSlider.value);
        redSliderText.text = redSlider.value.ToString("n2");
        greenSliderText.text = greenSlider.value.ToString("n2");
        blueSliderText.text = blueSlider.value.ToString("n2");
    }

    public static Color IntToColor(int red, int green, int blue)
    {
        float r = (float)red / 255;
        float g = (float)green / 255;
        float b = (float)blue / 255;
        Color col = new Color(r, g, b);
        return col;
    }
    void SetCarColor(int red, int green, int blue)
    {
        Color col = IntToColor(red, green, blue);
        int playerCar = PlayerPrefs.GetInt("PlayerCar");
        rend[playerCar].material.color = col;
        PlayerPrefs.SetInt("Red", red);
        PlayerPrefs.SetInt("Green", green);
        PlayerPrefs.SetInt("Blue", blue);
    }
}
