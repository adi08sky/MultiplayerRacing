using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarApperance : MonoBehaviour
{
    public string playerName;
    public Color carColor;
    public Text nameText;
    public Renderer carRenderer;

    void Start()
    {
        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;
    }
}
