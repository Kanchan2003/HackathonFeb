using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneHandler : MonoBehaviour
{
    public int loadScene = 1;
    public int leaderboardSize = 5;
    public void LoadGame()
    {
        SceneManager.LoadScene(loadScene);
    }
    public int[] GetLeaderboard()
    {
        string scoreString = PlayerPrefs.GetString("Scores");
        string[] stringArray = scoreString.Split(' ');
        int[] scores = new int[leaderboardSize];
        for (int i = 0; i < leaderboardSize; i++)
        {
            int tmp;
            if (i < stringArray.Length)
            {
                if (int.TryParse(stringArray[i], out tmp))
                {
                    scores[i] = tmp;
                }
                else
                {
                    scores[i] = -1;
                }
            } else
            {
                scores[i] = -1;
            }
            
        }
        return scores;
    }
    public void SaveLeaderboard(int[] scores, bool sort = true)
    {
        if (sort)
        {
            Array.Sort(scores);
        }
        string toSave = "";
        for (int i = 0; i < leaderboardSize; i++)
        {
            toSave += string.Format("{0} ", scores[i]);
        }
        PlayerPrefs.SetString("Scores", toSave);
    }
}
