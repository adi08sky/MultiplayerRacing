using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceLauncher : MonoBehaviour
{
    public InputField playerName;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName")) playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }
    public void StartRace()
    {
        SceneManager.LoadScene(0);
    }
}
