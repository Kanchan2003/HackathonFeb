using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanelHandler : MonoBehaviour
{
    public SceneHandler sceneHandler;
    int[] scores;
    public Text[] slots;
    // Start is called before the first frame update
    void Start()
    {
        scores = sceneHandler.GetLeaderboard();
        for (int i = 0; i < scores.Length; i++)
        {
            slots[i].text = string.Format("{0}",scores[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
