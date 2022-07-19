using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : MonoBehaviour
{
    public List<Text> placesNumbers;
    private void Start()
    {
        Leaderboard.Reset();
    }
    void LateUpdate()
    {
        List<string> places = Leaderboard.GetPlaces();
        for (int i = 0; i < placesNumbers.Count; i++)
        {
            if (i < places.Count)
            {
                placesNumbers[i].text = places[i];
            }
            else
            {
                placesNumbers[i].text = "";
            }
        }
    }
}

